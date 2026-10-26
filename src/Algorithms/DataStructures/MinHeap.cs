namespace Algorithms.DataStructures;

/// <summary>
/// REFERENCE SOLUTION — a binary min-heap stored in a list: the smallest item is always at index 0.
/// Push and Pop are O(log n), Peek is O(1).
/// </summary>
/// <remarks>
/// The heap rule is only "every parent ≤ its children" — siblings are unordered, so a heap
/// is cheaper to maintain than a sorted list. The tree is implicit: node i's children live at
/// 2i+1 and 2i+2, so there are no node objects or pointers at all.
/// </remarks>
public sealed class MinHeap<T> where T : IComparable<T>
{
    private readonly List<T> _items = [];

    public int Count => _items.Count;

    public bool IsEmpty => Count == 0;

    /// <summary>Add at the end (keeps the tree complete), then sift up. O(log n).</summary>
    public void Push(T item)
    {
        _items.Add(item);
        SiftUp(_items.Count - 1);
    }

    /// <summary>Remove and return the smallest item. O(log n).</summary>
    /// <exception cref="InvalidOperationException">The heap is empty.</exception>
    public T Pop()
    {
        var min = Peek();
        var last = _items.Count - 1;
        _items[0] = _items[last]; // the last leaf is the only item we can remove in O(1)
        _items.RemoveAt(last);
        if (_items.Count > 0) SiftDown(0);
        return min;
    }

    /// <summary>Return the smallest item without removing it. O(1).</summary>
    /// <exception cref="InvalidOperationException">The heap is empty.</exception>
    public T Peek() =>
        _items.Count > 0 ? _items[0] : throw new InvalidOperationException("Heap is empty.");

    /// <summary>Swap with the parent while smaller than it.</summary>
    private void SiftUp(int i)
    {
        while (i > 0 && Less(i, Parent(i)))
        {
            Swap(i, Parent(i));
            i = Parent(i);
        }
    }

    /// <summary>
    /// Swap with the SMALLER child while larger than it. Swapping with the larger child
    /// would put that child above its smaller sibling and break the heap rule.
    /// </summary>
    private void SiftDown(int i)
    {
        while (true)
        {
            int smallest = i, left = Left(i), right = Right(i);
            if (left < _items.Count && Less(left, smallest)) smallest = left;
            if (right < _items.Count && Less(right, smallest)) smallest = right;
            if (smallest == i) return;
            Swap(i, smallest);
            i = smallest;
        }
    }

    private bool Less(int a, int b) => _items[a].CompareTo(_items[b]) < 0;

    private void Swap(int a, int b) => (_items[a], _items[b]) = (_items[b], _items[a]);

    internal static int Parent(int i) => (i - 1) / 2;
    internal static int Left(int i) => 2 * i + 1;
    internal static int Right(int i) => 2 * i + 2;
}
