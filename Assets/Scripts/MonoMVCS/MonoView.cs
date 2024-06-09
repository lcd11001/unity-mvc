using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.View;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoView : MonoBehaviour, IView, IDisposable
{
    public bool IsInitialized => _isInitialized;

    public IContext Context => _context;

    private bool _isInitialized = false;
    private IContext _context;

    public virtual void Initialize(IContext context)
    {
        _context = context;
        _isInitialized = true;
    }

    public virtual void RequireIsInitialized()
    {
        if (!_isInitialized)
        {
            Debug.LogError("BaseView: MustBeInitialized");
            throw new Exception("MustBeInitialized");
        }
    }

    public abstract void UpdateView(MonoModel data);

    public virtual void Dispose()
    {
    }
}
