using Algorithms.DataStructures;

namespace Algorithms.Tests.DataStructures;

public class SinglyLinkedListTests
{
    private static SinglyLinkedList<int> ListOf(params int[] values)
    {
        var list = new SinglyLinkedList<int>();
        foreach (var v in values) list.AddLast(v);
        return list;
    }

    [Fact]
    public void AddFirstAndAddLast_KeepOrder()
    {
        var list = ListOf(2, 3);
        list.AddFirst(1);

        Assert.Equal(new[] { 1, 2, 3 }, list);
        Assert.Equal(1, list.First);
        Assert.Equal(3, list.Last);
    }

    [Fact]
    public void RemoveFirst_LastItem_ClearsTail()
    {
        var list = ListOf(1);

        Assert.Equal(1, list.RemoveFirst());
        Assert.Empty(list);
        Assert.Throws<InvalidOperationException>(() => list.Last);

        list.AddLast(9); // would break if the tail still pointed at the old node
        Assert.Equal(new[] { 9 }, list);
    }

    [Fact]
    public void RemoveFirst_Empty_Throws() =>
        Assert.Throws<InvalidOperationException>(() => new SinglyLinkedList<int>().RemoveFirst());

    [Fact]
    public void Contains_FindsValues()
    {
        var list = ListOf(4, 5, 6);

        Assert.Contains(5, list);
        Assert.DoesNotContain(7, list);
    }

    [Fact]
    public void Reverse_ReversesOrder()
    {
        var list = ListOf(1, 2, 3, 4);
        list.Reverse();

        Assert.Equal(new[] { 4, 3, 2, 1 }, list);
    }

    [Fact]
    public void Reverse_SwapsFirstAndLast_AndAddLastStillWorks()
    {
        var list = ListOf(1, 2, 3);
        list.Reverse();

        Assert.Equal(3, list.First);
        Assert.Equal(1, list.Last);
        list.AddLast(0);
        Assert.Equal(new[] { 3, 2, 1, 0 }, list);
    }

    [Fact]
    public void Reverse_EmptyAndSingle_AreUnchanged()
    {
        var empty = ListOf();
        empty.Reverse();
        Assert.Empty(empty);

        var single = ListOf(7);
        single.Reverse();
        Assert.Equal(new[] { 7 }, single);
    }
}
