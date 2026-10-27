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
    /// Number of nodes on the longest root-to-leaf path. Empty tree = 0, one node = 1.
    /// </summary>
    public int Height()
    {
        return Helper(_root);
        static int Helper(Node? node)
        {
            return node is null ? 0 : Math.Max(Helper(node.Left) + 1, Helper(node.Right) + 1);
        }
    }

    /// <summary>
    /// Remove a value; return false if it wasn't there. O(log(n)).
    /// </summary>
    public bool Remove(T value)
    {
        Node? node = _root;
        Node? parent = null;
        // O(log(h)) to find the node.
        while (node is not null)
        {
            int comp = value.CompareTo(node.Value);
            if (comp == 0)
            {
                RemoveNode();
                Count--;
                return true;
            }
            parent = node;
            node = comp < 0 ? node.Left : node.Right;
        }
        return false;

        // O(log(nh)) to find replacement node and O(1) to replace.
        void RemoveNode()
        {
            Node? newNode = null;
            if (node.Left is not null)
            {
                if (node.Right is not null)
                {
                    newNode = node.Right;
                    Node? prev = null;

                    // Traverse down to the samllest leaf of right child.
                    // Replace smallest leaf with its left child if any.
                    // Put smallest leaf in position of current node.
                    while (newNode.Left is not null)
                    {
                        prev = newNode;
                        newNode = newNode.Left;
                    }
                    if (prev is not null)
                    {
                        prev.Left = newNode.Right;
                    }
                    newNode.Left = node.Left;
                    if (newNode != node.Right) newNode.Right = node.Right;
                }
                else newNode = node.Left;
            }
            else if (node.Right is not null) newNode = node.Right;

            if (parent is null) _root = newNode;
            else if (node.Value.CompareTo(parent.Value) < 0) parent.Left = newNode;
            else parent.Right = newNode;
        }
    }
}