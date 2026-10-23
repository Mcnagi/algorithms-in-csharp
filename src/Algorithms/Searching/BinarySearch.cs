namespace Algorithms.Searching;

public static class BinarySearch
{
    /// <summary>
    /// WORKED EXAMPLE — index of <paramref name="target"/> in an ascending array, or -1. O(log n).
    /// Uses a half-open range [lo, hi) — the convention that makes off-by-one bugs rarer.
    /// </summary>
    public static int IndexOf<T>(IReadOnlyList<T> sorted, T target) where T : IComparable<T>
    {
        ArgumentNullException.ThrowIfNull(sorted);
        int lo = 0, hi = sorted.Count;
        while (lo < hi)
        {
            var mid = lo + (hi - lo) / 2; // not (lo + hi) / 2, which can overflow on huge arrays
            var cmp = sorted[mid].CompareTo(target);
            if (cmp == 0) return mid;
            if (cmp < 0) lo = mid + 1;
            else hi = mid;
        }
        return -1;
    }

    /// <summary>
    /// EXERCISE — the first index whose value is &gt;= <paramref name="target"/>
    /// (equal to the count if every value is smaller). O(log n).
    /// With duplicates this finds the FIRST occurrence, which <see cref="IndexOf{T}"/> doesn't promise.
    /// Hint: same loop as IndexOf, but never return early.
    /// Tests: BinarySearchTests.LowerBound_*
    /// </summary>
    public static int LowerBound<T>(IReadOnlyList<T> sorted, T target) where T : IComparable<T>
    {
        ArgumentNullException.ThrowIfNull(sorted);
        int lo = 0, hi = sorted.Count;
        while (lo < hi)
        {
            int mid = lo + (hi - lo) / 2; 
            var cmp = sorted[mid].CompareTo(target);
            if (cmp < 0) lo = mid + 1;
            else hi = mid;             
        }
       return lo;
    }
}
