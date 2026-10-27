using System.Collections;

namespace Algorithms.DataStructures;

/// <summary>
/// WORKED EXAMPLE — a growable array, like <see cref="List{T}"/>.
/// Read this first: it sets the patterns the exercises follow
/// (argument checks, a version counter, Big-O notes on every member).
/// </summary>
public sealed class DynamicArray<T> : IEnumerable<T>
{
    private const int DefaultCapacity = 4;
    private T[] _items;
    private int _version; // bumps on every change so enumerators can detect edits mid-loop

    public DynamicArray(int capacity = DefaultCapacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        _items = new T[capacity];
    }

    /// <summary>Number of stored items. O(1).</summary>
    public int Count { get; private set; }

    /// <summary>Size of the backing array. O(1).</summary>
    public int Capacity => _items.Length;

    /// <summary>Read or write by index. O(1).</summary>
    public T this[int index]
    {
        get
        {
            CheckIndex(index);
            return _items[index];
        }
        set
        {
            CheckIndex(index);
            _items[index] = value;
            _version++;
        }
    }

    /// <summary>
    /// Append to the end. Amortised O(1): doubling means each item is copied
    /// at most a constant number of times on average.
    /// </summary>
    public void Add(T item)
    {
        EnsureCapacity(Count + 1);
        _items[Count++] = item;
        _version++;
    }

    /// <summary>Insert at <paramref name="index"/>, shifting later items right. O(n).</summary>
    public void Insert(int index, T item)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, Count);
        EnsureCapacity(Count + 1);
        Array.Copy(_items, index, _items, index + 1, Count - index);
        _items[index] = item;
        Count++;
        _version++;
    }

    /// <summary>Remove at <paramref name="index"/>, shifting later items left. O(n).</summary>
    public void RemoveAt(int index)
    {
        CheckIndex(index);
        Array.Copy(_items, index + 1, _items, index, Count - index - 1);
        Count--;
        _items[Count] = default!; // drop the reference so the GC can collect it
        _version++;
    }

    /// <summary>Linear search. O(n).</summary>
    public int IndexOf(T item)
    {
        var comparer = EqualityComparer<T>.Default;
        for (var i = 0; i < Count; i++)
        {
            if (comparer.Equals(_items[i], item)) return i;
        }
        return -1;
    }

    public bool Contains(T item) => IndexOf(item) >= 0;

    public IEnumerator<T> GetEnumerator()
    {
        var version = _version;
        for (var i = 0; i < Count; i++)
        {
            if (version != _version)
                throw new InvalidOperationException("Collection was modified during enumeration.");
            yield return _items[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    private void EnsureCapacity(int needed)
    {
        if (needed <= _items.Length) return;
        var newItems = new T[Math.Max(needed, _items.Length * 2)];
        Array.Copy(_items, newItems, Count);
        _items = newItems;
    }

    private void CheckIndex(int index)
    {
        if ((uint)index >= (uint)Count) // one unsigned compare catches negatives too
            throw new ArgumentOutOfRangeException(nameof(index), index, $"Index must be in [0, {Count}).");
    }
}