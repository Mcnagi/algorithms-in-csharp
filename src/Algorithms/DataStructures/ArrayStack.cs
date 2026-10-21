namespace Algorithms.DataStructures;

/// <summary>
/// EXERCISE — a last-in, first-out stack backed by an array.
/// Every member below is O(1) (Push amortised). Model it on <see cref="DynamicArray{T}"/>:
/// a <c>T[]</c> field, a count, and doubling when full.
/// Tests: ArrayStackTests
/// </summary>
public sealed class ArrayStack<T>
{
    /// <summary>Number of items on the stack.</summary>
    public int Count => throw new NotImplementedException("Exercise: see LEARNING.md");

    public bool IsEmpty => Count == 0;

    /// <summary>Add to the top. Grow the array when full.</summary>
    public void Push(T item) => throw new NotImplementedException("Exercise: see LEARNING.md");

    /// <summary>Remove and return the top item. Throw <see cref="InvalidOperationException"/> when empty.</summary>
    public T Pop() => throw new NotImplementedException("Exercise: see LEARNING.md");

    /// <summary>Return the top item without removing it. Throw <see cref="InvalidOperationException"/> when empty.</summary>
    public T Peek() => throw new NotImplementedException("Exercise: see LEARNING.md");
}
