/*
 * Written by Warwick Molloy (c) Copyright 2020
 * May be distributed under the MIT License
 */

using UnityEngine;

namespace Kreation.Util
{

    public class ChangeTrigger<T> where T : struct
    {
        public delegate void Updater(T value, T oldvalue);
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
                if (!_Current.Equals(value))
                {
                    var old = _Current;
                    _Current = value;
                    if (_Callbacks != null)
                    {
                        _Callbacks.Invoke(value, old);
                    }
                    else
                    {
                        Debug.Log(
                            $"{nameof(ChangeTrigger<T>)} [{typeof(T).Name}]"
                            +$" No handlers registered."
                        );
                    }
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

        public void Reset()
        {
            _Callbacks = null;
        }
    }

}