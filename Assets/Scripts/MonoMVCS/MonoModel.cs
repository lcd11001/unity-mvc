using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IScriptableModel : IModel
{
    void OnModelChanged();
}

public abstract class MonoModel : ScriptableObject, IScriptableModel
{
    public bool IsInitialized => _isInitialized;
    public IContext Context => _context;

    private UnityEvent<MonoModel> OnModelChangedEvent;

    private bool _isInitialized = false;
    private IContext _context;

    public MonoModel()
    {
        OnModelChangedEvent = new UnityEvent<MonoModel>();
    }

    protected virtual void OnEnable()
    {
        // ScriptableObject instances persist across play sessions in the Unity editor.
        // This means that if you set _isInitialized to true during one play session,
        // it will remain true when you stop and then play again in the Unity editor.
        _isInitialized = false;
    }

    public virtual void Initialize(IContext context)
    {
        _context = context;
        _isInitialized = true;
    }

    public virtual void RequireIsInitialized()
    {
        if (!_isInitialized)
        {
            Debug.LogError("BaseScriptableModel: MustBeInitialized");
            throw new Exception("MustBeInitialized");
        }
    }

    public abstract void OnModelChanged();

    public void NotifyModelChanged()
    {
        OnModelChangedEvent.Invoke(this);
    }
}
