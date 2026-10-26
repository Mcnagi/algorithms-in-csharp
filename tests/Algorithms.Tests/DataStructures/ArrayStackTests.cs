using Algorithms.DataStructures;

namespace Algorithms.Tests.DataStructures;

public class ArrayStackTests
{
    [Fact]
    public void Pop_ReturnsItemsInReverseOrder()
    {
        var stack = new ArrayStack<int>();
        stack.Push(1); stack.Push(2); stack.Push(3);

        Assert.Equal(3, stack.Pop());
        Assert.Equal(2, stack.Pop());
        Assert.Equal(1, stack.Count);
    }

    [Fact]
    public void Peek_DoesNotRemove()
    {
        var stack = new ArrayStack<string>();
        stack.Push("x");

        Assert.Equal("x", stack.Peek());
        Assert.Equal(1, stack.Count);
    }

    [Fact]
    public void PopAndPeek_WhenEmpty_Throw()
    {
        var stack = new ArrayStack<int>();

        Assert.True(stack.IsEmpty);
        Assert.Throws<InvalidOperationException>(() => stack.Pop());
        Assert.Throws<InvalidOperationException>(() => stack.Peek());
    }

    [Fact]
    public void Push_ManyItems_Grows()
    {
        var stack = new ArrayStack<int>();
        for (var i = 0; i < 10_000; i++) stack.Push(i);
        for (var i = 9_999; i >= 0; i--) Assert.Equal(i, stack.Pop());

        Assert.True(stack.IsEmpty);
    }
}
