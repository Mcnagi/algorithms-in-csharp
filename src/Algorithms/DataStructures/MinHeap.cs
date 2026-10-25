namespace Algorithms.DataStructures;

/// <summary>
/// EXERCISE — a binary min-heap stored in an array: the smallest item is always at index 0.
/// Target: Push and Pop O(log n), Peek O(1).
/// The index helpers are done for you. Push = add at the end, then "sift up";
/// Pop = move the last item to the root, then "sift down" towards the smaller child.
/// Tests: MinHeapTests
/// </summary>
public sealed class MinHeap<T> where T : IComparable<T>
{
    private const int DefaultCapacity = 8;
    private T[] _heap = new T[DefaultCapacity];

    
    public int Count { get; private set; }

    public bool IsEmpty => Count == 0;

    public void Push(T item)
    {
        EnsureCapacity(Count + 1);
        _heap[Count] = item;
        
        // Push up the inserted item, if any parent.
        int i = Count;
        while(true)
        {
            int p = Parent(i);
            if (p < 0) break;
            if (_heap[p].CompareTo(item) > 0)
            {
                Swap(p, i);
                i = p;
            }
            else break;
        }

        Count++;
    }

    /// <summary>Remove and return the smallest item. Throw <see cref="InvalidOperationException"/> when empty.</summary>
    public T Pop()
    {
        ThrowIfEmpty();
        
        // Take the last item to the root.
        T ans = _heap[0];
        _heap[0] = _heap[--Count];
        _heap[Count] = default!;

        // Sift down to satisfy min heap condition.
        int i = 0;
        while (i < Count)
        {
            int l = Left(i), r = Right(i);
            int s = l;
            
            // Already pushed down to leaf
            if (l >= Count) break;

            // Compare between the childrens
            if (r < Count && _heap[l].CompareTo(_heap[r]) > 0) s = r;
            // Compare parent to the smaller child
            if (_heap[i].CompareTo(_heap[s]) > 0)
            {
                Swap(i, s);
                i = s;
            }
            else break;
        }

        return ans;
    }

    /// <summary>Return the smallest item without removing it. Throw <see cref="InvalidOperationException"/> when empty.</summary>
    public T Peek()
    {
        ThrowIfEmpty();
        return _heap[0];
    }

    internal void Swap(int a, int b)
    {
        (_heap[b], _heap[a]) = (_heap[a], _heap[b]);
    }

    internal void EnsureCapacity(int needed)
    {
        if (_heap.Length < needed)
        {
            T[] newHeap = new T[_heap.Length * 2];
            Array.Copy(_heap, newHeap, Count);
            _heap = newHeap;
        }
    }

    internal void ThrowIfEmpty()
    {
        if (IsEmpty)
            throw new InvalidOperationException("Heap is empty.");
    }

    internal static int Parent(int i) => (i - 1) / 2;
    internal static int Left(int i) => 2 * i + 1;
    internal static int Right(int i) => 2 * i + 2;
}
