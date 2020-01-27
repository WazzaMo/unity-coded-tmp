/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */


public class ChangeTrigger<T> where T : struct
{
    public delegate void Updater(T value);
    private event Updater _Callbacks;

    private T _Current;

    public ChangeTrigger(T value = default)
    {
        _Current = value;
        _Callbacks = null;
    }
    
    public T Value {
        get { return _Current; }

        set {
            if (!_Current.Equals( value) )
            {
                _Callbacks.Invoke(value);
                _Current = value;
            }
        }
    }

    public void Add(Updater callback)
    {
        _Callbacks += callback;
    }

    public void Del(Updater callback)
    {
        _Callbacks -= callback;
    }
}
