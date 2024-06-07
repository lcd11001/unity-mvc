using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IScriptableModel: IModel
{
    void UpdateModel(ScriptableObject data);
}

public abstract class BaseScriptableModel: ScriptableObject, IScriptableModel
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

    public abstract void UpdateModel(ScriptableObject data);
}
