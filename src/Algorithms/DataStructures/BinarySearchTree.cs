namespace Algorithms.DataStructures;

/// <summary>
/// WORKED EXAMPLE + REFERENCE SOLUTIONS — an unbalanced binary search tree (no duplicates).
/// <see cref="Height"/> and <see cref="Remove"/> are the reference solutions to exercises 5 and 6.
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
    /// REFERENCE SOLUTION — number of nodes on the longest root-to-leaf path.
    /// Empty tree = 0, one node = 1. O(n): every node is visited once.
    /// </summary>
    public int Height() => HeightOf(_root);

    // static: it needs no instance state, and the compiler then guarantees it uses none.
    private static int HeightOf(Node? node) =>
        node is null ? 0 : 1 + Math.Max(HeightOf(node.Left), HeightOf(node.Right));

    /// <summary>
    /// REFERENCE SOLUTION — remove a value; return false if it wasn't there.
    /// O(h), where h is the height: O(log n) when balanced, O(n) for a degenerate tree.
    /// </summary>
    /// <remarks>
    /// The recursive helper returns the new root of each subtree, and the caller re-links it
    /// (<c>node.Left = RemoveFrom(node.Left, ...)</c>). That removes the need to track the
    /// parent and which side we came from — the most common source of bugs in the iterative version.
    /// </remarks>
    public bool Remove(T value)
    {
        var removed = false;
        _root = RemoveFrom(_root, value, ref removed);
        if (removed) Count--;
        return removed;
    }

    private static Node? RemoveFrom(Node? node, T value, ref bool removed)
    {
        if (node is null) return null; // not found

        var cmp = value.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = RemoveFrom(node.Left, value, ref removed);
            return node;
        }
        if (cmp > 0)
        {
            node.Right = RemoveFrom(node.Right, value, ref removed);
            return node;
        }

        removed = true;

        // Cases 1 and 2: zero or one child — the child (or null) takes this node's place.
        if (node.Left is null) return node.Right;
        if (node.Right is null) return node.Left;

        // Case 3: two children. The in-order successor (smallest value in the right subtree)
        // is larger than everything on the left and smaller than everything else on the right,
        // so it can take this node's place. Copy its value up, then delete it from the right
        // subtree — it has no left child, so that delete is always case 1 or 2.
        var successor = node.Right;
        while (successor.Left is not null) successor = successor.Left;
        node.Value = successor.Value;
        var ignored = false;
        node.Right = RemoveFrom(node.Right, successor.Value, ref ignored);
        return node;
    }
}
