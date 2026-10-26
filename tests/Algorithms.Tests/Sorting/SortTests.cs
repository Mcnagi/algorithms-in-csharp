using Algorithms.Sorting;

namespace Algorithms.Tests.Sorting;

public class SortTests
{
    public static TheoryData<string, int[]> Cases()
    {
        var random = new Random(42);
        return new()
        {
            { "Empty", [] },
            { "Single", [7] },
            { "AlreadySorted", Enumerable.Range(0, 3_000).ToArray() },
            { "Reversed", Enumerable.Range(0, 500).Reverse().ToArray() },
            { "Duplicates", [3, 1, 3, 2, 1, 3, 2] },
            { "Negatives", [0, -5, 12, -1, 7, -5] },
            { "Random", Enumerable.Range(0, 2_000).Select(_ => random.Next(-10_000, 10_000)).ToArray() },
        };
    }

    private static void AssertSortsLikeArraySort(Action<int[]> sort, int[] input)
    {
        var actual = (int[])input.Clone();
        var expected = (int[])input.Clone();
        Array.Sort(expected);

        sort(actual);

        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(Cases))]
    public void InsertionSort_Sorts(string name, int[] input)
    {
        _ = name; // shown in the test runner so you can see which case failed
        AssertSortsLikeArraySort(Sorts.InsertionSort, input);
    }

    [Theory]
    [MemberData(nameof(Cases))]
    public void MergeSort_Sorts(string name, int[] input)
    {
        _ = name;
        AssertSortsLikeArraySort(Sorts.MergeSort, input);
    }

    [Fact]
    public void MergeSort_IsStable()
    {
        // Equal keys must keep their original relative order.
        var items = new[] { new Card(2, "a"), new Card(1, "b"), new Card(2, "c"), new Card(1, "d") };

        Sorts.MergeSort(items);

        Assert.Equal("bdac", string.Concat(items.Select(c => c.Tag)));
    }

    [Theory]
    [MemberData(nameof(Cases))]
    public void QuickSort_Sorts(string name, int[] input)
    {
        _ = name;
        AssertSortsLikeArraySort(Sorts.QuickSort, input);
    }

    [Fact]
    public void QuickSort_LargeSortedInput_Sorts()
    {
        // With the last item as pivot, sorted input splits n into (n-1, 0) every time:
        // ~5 billion comparisons and 100,000 nested calls. Measured: ~6.5 s instead of ~6 ms,
        // and on a thread with a 1 MB stack it overflows and crashes the process (a
        // StackOverflowException can't be caught). Median-of-three plus "recurse on the
        // smaller side" fixes both.
        var input = Enumerable.Range(0, 100_000).ToArray();
        AssertSortsLikeArraySort(Sorts.QuickSort, input);
    }

    private sealed record Card(int Rank, string Tag) : IComparable<Card>
    {
        public int CompareTo(Card? other) => Rank.CompareTo(other?.Rank ?? int.MinValue);
    }
}
