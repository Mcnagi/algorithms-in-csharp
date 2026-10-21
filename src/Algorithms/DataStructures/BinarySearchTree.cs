namespace Algorithms.DataStructures;

/// <summary>
/// WORKED EXAMPLE + 2 EXERCISES — an unbalanced binary search tree (no duplicates).
/// Insert, Contains, Min and InOrder are done; <see cref="Height"/> and <see cref="Remove"/> are yours.
/// All operations are O(h), where h is the height: O(log n) when balanced, O(n) for sorted input.
/// </summary>
public sealed class BinarySearchTree<T> where T : IComparable<T>
{
    private sealed class Node(T value)
    {
        public T Value { get; set; } = value;
        public Node? Left { get; set; }
        public Node? Right { get; set; }
    }

    private Node? _root;

    public int Count { get; private set; }

    /// <summary>Returns false if the value was already present.</summary>
    public bool Insert(T value)
    {
        if (_root is null)
        {
            _root = new Node(value);
            Count++;
            return true;
        }

        var node = _root;
        while (true)
        {
            var cmp = value.CompareTo(node.Value);
            if (cmp == 0) return false;

            if (cmp < 0)
            {
                if (node.Left is null) { node.Left = new Node(value); break; }
                node = node.Left;
            }
            else
            {
                if (node.Right is null) { node.Right = new Node(value); break; }
                node = node.Right;
            }
        }
        Count++;
        return true;
    }

    public bool Contains(T value)
    {
        var node = _root;
        while (node is not null)
        {
            var cmp = value.CompareTo(node.Value);
            if (cmp == 0) return true;
            node = cmp < 0 ? node.Left : node.Right;
        }
        return false;
    }

    public T Min()
    {
        var node = _root ?? throw new InvalidOperationException("Tree is empty.");
        while (node.Left is not null) node = node.Left;
        return node.Value;
    }

    /// <summary>Values in ascending order — the reason BSTs are useful. Recursive version.</summary>
    public IEnumerable<T> InOrder()
    {
        var result = new List<T>(Count);
        Walk(_root);
        return result;

        void Walk(Node? node)
        {
            if (node is null) return;
            Walk(node.Left);
            result.Add(node.Value);
            Walk(node.Right);
        }
    }

    /// <summary>
    /// EXERCISE — number of nodes on the longest root-to-leaf path. Empty tree = 0, one node = 1.
    /// Hint: a two-line recursion, the same shape as <see cref="InOrder"/>.
    /// Tests: BinarySearchTreeTests.Height_*
    /// </summary>
    public int Height() => throw new NotImplementedException("Exercise: see LEARNING.md");

    /// <summary>
    /// EXERCISE — remove a value; return false if it wasn't there. Three cases:
    /// (1) leaf: unlink it; (2) one child: replace the node with its child;
    /// (3) two children: copy the in-order successor (smallest value in the right subtree)
    /// into the node, then remove the successor from the right subtree. Remember <c>Count</c>.
    /// Tests: BinarySearchTreeTests.Remove_*
    /// </summary>
    public bool Remove(T value) => throw new NotImplementedException("Exercise: see LEARNING.md");
}
