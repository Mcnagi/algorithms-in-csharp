namespace Algorithms.DataStructures;

/// <summary>
/// A last-in, first-out stack backed by an array that doubles when full.
/// Every member is O(1); <see cref="Push"/> is amortised O(1).
/// </summary>
public sealed class ArrayStack<T>
{
    private T?[] stack;
    private int size;
    private const int SIZE_MULTIPLIER = 2;
    private const int INITIAL_SIZE = 8;


    public ArrayStack() {
        this.stack = new T?[INITIAL_SIZE];
        this.size = 0;
    }
    
    /// <summary>Number of items on the stack. O(1).</summary>
    public int Count => this.size;

    /// <summary>Whether the stack has no items. O(1).</summary>
    public bool IsEmpty => Count == 0;

    /// <summary>
    /// Add <paramref name="item"/> to the top. Amortised O(1): the array doubles
    /// when full, so each item is copied at most a constant number of times on average.
    /// </summary>
    public void Push(T item) {
        if (this.size == this.stack.Length) {
            T?[] tmp = new T?[this.stack.Length * SIZE_MULTIPLIER];
            for (int i = 0; i < this.size; i ++) tmp[i] = this.stack[i];
            this.stack = tmp;
        }
        this.stack[this.size ++] = item; 
    }

    /// <summary>Remove and return the top item. O(1).</summary>
    /// <exception cref="InvalidOperationException">The stack is empty.</exception>
    public T Pop() {
        if (this.size <= 0) throw new InvalidOperationException();
        
        T tmp = this.stack[--this.size]!;
        this.stack[this.size] = default;
        return tmp;
    }

    /// <summary>Return the top item without removing it. O(1).</summary>
    /// <exception cref="InvalidOperationException">The stack is empty.</exception>
    public T Peek() {
        if (this.size <=0 ) throw new InvalidOperationException();
        return this.stack[size - 1]!;
    }
    
}
