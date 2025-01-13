using System.Collections;

namespace AIoTServer.Util.Collections;

public class OrderedSet<T>(IEqualityComparer<T> comparer) : ICollection<T>
{
    private readonly IDictionary<T, LinkedListNode<T>> _mDictionary = new Dictionary<T, LinkedListNode<T>>(comparer);
    private readonly LinkedList<T> _mLinkedList = new();

    public OrderedSet()
        : this(EqualityComparer<T>.Default)
    {
    }

    public int Count => _mDictionary.Count;

    public virtual bool IsReadOnly => _mDictionary.IsReadOnly;

    void ICollection<T>.Add(T item)
    {
        Add(item);
    }

    public void Clear()
    {
        _mLinkedList.Clear();
        _mDictionary.Clear();
    }

    public bool Remove(T item)
    {
        if (item == null) return false;
        var found = _mDictionary.TryGetValue(item, out var node);
        if (!found) return false;
        _mDictionary.Remove(item);
        _mLinkedList.Remove(node);
        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _mLinkedList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public bool Contains(T item)
    {
        return item != null && _mDictionary.ContainsKey(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        _mLinkedList.CopyTo(array, arrayIndex);
    }

    public bool Add(T item)
    {
        if (_mDictionary.ContainsKey(item)) return false;
        var node = _mLinkedList.AddLast(item);
        _mDictionary.Add(item, node);
        return true;
    }
}