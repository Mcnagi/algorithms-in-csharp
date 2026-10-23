using Algorithms.Searching;

namespace Algorithms.Tests.Searching;

public class BinarySearchTests
{
    private static readonly int[] Sorted = [1, 3, 5, 7, 9, 11];

    [Theory]
    [InlineData(1, 0)]
    [InlineData(7, 3)]
    [InlineData(11, 5)]
    [InlineData(4, -1)]
    [InlineData(0, -1)]
    [InlineData(12, -1)]
    public void IndexOf_FindsTargetOrMinusOne(int target, int expected)
    {
        Assert.Equal(expected, BinarySearch.IndexOf(Sorted, target));
    }

    [Fact]
    public void IndexOf_Empty_ReturnsMinusOne()
    {
        Assert.Equal(-1, BinarySearch.IndexOf(Array.Empty<int>(), 1));
    }

    [Theory()]
    [InlineData(2, 1)]  // first of three 2s
    [InlineData(3, 4)]  // not present: where it would be inserted
    [InlineData(0, 0)]
    [InlineData(9, 6)]  // bigger than everything: the count
    public void LowerBound_FindsFirstIndexNotLessThanTarget(int target, int expected)
    {
        int[] withDuplicates = [1, 2, 2, 2, 5, 8];
        Assert.Equal(expected, BinarySearch.LowerBound(withDuplicates, target));
    }

    [Fact()]
    public void LowerBound_Empty_ReturnsZero()
    {
        Assert.Equal(0, BinarySearch.LowerBound(Array.Empty<int>(), 1));
    }
}
