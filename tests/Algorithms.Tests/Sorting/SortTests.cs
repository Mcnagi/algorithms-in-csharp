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

    [Theory()]
    [MemberData(nameof(Cases))]
    public void MergeSort_Sorts(string name, int[] input)
    {
        _ = name;
        AssertSortsLikeArraySort(Sorts.MergeSort, input);
    }

    [Fact()]
    public void MergeSort_IsStable()
    {
        // Equal keys must keep their original relative order.
        var items = new[] { new Card(2, "a"), new Card(1, "b"), new Card(2, "c"), new Card(1, "d") };

        Sorts.MergeSort(items);

        Assert.Equal("bdac", string.Concat(items.Select(c => c.Tag)));
    }

    [Theory()]
    [MemberData(nameof(Cases))]
    public void QuickSort_Sorts(string name, int[] input)
    {
        _ = name;
        AssertSortsLikeArraySort(Sorts.QuickSort, input);
    }

    [Theory]
    [InlineData("Sorted")]
    [InlineData("Reversed")]
    public void QuickSort_SortedOrReversedInput_StaysNearNLogN(string order)
    {
        // Choosing the last item as pivot splits sorted (or reversed) input into (n-1, 0)
        // every time, which costs n²/2 comparisons and n nested calls. Counting comparisons
        // catches that deterministically, without a slow or stack-overflowing test.
        const int n = 5_000;
        var values = Enumerable.Range(0, n);
        if (order == "Reversed") values = values.Reverse();
        var counter = new ComparisonCounter();
        var items = values.Select(v => new Counted(v, counter)).ToArray();

        Sorts.QuickSort(items);

        Assert.Equal(Enumerable.Range(0, n), items.Select(c => c.Value));
        const long nLogN = 61_440;    // 5,000 × log2(5,000)
        const long limit = 10 * nLogN; // generous headroom; n²/2 would be 12,500,000
        Assert.True(counter.Comparisons <= limit,
            $"{counter.Comparisons:N0} comparisons on {order.ToLowerInvariant()} input; expected about " +
            $"n·log2(n) = {nLogN:N0} (limit {limit:N0}). Hint: pick a better pivot.");
    }

    private sealed record Card(int Rank, string Tag) : IComparable<Card>
    {
        public int CompareTo(Card? other) => Rank.CompareTo(other?.Rank ?? int.MinValue);
    }

    private sealed class ComparisonCounter
    {
        public long Comparisons;
    }

    /// <summary>An int that counts every comparison made on it.</summary>
    private sealed class Counted(int value, ComparisonCounter counter) : IComparable<Counted>
    {
        public int Value { get; } = value;

        public int CompareTo(Counted? other)
        {
            counter.Comparisons++;
            return Value.CompareTo(other!.Value);
        }
    }
}
