using Algorithms.DataStructures;

namespace Algorithms.Tests.DataStructures;

public class DynamicArrayTests
{
    [Fact]
    public void Add_GrowsPastInitialCapacity()
    {
        var array = new DynamicArray<int>(capacity: 2);
        for (var i = 0; i < 10; i++) array.Add(i);

        Assert.Equal(10, array.Count);
        Assert.True(array.Capacity >= 10);
        Assert.Equal(Enumerable.Range(0, 10), array);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(3)]
    public void Indexer_OutOfRange_Throws(int index)
    {
        var array = new DynamicArray<string>();
        array.Add("a"); array.Add("b"); array.Add("c");

        Assert.Throws<ArgumentOutOfRangeException>(() => array[index]);
    }

    [Fact]
    public void Insert_ShiftsLaterItemsRight()
    {
        var array = new DynamicArray<char>();
        array.Add('a'); array.Add('c');

        array.Insert(1, 'b');
        array.Insert(3, 'd'); // inserting at Count appends

        Assert.Equal("abcd", string.Concat(array));
    }

    [Fact]
    public void RemoveAt_ShiftsLaterItemsLeft()
    {
        var array = new DynamicArray<int>();
        foreach (var n in new[] { 1, 2, 3, 4 }) array.Add(n);

        array.RemoveAt(1);

        Assert.Equal(new[] { 1, 3, 4 }, array);
        Assert.Equal(3, array.Count);
    }

    [Fact]
    public void IndexOf_ReturnsFirstMatchOrMinusOne()
    {
        var array = new DynamicArray<int>();
        foreach (var n in new[] { 5, 7, 5 }) array.Add(n);

        Assert.Equal(0, array.IndexOf(5));
        Assert.Equal(-1, array.IndexOf(9));
    }

    [Fact]
    public void Enumerating_WhileModifying_Throws()
    {
        var array = new DynamicArray<int>();
        array.Add(1); array.Add(2);

        Assert.Throws<InvalidOperationException>(() =>
        {
            foreach (var n in array) array.Add(n);
        });
    }
}
