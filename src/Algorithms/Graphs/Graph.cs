namespace Algorithms.Graphs;

/// <summary>
/// WORKED EXAMPLE + REFERENCE SOLUTIONS — a graph over vertices 0..n-1 stored as adjacency lists.
/// DepthFirst and ShortestPathLength are the reference solutions to exercises 10 and 11.
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
    /// REFERENCE SOLUTION — visit order of a depth-first search from <paramref name="start"/>. O(V + E).
    /// </summary>
    /// <remarks>
    /// Iterative, with an explicit <see cref="Stack{T}"/>, so a 100,000-vertex chain can't overflow the
    /// call stack (recursion uses one stack frame per level of depth). It produces the same order as
    /// the recursive version because of two details:
    /// <list type="number">
    /// <item>Neighbours are pushed in REVERSE, so the first neighbour is popped first.</item>
    /// <item>Vertices are marked visited when POPPED, not when pushed. A vertex can sit on the stack
    /// twice; the second copy is skipped. (BFS marks on enqueue instead — DFS can't, or the order changes.)</item>
    /// </list>
    /// </remarks>
    public IReadOnlyList<int> DepthFirst(int start)
    {
        CheckVertex(start);
        var order = new List<int>();
        var visited = new bool[VertexCount];
        var stack = new Stack<int>();
        stack.Push(start);

        while (stack.Count > 0)
        {
            var v = stack.Pop();
            if (visited[v]) continue;
            visited[v] = true;
            order.Add(v);

            var neighbours = _adjacency[v];
            for (var i = neighbours.Count - 1; i >= 0; i--)
            {
                if (!visited[neighbours[i]]) stack.Push(neighbours[i]);
            }
        }
        return order;
    }

    /// <summary>
    /// REFERENCE SOLUTION — fewest edges from <paramref name="from"/> to <paramref name="to"/>,
    /// or -1 if unreachable. O(V + E).
    /// </summary>
    /// <remarks>
    /// BFS dequeues vertices in order of distance, so the first time it reaches a vertex is along
    /// a shortest path. The <c>distance</c> array does double duty: -1 means "not visited yet".
    /// This only works for unweighted edges; with weights you need Dijkstra (a stretch goal).
    /// </remarks>
    public int ShortestPathLength(int from, int to)
    {
        CheckVertex(from);
        CheckVertex(to);

        var distance = new int[VertexCount];
        Array.Fill(distance, -1);
        distance[from] = 0;
        var queue = new Queue<int>();
        queue.Enqueue(from);

        while (queue.Count > 0)
        {
            var v = queue.Dequeue();
            if (v == to) return distance[v]; // stop early: no need to explore the rest
            foreach (var n in _adjacency[v])
            {
                if (distance[n] != -1) continue;
                distance[n] = distance[v] + 1;
                queue.Enqueue(n);
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
