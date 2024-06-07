using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class FieldObservable<T>
{
    [SerializeField] private T _value;
    public readonly UnityEvent<T> OnValueChanged;

    public FieldObservable()
    {
        OnValueChanged = new UnityEvent<T>();
    }

    public T Value
    {
        get => _value;
        set
        {
            _value = value;
            OnValueChanged.Invoke(_value);
        }
    }
}  
