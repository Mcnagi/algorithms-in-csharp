namespace Algorithms.Graphs;

/// <summary>
/// WORKED EXAMPLE + 2 EXERCISES — a graph over vertices 0..n-1 stored as adjacency lists.
/// Breadth-first search is done; depth-first search and shortest path are yours.
/// Neighbours are visited in the order edges were added, so traversal order is predictable.
/// </summary>
public sealed class Graph
{
    private readonly List<int>[] _adjacency;

    public Graph(int vertexCount, bool directed = false)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(vertexCount);
        _adjacency = new List<int>[vertexCount];
        for (var i = 0; i < vertexCount; i++) _adjacency[i] = [];
        IsDirected = directed;
    }

    public int VertexCount => _adjacency.Length;
    public bool IsDirected { get; }

    public void AddEdge(int from, int to)
    {
        CheckVertex(from);
        CheckVertex(to);
        _adjacency[from].Add(to);
        if (!IsDirected && from != to) _adjacency[to].Add(from);
    }

    public IReadOnlyList<int> Neighbours(int vertex)
    {
        CheckVertex(vertex);
        return _adjacency[vertex];
    }

    /// <summary>Visit order of a breadth-first search from <paramref name="start"/>. O(V + E).</summary>
    public IReadOnlyList<int> BreadthFirst(int start)
    {
        CheckVertex(start);
        var order = new List<int>();
        var visited = new bool[VertexCount];
        var queue = new Queue<int>();

        visited[start] = true; // mark on enqueue, not dequeue, so nothing is queued twice
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var v = queue.Dequeue();
            order.Add(v);
            foreach (var n in _adjacency[v])
            {
                if (visited[n]) continue;
                visited[n] = true;
                queue.Enqueue(n);
            }
        }
        return order;
    }

    /// <summary>
    /// EXERCISE — visit order of a recursive depth-first search from <paramref name="start"/>. O(V + E).
    /// Visit a vertex, then recurse into each unvisited neighbour in adjacency order.
    /// Stretch goal: rewrite it with an explicit <see cref="Stack{T}"/> and keep the same order.
    /// Tests: GraphTests.DepthFirst_*
    /// </summary>
    public IReadOnlyList<int> DepthFirst(int start)
    {
        return DepthFirstWithStack(start);
    }

    private IReadOnlyList<int> DepthFirstWithRecursive(int start)
    {
        CheckVertex(start);
        var order = new List<int>();
        var visited = new bool[VertexCount];
        Visit(start);
        
        void Visit(int current)
        {
            order.Add(current);
            visited[current] = true;
            foreach (var n in _adjacency[current])
            {
                if (visited[n]) continue;
                Visit(n);
            }
        }
        return order;
    }

    private IReadOnlyList<int> DepthFirstWithStack(int start)
    {
        List<int> order = new List<int>();
        Stack<int> s = new Stack<int>();
        bool[] visited = new bool[_adjacency.Length];
        s.Push(start);
        visited[start] = true;
        while(s.Count > 0)
        {
            int current = s.Pop();
            order.Add(current);
            for (int i = _adjacency[current].Count - 1; i >= 0; i--)
            {
                int n = _adjacency[current][i];
                if (visited[n]) continue;
                visited[n] = true;
                s.Push(n);
            }
        }
        return order;
    }

    /// <summary>
    /// EXERCISE — fewest edges from <paramref name="from"/> to <paramref name="to"/>, or -1 if unreachable.
    /// Hint: BFS already explores in rings of increasing distance; record each vertex's distance
    /// instead of a bool.
    /// Tests: GraphTests.ShortestPathLength_*
    /// </summary>
    public int ShortestPathLength(int from, int to) => throw new NotImplementedException("Exercise: see LEARNING.md");

    private void CheckVertex(int v)
    {
        if ((uint)v >= (uint)VertexCount)
            throw new ArgumentOutOfRangeException(nameof(v), v, $"Vertex must be in [0, {VertexCount}).");
    }
}
