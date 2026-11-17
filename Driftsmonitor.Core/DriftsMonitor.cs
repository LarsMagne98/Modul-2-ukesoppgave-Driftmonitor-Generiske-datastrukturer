using System.Collections;
namespace Driftsmonitor.Core;

public record LogEntry(DateTime Time, string Level, string Message);
public record SensorReading(string SensorId, double Value, DateTime Time);
public record RequestLog(string Endpoint, int DurationMs, int StatusCode);

public class RingBuffer<T> : IEnumerable<T>
{
    private readonly T[] _items;
    private int _start;
    private int _count;

    public RingBuffer(int capacity)
    {
        if (capacity < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(capacity));
        }

        _items = new T[capacity];
    }

    public int Capacity => _items.Length;
    public int Count => _count;
    public bool IsFull => _count == _items.Length;

    public void Add(T item)
    {
        if (IsFull)
        {
            _items[_start] = item;
            _start = (_start + 1) % Capacity;
        }
        else
        {
            int index = (_start + _count) % Capacity;
            _items[index] = item;
            _count++;
        }
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= _count)
            {
                throw new NotImplementedException();
            }

            return _items[(_start + index) % Capacity];
        }
    }

    public T Oldest
    {
        get
        {
            if (_count == 0)
            {
                throw new NotImplementedException("Bufferen er tom");
            }

            return _items[_start];
        }
    }

    public T Newest
    {
        get
        {
            if (_count == 0)
            {
                throw new NotImplementedException("Bufferen er tom");
            }

            int index = (_start + _count - 1) % Capacity;
            return _items[index];
        }
    }


    public void Clear()
    {
        Array.Clear(_items, 0, _items.Length);
        _start = 0;
        _count = 0;
    }

    public T[] ToArray()
    {
        T[] result = new T[_count];

        for (int i = 0; i < _count; i++)
        {
            result[i] = this[i];
        }

        return result;
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = 0; i < _count; i++)
        {
            yield return this[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

}
