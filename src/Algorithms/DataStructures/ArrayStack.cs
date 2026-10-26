namespace Algorithms.DataStructures;

/// <summary>
/// REFERENCE SOLUTION — a last-in, first-out stack backed by an array that doubles when full.
/// Pop and Peek are O(1); Push is amortised O(1).
/// </summary>
public sealed class ArrayStack<T>
{
    private const int DefaultCapacity = 4;
    private T[] _items = new T[DefaultCapacity];

    /// <summary>Number of items on the stack. O(1).</summary>
    public int Count { get; private set; }

    /// <summary>Whether the stack has no items. O(1).</summary>
    public bool IsEmpty => Count == 0;

    /// <summary>
    /// Add to the top. Amortised O(1): doubling means n pushes copy fewer than 2n items in total.
    /// </summary>
    public void Push(T item)
    {
        if (Count == _items.Length)
            Array.Resize(ref _items, _items.Length * 2); // allocates + copies, same as a manual loop
        _items[Count++] = item;
    }

    /// <summary>Remove and return the top item. O(1).</summary>
    /// <exception cref="InvalidOperationException">The stack is empty.</exception>
    public T Pop()
    {
        ThrowIfEmpty();
        var item = _items[--Count];
        _items[Count] = default!; // release the reference so the GC can reclaim it
        return item;
    }

    /// <summary>Return the top item without removing it. O(1).</summary>
    /// <exception cref="InvalidOperationException">The stack is empty.</exception>
    public T Peek()
    {
        ThrowIfEmpty();
        return _items[Count - 1];
    }

    private void ThrowIfEmpty()
    {
        if (Count == 0) throw new InvalidOperationException("Stack is empty.");
    }
}
