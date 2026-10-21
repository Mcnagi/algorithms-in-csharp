using System.Collections;

namespace Algorithms.DataStructures;

/// <summary>
/// WORKED EXAMPLE + 1 EXERCISE — a singly linked list with head and tail pointers.
/// Everything is implemented except <see cref="Reverse"/>.
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
    /// EXERCISE — reverse the list in place. Target: O(n) time, O(1) extra space.
    /// Hint: walk the list with three references (previous, current, next) and
    /// flip each <c>Next</c> pointer. Don't forget to swap <c>_head</c> and <c>_tail</c>.
    /// Tests: SinglyLinkedListTests.Reverse_*
    /// </summary>
    public void Reverse()
    {
        throw new NotImplementedException("Exercise: see LEARNING.md");
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var node = _head; node is not null; node = node.Next)
            yield return node.Value;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
