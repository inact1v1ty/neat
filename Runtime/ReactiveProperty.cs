using System;
using System.Collections;
using System.Collections.Generic;

public class ReactiveProperty<T>
{
    private T _value = default;
    public T Value
    {
        get {
            return _value;
        }
        set {
            _value = value;
            OnChanged?.Invoke();
        }
    }

    public delegate void OnChange();
    public event OnChange OnChanged;

    public ReactiveProperty(T value)
    {
        _value = value;
    }

    public void SilentSet(T value)
    {
        _value = value;
    }
}
