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
    /// EXERCISE — merge sort. O(n log n) always, stable, O(n) extra space.
    /// Hint: allocate one temp buffer up front and pass it down the recursion
    /// rather than allocating in every merge. Use &lt;= when comparing so equal items keep order.
    /// Tests: SortTests (MergeSort cases)
    /// </summary>
    public static void MergeSort<T>(T[] items) where T : IComparable<T> =>
        throw new NotImplementedException("Exercise: see LEARNING.md");

    /// <summary>
    /// EXERCISE — quicksort. O(n log n) on average, O(n²) worst case, not stable, O(log n) stack.
    /// Hint: start with the Lomuto partition (last item as pivot). Then try a random or
    /// median-of-three pivot and see the AlreadySorted test case get much faster.
    /// Tests: SortTests (QuickSort cases)
    /// </summary>
    public static void QuickSort<T>(T[] items) where T : IComparable<T> =>
        throw new NotImplementedException("Exercise: see LEARNING.md");
}
