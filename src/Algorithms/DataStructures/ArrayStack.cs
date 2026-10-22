namespace Algorithms.DataStructures;

/// <summary>
/// A last-in, first-out stack backed by an array that doubles when full.
/// Every member is O(1); <see cref="Push"/> is amortised O(1).
/// </summary>
public sealed class ArrayStack<T>
{
    private T[] _stack;
    private int _size;
    private const int GrowthFactor = 2;
    private const int InitialSize = 8;


    public ArrayStack() {
        _stack = new T[InitialSize];
        _size = 0;
    }
    
    /// <summary>Number of items on the stack. O(1).</summary>
    public int Count => _size;

    /// <summary>Whether the stack has no items. O(1).</summary>
    public bool IsEmpty => Count == 0;

    /// <summary>
    /// Add <paramref name="item"/> to the top. Amortised O(1): the array doubles
    /// when full, so each item is copied at most a constant number of times on average.
    /// </summary>
    public void Push(T item) {
        if (_size == _stack.Length) {
            T[] tmp = new T[_stack.Length * GrowthFactor];
            Array.Copy(_stack, tmp, _size);
            _stack = tmp;
        }
        _stack[_size ++] = item; 
    }

    /// <summary>Remove and return the top item. O(1).</summary>
    /// <exception cref="InvalidOperationException">The stack is empty.</exception>
    public T Pop() {
        if (_size <= 0) throw new InvalidOperationException();
        
        T tmp = _stack[--_size];
        _stack[_size] = default!;
        return tmp;
    }

    /// <summary>Return the top item without removing it. O(1).</summary>
    /// <exception cref="InvalidOperationException">The stack is empty.</exception>
    public T Peek() {
        if (_size <=0 ) throw new InvalidOperationException();
        return _stack[_size - 1];
    }
    
}
