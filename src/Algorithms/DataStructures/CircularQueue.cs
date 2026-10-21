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
    public int Count => throw new NotImplementedException("Exercise: see LEARNING.md");

    public bool IsEmpty => Count == 0;

    /// <summary>Add to the back. Grow the buffer when full.</summary>
    public void Enqueue(T item) => throw new NotImplementedException("Exercise: see LEARNING.md");

    /// <summary>Remove and return the front item. Throw <see cref="InvalidOperationException"/> when empty.</summary>
    public T Dequeue() => throw new NotImplementedException("Exercise: see LEARNING.md");

    /// <summary>Return the front item without removing it. Throw <see cref="InvalidOperationException"/> when empty.</summary>
    public T Peek() => throw new NotImplementedException("Exercise: see LEARNING.md");
}
