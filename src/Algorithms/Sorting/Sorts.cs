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
    /// Merge sort. O(n log n) always, stable, O(n) extra space.
    /// </summary>
    public static void MergeSort<T>(T[] items) where T : IComparable<T>
    {
        T[] sorted = new T[items.Length];
        sort(0, items.Length - 1);
        return;

        void sort(int left, int right)
        {
            if (left >= right) return;
            if (left == right - 1)
            {
                if (items[left].CompareTo(items[right]) > 0)
                {
                    T itemLeft = items[left];
                    items[left] = items[right];
                    items[right] = itemLeft;
                }
            }
            else
            {
                // divide into two lists
                int mid = left + (right - left) / 2;
                sort(left, mid);
                sort(mid + 1, right);
                merge(left, mid, right);
            }
        }

        void merge(int lo, int mid, int hi)
        {
            int i = lo, j = mid + 1, k = lo;
            while (i <= mid || j <= hi)
            {
                if (i > mid) sorted[k++] = items[j++];
                else if (j > hi) sorted[k++] = items[i++];
                else
                {
                    int comp = items[i].CompareTo(items[j]);
                    if (comp <= 0) sorted[k++] = items[i++];
                    else sorted[k++] = items[j++];
                }
            }
            Array.Copy(sorted, lo, items, lo, hi - lo + 1);
        }
    }

    /// <summary>
    /// Quicksort. O(n log n) on average, O(n²) worst case, not stable, O(log n) stack.
    /// Used Lomuto partition (last item as pivot). 
    /// </summary>
    public static void QuickSort<T>(T[] items) where T : IComparable<T>
    {
        SortLomuto(0, items.Length);
        
        void SortLomuto(int from, int to)
        {
            // Do nothing when only one item.
            if (from + 1 >= to) return;
            
            T pivot = items[to - 1];
            int j = from;
            for (int i = from; i < to - 1; i++)
            {
                if (items[i].CompareTo(pivot) <= 0)
                    Swap(i, j++);
            }
            Swap(to - 1, j);
            SortLomuto(from, j);
            SortLomuto(j + 1, to);
        }

        void Swap(int a, int b)
        {
            T itemA = items[a];
            items[a] = items[b];
            items[b] = itemA;
        }
    }
}
