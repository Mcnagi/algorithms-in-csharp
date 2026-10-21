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
    public int Count => throw new NotImplementedException("Exercise: see LEARNING.md");

    public bool IsEmpty => Count == 0;

    public void Push(T item) => throw new NotImplementedException("Exercise: see LEARNING.md");

    /// <summary>Remove and return the smallest item. Throw <see cref="InvalidOperationException"/> when empty.</summary>
    public T Pop() => throw new NotImplementedException("Exercise: see LEARNING.md");

    /// <summary>Return the smallest item without removing it. Throw <see cref="InvalidOperationException"/> when empty.</summary>
    public T Peek() => throw new NotImplementedException("Exercise: see LEARNING.md");

    internal static int Parent(int i) => (i - 1) / 2;
    internal static int Left(int i) => 2 * i + 1;
    internal static int Right(int i) => 2 * i + 2;
}
