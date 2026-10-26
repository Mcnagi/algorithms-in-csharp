using System.Collections;

namespace Algorithms.DataStructures;

/// <summary>
/// WORKED EXAMPLE + REFERENCE SOLUTION — a singly linked list with head and tail pointers.
/// <see cref="Reverse"/> is the reference solution to exercise 3.
/// </summary>
public sealed class SinglyLinkedList<T> : IEnumerable<T>
{
    private sealed class Node(T value)
    {
        public T Value { get; } = value;
        public Node? Next { get; set; }
    }

    private Node? _head;
    private Node? _tail;

    public int Count { get; private set; }

    /// <summary>O(1).</summary>
    public T First => _head is null ? throw new InvalidOperationException("List is empty.") : _head.Value;

    /// <summary>O(1) — the tail pointer saves walking the list.</summary>
    public T Last => _tail is null ? throw new InvalidOperationException("List is empty.") : _tail.Value;

    /// <summary>O(1).</summary>
    public void AddFirst(T value)
    {
        var node = new Node(value) { Next = _head };
        _head = node;
        _tail ??= node;
        Count++;
    }

    /// <summary>O(1).</summary>
    public void AddLast(T value)
    {
        var node = new Node(value);
        if (_tail is null)
        {
            _head = _tail = node;
        }
        else
        {
            _tail.Next = node;
            _tail = node;
        }
        Count++;
    }

    /// <summary>O(1).</summary>
    public T RemoveFirst()
    {
        if (_head is null) throw new InvalidOperationException("List is empty.");
        var value = _head.Value;
        _head = _head.Next;
        if (_head is null) _tail = null;
        Count--;
        return value;
    }

    /// <summary>O(n).</summary>
    public bool Contains(T value)
    {
        var comparer = EqualityComparer<T>.Default;
        for (var node = _head; node is not null; node = node.Next)
        {
            if (comparer.Equals(node.Value, value)) return true;
        }
        return false;
    }

    /// <summary>
    /// REFERENCE SOLUTION — reverse the list in place. O(n) time, O(1) extra space.
    /// </summary>
    /// <remarks>
    /// Three references walk the list: <c>previous</c> (already reversed), <c>current</c>
    /// (being flipped) and <c>next</c> (saved before the flip, or the rest of the list is lost).
    /// No empty-list guard is needed: the loop simply doesn't run, and the swap of two nulls is harmless.
    /// </remarks>
    public void Reverse()
    {
        Node? previous = null;
        var current = _head;
        while (current is not null)
        {
            var next = current.Next;
            current.Next = previous;
            previous = current;
            current = next;
        }
        (_head, _tail) = (_tail, _head); // tuple swap: no temp variable
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var node = _head; node is not null; node = node.Next)
            yield return node.Value;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
