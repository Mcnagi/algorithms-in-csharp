using Algorithms.DataStructures;

namespace Algorithms.Tests.DataStructures;

public class CircularQueueTests
{
    [Fact]
    public void Dequeue_ReturnsItemsInArrivalOrder()
    {
        var queue = new CircularQueue<int>();
        queue.Enqueue(1); queue.Enqueue(2); queue.Enqueue(3);

        Assert.Equal(1, queue.Dequeue());
        Assert.Equal(2, queue.Peek());
        Assert.Equal(2, queue.Count);
    }

    [Fact]
    public void DequeueAndPeek_WhenEmpty_Throw()
    {
        var queue = new CircularQueue<int>();

        Assert.True(queue.IsEmpty);
        Assert.Throws<InvalidOperationException>(() => queue.Dequeue());
        Assert.Throws<InvalidOperationException>(() => queue.Peek());
    }

    [Fact]
    public void InterleavedOperations_WrapAroundAndGrow_InOrder()
    {
        // Mixing enqueues and dequeues makes the head move, so the buffer wraps
        // before it grows — the case that catches most ring-buffer bugs.
        var queue = new CircularQueue<int>();
        var expected = new Queue<int>();
        var next = 0;
        for (var round = 0; round < 200; round++)
        {
            for (var i = 0; i < 3; i++) { queue.Enqueue(next); expected.Enqueue(next); next++; }
            for (var i = 0; i < 2; i++) Assert.Equal(expected.Dequeue(), queue.Dequeue());
        }

        Assert.Equal(expected.Count, queue.Count);
        while (expected.Count > 0) Assert.Equal(expected.Dequeue(), queue.Dequeue());
    }
}
