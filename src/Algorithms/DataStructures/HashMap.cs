namespace Algorithms.DataStructures;

/// <summary>
/// WORKED EXAMPLE — a hash map using separate chaining: each bucket holds a
/// small list of entries whose keys hashed to that bucket.
/// </summary>
public sealed class HashMap<TKey, TValue> where TKey : notnull
{
    private const double MaxLoadFactor = 0.75;
    private List<KeyValuePair<TKey, TValue>>?[] _buckets;
    private readonly IEqualityComparer<TKey> _comparer;

    public HashMap(int capacity = 16, IEqualityComparer<TKey>? comparer = null)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        _buckets = new List<KeyValuePair<TKey, TValue>>?[capacity];
        _comparer = comparer ?? EqualityComparer<TKey>.Default;
    }

    public int Count { get; private set; }

    /// <summary>Exposed so tests can check resizing happens.</summary>
    public int BucketCount => _buckets.Length;

    /// <summary>Insert or overwrite. Average O(1); worst case O(n) if every key collides.</summary>
    public void Put(TKey key, TValue value)
    {
        var bucket = _buckets[IndexFor(key, _buckets.Length)] ??= [];
        for (var i = 0; i < bucket.Count; i++)
        {
            if (_comparer.Equals(bucket[i].Key, key))
            {
                bucket[i] = new(key, value);
                return;
            }
        }
        bucket.Add(new(key, value));
        Count++;
        if ((double)Count / _buckets.Length > MaxLoadFactor) Resize();
    }

    /// <summary>Average O(1).</summary>
    public bool TryGetValue(TKey key, out TValue value)
    {
        var bucket = _buckets[IndexFor(key, _buckets.Length)];
        if (bucket is not null)
        {
            foreach (var entry in bucket)
            {
                if (_comparer.Equals(entry.Key, key))
                {
                    value = entry.Value;
                    return true;
                }
            }
        }
        value = default!;
        return false;
    }

    public bool ContainsKey(TKey key) => TryGetValue(key, out _);

    /// <summary>Average O(1).</summary>
    public bool Remove(TKey key)
    {
        var bucket = _buckets[IndexFor(key, _buckets.Length)];
        if (bucket is null) return false;
        var removed = bucket.RemoveAll(e => _comparer.Equals(e.Key, key)) > 0;
        if (removed) Count--;
        return removed;
    }

    public IEnumerable<TKey> Keys =>
        _buckets.Where(b => b is not null).SelectMany(b => b!).Select(e => e.Key);

    // Mask off the sign bit rather than Math.Abs: Math.Abs(int.MinValue) throws.
    private int IndexFor(TKey key, int bucketCount) =>
        (_comparer.GetHashCode(key) & int.MaxValue) % bucketCount;

    /// <summary>Double the buckets and rehash everything. O(n), but rare enough to be amortised O(1) per Put.</summary>
    private void Resize()
    {
        var newBuckets = new List<KeyValuePair<TKey, TValue>>?[_buckets.Length * 2];
        foreach (var bucket in _buckets)
        {
            if (bucket is null) continue;
            foreach (var entry in bucket)
                (newBuckets[IndexFor(entry.Key, newBuckets.Length)] ??= []).Add(entry);
        }
        _buckets = newBuckets;
    }
}
