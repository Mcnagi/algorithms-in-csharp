namespace Algorithms.DataStructures;

/// <summary>
/// A last-in, first-out stack backed by an array that doubles when full.
/// Every member is O(1); <see cref="Push"/> is amortised O(1).
/// </summary>
public sealed class ArrayStack<T>
{
    private const int GrowthFactor = 2;
    private const int DefaultCapacity = 8;
    private T[] _stack = new T[DefaultCapacity];
    private int _size = 0;

    /// <summary>Number of items on the stack. O(1).</summary>
    public int Count => _size;

    /// <summary>Whether the stack has no items. O(1).</summary>
    public bool IsEmpty => Count == 0;

    /// <summary>
    /// Add <paramref name="item"/> to the top. Amortised O(1): the array doubles
    /// when full, so each item is copied at most a constant number of times on average.
    /// </summary>
    public void Push(T item) 
    {
        if (_size == _stack.Length)
        {
            T[] tmp = new T[_stack.Length * GrowthFactor];
            Array.Copy(_stack, tmp, _size);
            _stack = tmp;
        }
        _stack[_size++] = item;
    }

    /// <summary>Remove and return the top item. O(1).</summary>
    /// <exception cref="InvalidOperationException">The stack is empty.</exception>
    public T Pop()
    {
        if (IsEmpty) ThrowIfEmpty();
        T item = _stack[--_size];
        _stack[_size] = default!;
        return item;
    }

    /// <summary>Return the top item without removing it. O(1).</summary>
    /// <exception cref="InvalidOperationException">The stack is empty.</exception>
    public T Peek()
    {
        if (IsEmpty) ThrowIfEmpty();
        return _stack[_size - 1];
    }

    private void ThrowIfEmpty()
    {
        if (IsEmpty) throw new InvalidOperationException("Stack is empty.");
    }
    
}