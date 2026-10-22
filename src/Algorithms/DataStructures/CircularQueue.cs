namespace Algorithms.DataStructures;

/// <summary>
/// EXERCISE — a first-in, first-out queue backed by a circular (ring) buffer.
/// Target: Enqueue amortised O(1), Dequeue and Peek O(1).
/// Hint: keep <c>_head</c> (next to dequeue) and <c>Count</c>; the tail slot is
/// <c>(_head + Count) % _items.Length</c>. When growing, copy items out in queue
/// order so the new buffer starts at index 0.
/// Tests: CircularQueueTests
/// </summary>
public sealed class CircularQueue<T>
{
    private const int DefaultCapacity = 8;
    private const int GrowthFactor = 2;
    private T[] _q = new T[DefaultCapacity];
    private int _head = 0;

    ///<summary>Return the number of elements. O(1).</summary>
    public int Count {get; private set;}

    ///<summary>Whether the queue is empty. O(1).</summary>
    public bool IsEmpty => Count == 0;

    /// <summary>Add to the back. Grow the buffer when full. Amortised O(1).</summary>
    public void Enqueue(T item) 
    {
        EnsureCapacity(Count + 1);
        int index = (_head + Count) % _q.Length; 
        _q[index] = item;
        Count++;
    }

    /// <summary>Remove and return the front item. Throw <see cref="InvalidOperationException"/> when empty. O(1).</summary>
    public T Dequeue()
    {
        ThrowIfEmpty();
        T item = _q[_head];
        _q[_head] = default!;
        _head = (_head + 1) % _q.Length;
        Count --;
        return item;
    }

    /// <summary>Return the front item without removing it. Throw <see cref="InvalidOperationException"/> when empty. O(1).</summary>
    public T Peek()
    {
        ThrowIfEmpty();
        return _q[_head];
    }

    private void ThrowIfEmpty()
    {
        if (IsEmpty) throw new InvalidOperationException("Queue is empty.");
    }

    private void EnsureCapacity(int needed)
    {
        if (needed <= _q.Length) return;
        int newCapacity = Math.Max(needed, _q.Length * GrowthFactor);
        T[] newQ = new T[newCapacity];
        for (int i = 0; i < Count; i ++) 
        {
            newQ[i] = _q[(_head + i) % _q.Length];
        }
        _q = newQ;
        _head = 0;
    }
}
