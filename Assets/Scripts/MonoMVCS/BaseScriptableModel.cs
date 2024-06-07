using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseScriptableModel : ScriptableObject, IModel
{
    public bool IsInitialized => _isInitialized;
    public IContext Context => _context;

    private bool _isInitialized = false;
    private IContext _context;

    public void Initialize(IContext context)
    {
        _context = context;
    }

    public void RequireIsInitialized()
    {
        if (!_isInitialized)
        {
            throw new System.Exception("MustBeInitialized");
        }
    }

    public abstract void UpdateModel<T>(T data);
}
