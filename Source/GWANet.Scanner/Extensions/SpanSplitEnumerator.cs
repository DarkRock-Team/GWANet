using System;

namespace GWANet.Scanner.Extensions;

/// <summary>
/// Allows for effective, no heap alloction string splitting
/// </summary>
internal ref struct SpanSplitEnumerator<T> where T : IEquatable<T>
{
    public T SplitItem { get; private set; }
    public ReadOnlySpan<T> Current { get; private set; }
    private ReadOnlySpan<T> _original;
    private bool _reachedEnd;
    
    public bool MoveNext()
    {
        var index = _original.IndexOf(SplitItem);
        if (index == -1)
        {
            if (_reachedEnd)
                return false;

            Current = _original;
            _reachedEnd = true;
            return true;
        }
        
        Current = _original[..index];
        _original = _original[(index + 1)..];

        return true;
    }
    
    public SpanSplitEnumerator(ReadOnlySpan<T> item, T splitItem)
    {
        _original  = item;
        Current    = _original;
        SplitItem  = splitItem;
        _reachedEnd = false;
    }
}