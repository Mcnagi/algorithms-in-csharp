namespace Algorithms.DataStructures;

/// <summary>
/// EXERCISE — a last-in, first-out stack backed by an array.
/// Every member below is O(1) (Push amortised). Model it on <see cref="DynamicArray{T}"/>:
/// a <c>T[]</c> field, a count, and doubling when full.
/// Tests: ArrayStackTests
/// </summary>
public sealed class ArrayStack<T>
{
    private T?[] stack;
    private int size;
    private const int SIZE_MULTIPLIER = 2;
    private const int INITIAL_SIZE = 8;
    private int capacity;

    public ArrayStack() {
        this.stack = new T?[INITIAL_SIZE];
        this.size = 0;
        this.capacity = INITIAL_SIZE;
    }
    
    /// <summary>Number of items on the stack.</summary>
    public int Count => this.size;

    public bool IsEmpty => Count == 0;

    /// <summary>Add to the top. Grow the array when full.</summary>
    public void Push(T item) {
        if (this.size == this.capacity) {
            this.capacity *= SIZE_MULTIPLIER;
            T?[] tmp = new T?[this.capacity];
            for (int i = 0; i < this.size; i ++) tmp[i] = this.stack[i];
            this.stack = tmp;
        }
        this.stack[this.size ++] = item; 
    }

    /// <summary>Remove and return the top item. Throw <see cref="InvalidOperationException"/> when empty.</summary>
    public T Pop() {
        if (this.size <= 0) throw new InvalidOperationException();
        
        T tmp = this.stack[--this.size]!;
        this.stack[this.size] = default;
        return tmp;
    }

    /// <summary>Return the top item without removing it. Throw <see cref="InvalidOperationException"/> when empty.</summary>
    public T Peek() {
        if (this.size <=0 ) throw new InvalidOperationException();
        return this.stack[size - 1]!;
    }
    
}
