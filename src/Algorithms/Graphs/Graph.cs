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
    /// Visit order of a recursive depth-first search from <paramref name="start"/>. O(V + E).
    /// Visit a vertex, then recurse into each unvisited neighbour in adjacency order.
    /// </summary>
    public IReadOnlyList<int> DepthFirst(int start)
    {
        return DepthFirstWithStack(start);
    }

    /// <summary>
    /// Recursively search the neighbours of the start node.
    /// </summary>
    /// <param name="start">Start index of the search.</param>
    /// <returns>Order of a depth frist search from start.</returns>
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

    /// <summary>
    /// Use stack to achieve depth first search.
    /// </summary>
    /// <param name="start">Start index of the search.</param>
    /// <returns>Order of a depth frist search from start. The same result
    /// using recursive search. </returns>
    private IReadOnlyList<int> DepthFirstWithStack(int start)
    {
        CheckVertex(start);
        List<int> order = new List<int>();
        Stack<int> s = new Stack<int>();
        bool[] visited = new bool[_adjacency.Length];
        s.Push(start);
        while (s.Count > 0)
        {
            int current = s.Pop();
            if (visited[current]) continue;
            visited[current] = true;
            order.Add(current);
            for (int i = _adjacency[current].Count - 1; i >= 0; i--)
            {
                int n = _adjacency[current][i];
                CheckVertex(n);
                if (visited[n]) continue;
                s.Push(n);
            }
        }
        return order;
    }

    /// <summary>
    /// Fewest edges from <paramref name="from"/> to <paramref name="to"/>, or -1 if unreachable.
    /// O(V + E).
    /// </summary>
    public int ShortestPathLength(int from, int to)
    {
        CheckVertex(from);
        CheckVertex(to);
        if (from == to) return 0; 
        int[] visited = new int[_adjacency.Length];
        Queue<int> q = new();
        q.Enqueue(from);
        visited[from] = 1;
        while (q.Count > 0)
        {
            int i = q.Dequeue();
            int edges = visited[i] + 1;
            foreach (int n in _adjacency[i])
            {
                if (visited[n] != 0) continue;
                if (n == to) return visited[i];
                visited[n] = edges;
                q.Enqueue(n);
            }
        }
        return -1;
    }

    private void CheckVertex(int v)
    {
        if ((uint)v >= (uint)VertexCount)
            throw new ArgumentOutOfRangeException(nameof(v), v, $"Vertex must be in [0, {VertexCount}).");
    }
}