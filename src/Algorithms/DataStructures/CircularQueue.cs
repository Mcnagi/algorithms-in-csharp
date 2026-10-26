namespace Algorithms.DataStructures;

/// <summary>
/// REFERENCE SOLUTION — a first-in, first-out queue backed by a circular (ring) buffer.
/// Dequeue and Peek are O(1); Enqueue is amortised O(1).
/// </summary>
/// <remarks>
/// Only two numbers describe the queue: <c>_head</c> (the slot to dequeue next) and
/// <see cref="Count"/>. The tail is derived, never stored, so the two can't disagree.
/// </remarks>
public sealed class CircularQueue<T>
{
    private const int DefaultCapacity = 4;
    private T[] _items = new T[DefaultCapacity];
    private int _head;

    public int Count { get; private set; }

    public bool IsEmpty => Count == 0;

    /// <summary>Add to the back. Amortised O(1).</summary>
    public void Enqueue(T item)
    {
        if (Count == _items.Length) Grow();
        _items[(_head + Count) % _items.Length] = item;
        Count++;
    }

    /// <summary>Remove and return the front item. O(1).</summary>
    /// <exception cref="InvalidOperationException">The queue is empty.</exception>
    public T Dequeue()
    {
        ThrowIfEmpty();
        var item = _items[_head];
        _items[_head] = default!;
        _head = (_head + 1) % _items.Length;
        Count--;
        return item;
    }

    /// <summary>Return the front item without removing it. O(1).</summary>
    /// <exception cref="InvalidOperationException">The queue is empty.</exception>
    public T Peek()
    {
        ThrowIfEmpty();
        return _items[_head];
    }

    /// <summary>
    /// Copy items out in queue order so the new buffer starts at index 0. A plain
    /// <see cref="Array.Copy(Array, Array, int)"/> would be wrong once the buffer has wrapped.
    /// Two block copies (head→end, then start→tail) avoid the per-item modulo.
    /// </summary>
    private void Grow()
    {
        var bigger = new T[_items.Length * 2];
        var firstRun = Math.Min(Count, _items.Length - _head);
        Array.Copy(_items, _head, bigger, 0, firstRun);
        Array.Copy(_items, 0, bigger, firstRun, Count - firstRun);
        _items = bigger;
        _head = 0;
    }

    private void ThrowIfEmpty()
    {
        if (Count == 0) throw new InvalidOperationException("Queue is empty.");
    }
}
