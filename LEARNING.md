# Learning guide

Read a worked example, then do the exercises that build on it. Each exercise has an issue, a hint in the code's doc comment, and tests that define "done".

## Workflow for each exercise

1. Pick the next issue and create a branch: `git switch -c exercise/stack`
2. In the test file, delete `Skip = Exercise.Todo` from that exercise's tests.
3. Run `dotnet test` and watch them fail (red).
4. Write the simplest code that passes (green), then tidy it (refactor).
5. Commit, push, and open a pull request with `Closes #<issue>` in the description.
6. Let CI go green, read your own diff once more, then merge.

That loop — branch, red, green, refactor, PR — is the same one used on professional teams.

## Order

| # | Read first | Exercise | Target |
| --- | --- | --- | --- |
| 1 | `DynamicArray` | `ArrayStack` | O(1) push/pop (amortised) |
| 2 | `ArrayStack` | `CircularQueue` | O(1) enqueue/dequeue (amortised) |
| 3 | `SinglyLinkedList` | `SinglyLinkedList.Reverse` | O(n) time, O(1) space |
| 4 | `BinarySearch.IndexOf` | `BinarySearch.LowerBound` | O(log n) |
| 5 | `BinarySearchTree` | `BinarySearchTree.Height` | O(n) |
| 6 | `BinarySearchTree` | `BinarySearchTree.Remove` | O(h) |
| 7 | `Sorts.InsertionSort` | `Sorts.MergeSort` | O(n log n), stable |
| 8 | `Sorts.MergeSort` | `Sorts.QuickSort` | O(n log n) average |
| 9 | `ArrayStack` | `MinHeap` | O(log n) push/pop |
| 10 | `Graph.BreadthFirst` | `Graph.DepthFirst` | O(V + E) |
| 11 | `Graph.BreadthFirst` | `Graph.ShortestPathLength` | O(V + E) |

Stuck for more than 30 minutes? Draw it on paper with 3–4 items, then step through the failing test in the debugger.

## Stretch goals

Open your own issues for these once the core is done:

- Dijkstra's shortest path using your `MinHeap`
- A self-balancing tree (AVL) and a test showing `Height` stays near log₂ n
- Benchmark your sorts against `Array.Sort` with BenchmarkDotNet
- Make `HashMap` implement `IEnumerable<KeyValuePair<TKey, TValue>>`
- Trie for prefix search; union–find for connected components
