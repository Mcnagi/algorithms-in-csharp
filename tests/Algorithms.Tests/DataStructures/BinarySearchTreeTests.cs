using Algorithms.DataStructures;

namespace Algorithms.Tests.DataStructures;

public class BinarySearchTreeTests
{
    //        50
    //      /    \
    //    30      70
    //   /  \    /
    //  20  40  60
    //       \
    //        45
    private static BinarySearchTree<int> SampleTree()
    {
        var tree = new BinarySearchTree<int>();
        foreach (var v in new[] { 50, 30, 70, 20, 40, 60, 45 }) tree.Insert(v);
        return tree;
    }

    [Fact]
    public void Insert_Duplicate_ReturnsFalse()
    {
        var tree = SampleTree();

        Assert.False(tree.Insert(40));
        Assert.Equal(7, tree.Count);
    }

    [Fact]
    public void Contains_FindsInsertedValuesOnly()
    {
        var tree = SampleTree();

        Assert.True(tree.Contains(45));
        Assert.False(tree.Contains(55));
    }

    [Fact]
    public void InOrder_ReturnsAscending()
    {
        Assert.Equal(new[] { 20, 30, 40, 45, 50, 60, 70 }, SampleTree().InOrder());
    }

    [Fact]
    public void Min_ReturnsSmallest_AndThrowsWhenEmpty()
    {
        Assert.Equal(20, SampleTree().Min());
        Assert.Throws<InvalidOperationException>(() => new BinarySearchTree<int>().Min());
    }

    [Fact(Skip = Exercise.Todo)]
    public void Height_CountsNodesOnLongestPath()
    {
        Assert.Equal(0, new BinarySearchTree<int>().Height());
        Assert.Equal(4, SampleTree().Height()); // 50 → 30 → 40 → 45
    }

    [Fact(Skip = Exercise.Todo)]
    public void Height_SortedInsertsDegradeToAList()
    {
        var tree = new BinarySearchTree<int>();
        for (var i = 0; i < 100; i++) tree.Insert(i);

        Assert.Equal(100, tree.Height()); // why balanced trees (AVL, red-black) exist
    }

    [Fact(Skip = Exercise.Todo)]
    public void Remove_Leaf()
    {
        var tree = SampleTree();

        Assert.True(tree.Remove(20));
        Assert.Equal(new[] { 30, 40, 45, 50, 60, 70 }, tree.InOrder());
        Assert.Equal(6, tree.Count);
    }

    [Fact(Skip = Exercise.Todo)]
    public void Remove_NodeWithOneChild()
    {
        var tree = SampleTree();

        Assert.True(tree.Remove(70));
        Assert.Equal(new[] { 20, 30, 40, 45, 50, 60 }, tree.InOrder());
    }

    [Fact(Skip = Exercise.Todo)]
    public void Remove_NodeWithTwoChildren()
    {
        var tree = SampleTree();

        Assert.True(tree.Remove(30));
        Assert.Equal(new[] { 20, 40, 45, 50, 60, 70 }, tree.InOrder());
        Assert.False(tree.Contains(30));
    }

    [Fact(Skip = Exercise.Todo)]
    public void Remove_Root_AndMissingValue()
    {
        var tree = SampleTree();

        Assert.True(tree.Remove(50));
        Assert.False(tree.Remove(999));
        Assert.Equal(new[] { 20, 30, 40, 45, 60, 70 }, tree.InOrder());
        Assert.Equal(6, tree.Count);
    }
}
