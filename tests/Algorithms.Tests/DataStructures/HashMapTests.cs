using Algorithms.DataStructures;

namespace Algorithms.Tests.DataStructures;

public class HashMapTests
{
    [Fact]
    public void Put_ThenTryGetValue_ReturnsValue()
    {
        var map = new HashMap<string, int>();
        map.Put("one", 1);
        map.Put("two", 2);

        Assert.True(map.TryGetValue("two", out var value));
        Assert.Equal(2, value);
        Assert.False(map.TryGetValue("three", out _));
    }

    [Fact]
    public void Put_ExistingKey_OverwritesWithoutGrowingCount()
    {
        var map = new HashMap<string, int>();
        map.Put("k", 1);
        map.Put("k", 2);

        Assert.Equal(1, map.Count);
        Assert.True(map.TryGetValue("k", out var value));
        Assert.Equal(2, value);
    }

    [Fact]
    public void Remove_DeletesKey()
    {
        var map = new HashMap<int, string>();
        map.Put(1, "a");

        Assert.True(map.Remove(1));
        Assert.False(map.Remove(1));
        Assert.False(map.ContainsKey(1));
        Assert.Equal(0, map.Count);
    }

    [Fact]
    public void Put_ManyKeys_ResizesAndKeepsEverything()
    {
        var map = new HashMap<int, int>(capacity: 4);
        for (var i = 0; i < 1_000; i++) map.Put(i, i * i);

        Assert.True(map.BucketCount > 4);
        Assert.Equal(1_000, map.Count);
        for (var i = 0; i < 1_000; i++)
        {
            Assert.True(map.TryGetValue(i, out var v));
            Assert.Equal(i * i, v);
        }
    }

    [Fact]
    public void CollidingKeys_AreStillDistinct()
    {
        var map = new HashMap<string, int>(comparer: new AlwaysCollides());
        map.Put("a", 1);
        map.Put("b", 2);
        map.Remove("a");

        Assert.False(map.ContainsKey("a"));
        Assert.True(map.TryGetValue("b", out var v));
        Assert.Equal(2, v);
    }

    /// <summary>Forces every key into one bucket to test chaining.</summary>
    private sealed class AlwaysCollides : IEqualityComparer<string>
    {
        public bool Equals(string? x, string? y) => x == y;
        public int GetHashCode(string obj) => 42;
    }
}