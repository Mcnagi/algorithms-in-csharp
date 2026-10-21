# algorithms-in-csharp

[![CI](https://github.com/mcnagi/algorithms-in-csharp/actions/workflows/ci.yml/badge.svg)](https://github.com/mcnagi/algorithms-in-csharp/actions/workflows/ci.yml)

Core data structures and algorithms in C# 14 / .NET 10, written from scratch with unit tests.
Part worked examples, part exercises: each exercise has failing tests waiting for an implementation.

## What's here

| Topic | File | Status |
| --- | --- | --- |
| Dynamic array | `DataStructures/DynamicArray.cs` | Worked example |
| Hash map (separate chaining) | `DataStructures/HashMap.cs` | Worked example |
| Singly linked list | `DataStructures/SinglyLinkedList.cs` | Example + `Reverse` exercise |
| Binary search tree | `DataStructures/BinarySearchTree.cs` | Example + `Height`, `Remove` exercises |
| Graph (adjacency list) | `Graphs/Graph.cs` | BFS example + DFS, shortest-path exercises |
| Binary search | `Searching/BinarySearch.cs` | Example + `LowerBound` exercise |
| Sorting | `Sorting/Sorts.cs` | Insertion sort example + merge sort, quicksort exercises |
| Stack | `DataStructures/ArrayStack.cs` | Exercise |
| Queue (ring buffer) | `DataStructures/CircularQueue.cs` | Exercise |
| Min-heap | `DataStructures/MinHeap.cs` | Exercise |

Progress is tracked in [Issues](../../issues) — one per exercise.

## Run it

Needs the [.NET 10 SDK](https://dotnet.microsoft.com/download).

```bash
git clone https://github.com/mcnagi/algorithms-in-csharp.git
cd algorithms-in-csharp
dotnet test
```

Exercise tests show as **skipped** until you start them. See [LEARNING.md](LEARNING.md) for the order and the workflow.

## Layout

```
src/Algorithms/          the library
tests/Algorithms.Tests/  xUnit tests, mirroring the src folders
```

## Licence

[MIT](LICENSE)
