using Algorithms.DataStructures;

namespace Algorithms.Tests.DataStructures;

public class MinHeapTests
{
    [Fact()]
    public void Pop_ReturnsItemsSmallestFirst()
    {
        var random = new Random(1234);
        var values = Enumerable.Range(0, 500).Select(_ => random.Next(-1000, 1000)).ToArray();
        var heap = new MinHeap<int>();
        foreach (var v in values) heap.Push(v);

        var popped = new List<int>();
        while (!heap.IsEmpty) popped.Add(heap.Pop());

        Assert.Equal(values.Order(), popped);
    }

    [Fact()]
    public void Peek_ReturnsMinimumWithoutRemoving()
    {
        var heap = new MinHeap<int>();
        heap.Push(5); heap.Push(1); heap.Push(3);

        Assert.Equal(1, heap.Peek());
        Assert.Equal(3, heap.Count);
    }

    [Fact()]
    public void PopAndPeek_WhenEmpty_Throw()
    {
        var heap = new MinHeap<int>();

        Assert.Throws<InvalidOperationException>(() => heap.Pop());
        Assert.Throws<InvalidOperationException>(() => heap.Peek());
    }
}