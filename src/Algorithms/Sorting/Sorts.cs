namespace Algorithms.Sorting;

/// <summary>All sorts work in place on the array and sort ascending.</summary>
public static class Sorts
{
    /// <summary>
    /// WORKED EXAMPLE — insertion sort. O(n²) worst case, O(n) on nearly-sorted input, stable.
    /// Real libraries switch to it for small slices because it has tiny overhead.
    /// </summary>
    public static void InsertionSort<T>(T[] items) where T : IComparable<T>
    {
        ArgumentNullException.ThrowIfNull(items);
        for (var i = 1; i < items.Length; i++)
        {
            var current = items[i];
            var j = i - 1;
            while (j >= 0 && items[j].CompareTo(current) > 0)
            {
                items[j + 1] = items[j]; // shift bigger items right
                j--;
            }
            items[j + 1] = current;
        }
    }

    /// <summary>
    /// REFERENCE SOLUTION — top-down merge sort. O(n log n) always, stable, O(n) extra space.
    /// </summary>
    public static void MergeSort<T>(T[] items) where T : IComparable<T>
    {
        ArgumentNullException.ThrowIfNull(items);
        var buffer = new T[items.Length]; // one allocation, reused by every merge
        SortRange(items, buffer, 0, items.Length);
    }

    /// <summary>Sort the half-open range [lo, hi).</summary>
    private static void SortRange<T>(T[] items, T[] buffer, int lo, int hi) where T : IComparable<T>
    {
        if (hi - lo < 2) return; // 0 or 1 items: already sorted
        var mid = lo + (hi - lo) / 2;
        SortRange(items, buffer, lo, mid);
        SortRange(items, buffer, mid, hi);

        // Both halves are sorted. If the last of the left ≤ first of the right, nothing to merge —
        // this one check makes already-sorted input O(n).
        if (items[mid - 1].CompareTo(items[mid]) <= 0) return;

        Merge(items, buffer, lo, mid, hi);
    }

    private static void Merge<T>(T[] items, T[] buffer, int lo, int mid, int hi) where T : IComparable<T>
    {
        int left = lo, right = mid, output = lo;
        while (left < mid && right < hi)
        {
            // <= takes from the LEFT on ties. That keeps equal items in their original
            // order, which is exactly what "stable" means. With < the sort is not stable.
            buffer[output++] = items[left].CompareTo(items[right]) <= 0 ? items[left++] : items[right++];
        }
        while (left < mid) buffer[output++] = items[left++];
        while (right < hi) buffer[output++] = items[right++];
        Array.Copy(buffer, lo, items, lo, hi - lo);
    }

    /// <summary>
    /// REFERENCE SOLUTION — quicksort with a median-of-three pivot and Lomuto partition.
    /// O(n log n) on average, O(n²) worst case, not stable, O(log n) stack in the worst case.
    /// </summary>
    /// <remarks>
    /// Median-of-three handles sorted and reversed input. It does NOT help with many equal keys:
    /// Lomuto puts every item equal to the pivot on one side, so an all-equal array is still O(n²).
    /// The fix is a three-way ("Dutch national flag") partition — a good stretch goal.
    /// </remarks>
    public static void QuickSort<T>(T[] items) where T : IComparable<T>
    {
        ArgumentNullException.ThrowIfNull(items);
        SortSpan(items.AsSpan());
    }

    /// <summary>
    /// Sorts a span in place. A <see cref="Span{T}"/> is a window onto the array, so
    /// slicing it (<c>s[..p]</c>) hands a sub-range to the recursion without copying anything.
    /// </summary>
    private static void SortSpan<T>(Span<T> s) where T : IComparable<T>
    {
        // Recurse into the SMALLER side and loop on the larger one. The recursion depth is then
        // at most log2(n) even when partitions are lopsided, so the stack can't overflow.
        while (s.Length > 1)
        {
            var p = Partition(s);
            var left = s[..p];
            var right = s[(p + 1)..];
            if (left.Length < right.Length)
            {
                SortSpan(left);
                s = right;
            }
            else
            {
                SortSpan(right);
                s = left;
            }
        }
    }

    /// <summary>
    /// Lomuto partition of the whole span. Returns the pivot's final index: everything left of it
    /// is smaller, everything right of it is greater or equal.
    /// </summary>
    private static int Partition<T>(Span<T> s) where T : IComparable<T>
    {
        int lo = 0, hi = s.Length - 1;
        // Median-of-three: sort s[lo], s[mid], s[hi], then use the median as the pivot.
        // On sorted or reversed input the last item alone would be the max or min every time,
        // giving O(n²) and n-deep recursion. The median avoids that.
        var mid = lo + (hi - lo) / 2;
        if (s[mid].CompareTo(s[lo]) < 0) (s[mid], s[lo]) = (s[lo], s[mid]);
        if (s[hi].CompareTo(s[lo]) < 0) (s[hi], s[lo]) = (s[lo], s[hi]);
        if (s[hi].CompareTo(s[mid]) < 0) (s[hi], s[mid]) = (s[mid], s[hi]);
        (s[mid], s[hi]) = (s[hi], s[mid]); // move the median to the end for Lomuto

        var pivot = s[hi];
        var store = lo;
        for (var i = lo; i < hi; i++)
        {
            if (s[i].CompareTo(pivot) < 0)
            {
                (s[i], s[store]) = (s[store], s[i]);
                store++;
            }
        }
        (s[store], s[hi]) = (s[hi], s[store]);
        return store;
    }
}
