using Algorithms.Graphs;

namespace Algorithms.Tests.Graphs;

public class GraphTests
{
    //  0 — 1 — 3
    //  |   |
    //  2 — 4    5   (5 is disconnected)
    private static Graph SampleGraph()
    {
        var g = new Graph(6);
        g.AddEdge(0, 1);
        g.AddEdge(0, 2);
        g.AddEdge(1, 3);
        g.AddEdge(1, 4);
        g.AddEdge(2, 4);
        return g;
    }

    [Fact]
    public void BreadthFirst_VisitsInRingsOfDistance()
    {
        Assert.Equal(new[] { 0, 1, 2, 3, 4 }, SampleGraph().BreadthFirst(0));
    }

    [Fact]
    public void BreadthFirst_Directed_FollowsEdgeDirectionOnly()
    {
        var g = new Graph(3, directed: true);
        g.AddEdge(0, 1);
        g.AddEdge(2, 1);

        Assert.Equal(new[] { 0, 1 }, g.BreadthFirst(0));
        Assert.Equal(new[] { 1 }, g.BreadthFirst(1));
    }

    [Fact]
    public void AddEdge_UnknownVertex_Throws() =>
        Assert.Throws<ArgumentOutOfRangeException>(() => SampleGraph().AddEdge(0, 6));

    [Fact()]
    public void DepthFirst_GoesDeepBeforeWide()
    {
        // 0 → 1 → 3 (dead end) → back to 1 → 4 → 2
        Assert.Equal(new[] { 0, 1, 3, 4, 2 }, SampleGraph().DepthFirst(0));
    }

    [Fact()]
    public void DepthFirst_DisconnectedVertex_VisitsOnlyItself()
    {
        Assert.Equal(new[] { 5 }, SampleGraph().DepthFirst(5));
    }

    [Fact()]
    public void DepthFirst_HandlesCyclesAndLongChains()
    {
        var g = new Graph(5_000);
        for (var i = 0; i < 4_999; i++) g.AddEdge(i, i + 1);
        g.AddEdge(4_999, 0); // one big cycle

        Assert.Equal(Enumerable.Range(0, 5_000), g.DepthFirst(0));
    }

    [Fact]
    public void DepthFirst_ReachesSharedNeighbourThroughTheDeeperPath()
    {
        //   0 — 1
        //   | \ |      0's neighbours, in order: 1, 4, 2
        //   4   2
        // Recursive DFS goes 0 → 1 → 2 (via 1) before backtracking to 4.
        // An iterative DFS that marks vertices visited when PUSHED (not popped) skips 2
        // under 1, because 0 already pushed it, and returns 0, 1, 4, 2 instead.
        var g = new Graph(5);
        g.AddEdge(0, 1);
        g.AddEdge(0, 4);
        g.AddEdge(0, 2);
        g.AddEdge(1, 2);

        Assert.Equal(new[] { 0, 1, 2, 4 }, g.DepthFirst(0));
    }

    [Fact]
    public void DepthFirst_DeepChain_DoesNotOverflowTheStack()
    {
        // 200,000 levels deep: a recursive DFS needs one stack frame per level.
        var g = new Graph(200_000);
        for (var i = 0; i < 199_999; i++) g.AddEdge(i, i + 1);

        Assert.Equal(200_000, g.DepthFirst(0).Count);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    public void DepthFirst_UnknownStart_Throws(int start) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => SampleGraph().DepthFirst(start));

    [Theory]
    [InlineData(0, 6)]
    [InlineData(6, 0)]
    [InlineData(6, 6)]   // from == to must still be validated, not answered with 0
    [InlineData(-1, 0)]
    public void ShortestPathLength_UnknownVertex_Throws(int from, int to) =>
        Assert.Throws<ArgumentOutOfRangeException>(() => SampleGraph().ShortestPathLength(from, to));

    [Theory()]
    [InlineData(0, 0, 0)]
    [InlineData(0, 1, 1)]
    [InlineData(0, 3, 2)]
    [InlineData(3, 2, 3)]
    [InlineData(0, 5, -1)]
    public void ShortestPathLength_CountsFewestEdges(int from, int to, int expected)
    {
        Assert.Equal(expected, SampleGraph().ShortestPathLength(from, to));
    }
}