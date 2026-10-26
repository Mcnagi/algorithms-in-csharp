# Reference solutions

> **This is the `solutions` branch. Never merge it into `practice`.**
> It holds a reference answer for all 11 exercises, every test un-skipped, plus extra tests
> that catch bugs the original tests miss.

## How to use it

1. **Attempt the exercise first.** If you're stuck for 30 minutes, read only the `<remarks>` on that method, then close it and try again.
2. **After your PR is green**, compare your version with the reference:

   ```bash
   git fetch origin
   git diff origin/practice origin/solutions -- src/Algorithms/Sorting/Sorts.cs
   ```

   Or on GitHub: **Compare → base `practice` ← compare `solutions`**.
3. **Copy the extra tests** (below) into your branch and see whether your version passes them.
4. A different solution isn't a wrong one. The reference optimises for clarity; look for ideas, not for a match.

## Extra tests on this branch

| Test | Catches |
| --- | --- |
| `GraphTests.DepthFirst_ReachesSharedNeighbourThroughTheDeeperPath` | An iterative DFS that marks vertices visited when *pushed* returns the wrong order |
| `GraphTests.DepthFirst_DeepChain_DoesNotOverflowTheStack` | A recursive DFS on a 200,000-vertex chain |
| `SortTests.QuickSort_LargeSortedInput_Sorts` | Last-item pivot on sorted input: O(n²), and a stack overflow on a 1 MB stack |

## Review of your code (as of `practice` @ ab51847 and `dfs` @ f9c60c3)

All merged exercises are correct and pass the original tests. Most important first.

| # | Exercise | Verdict | Notes |
| --- | --- | --- | --- |
| 10 | DepthFirst (`dfs` branch) | **Bug — wrong order** | `DepthFirstWithStack` marks vertices visited when pushed. In the graph from the new test it returns `0, 1, 4, 2` instead of `0, 1, 2, 4`. Mark on **pop** instead, and skip already-visited vertices when popping. Also: it skips `CheckVertex(start)`, and `DepthFirstWithRecursive` is now dead code — keep one version. |
| 8 | QuickSort | Correct, but slow on sorted input | 100,000 sorted items: **6.5 s** (reference: 6 ms), and a stack overflow on a 1 MB stack. The doc comment's "O(log n) stack" is untrue for last-item pivot — it's O(n). Fix: median-of-three pivot + recurse into the smaller side. |
| 6 | Remove | Correct | **O(h), not O(log n)** — your own `Height_SortedInsertsDegradeToAList` test shows h can be n. Choosing the parent's side by comparing values only works because the tree has no duplicates. The recursive "return the new subtree root" pattern avoids tracking the parent at all. |
| 9 | MinHeap | Correct | `if (p < 0) break;` never fires: in C#, `(0 - 1) / 2` is **0**, not −1 (integer division truncates toward zero). The loop only stops at the root because the item compares equal to itself. Write `while (i > 0)` instead. |
| 7 | MergeSort | Correct and stable | The two-item special case in `sort` isn't needed — the general path handles it. Skipping the merge when `items[mid] <= items[mid + 1]` makes sorted input O(n). |
| 1 | ArrayStack | Correct | `Pop`/`Peek` check emptiness twice (`if (IsEmpty) ThrowIfEmpty()`). `_size` duplicates `Count`. |
| 2 | CircularQueue | Correct, incl. wrap-then-grow | Class doc still says "EXERCISE". `Grow` can use two `Array.Copy` calls instead of a per-item modulo. |
| 3 | Reverse | Correct | The empty-list guard isn't needed. `(_head, _tail) = (_tail, _head)` swaps without a temp. |
| 4 | LowerBound | Correct — same algorithm | The reference's remark shows the *invariant* that makes ±1 errors impossible. |
| 5 | Height | Correct | `1 + Math.Max(a, b)` reads more directly than `Math.Max(a + 1, b + 1)`. |

## Key idea for exercise 11

Read only when stuck — the code is the full answer.

- **ShortestPathLength** — BFS with an `int[] distance` (−1 = unvisited) instead of `bool[] visited`; return as soon as you dequeue the target.
