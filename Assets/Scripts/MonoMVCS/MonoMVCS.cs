using NaughtyAttributes;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoMVCS<TModel, TView, TController, TService> : MonoBehaviour
    where TModel : BaseScriptableModel
    where TView : BaseView
    where TService : BaseService
    where TController : BaseController<TModel, TView, TService>
{
    [Required]
    [SerializeField] protected TModel model;

    [Required]
    [SerializeField] protected TView view;

    protected TController controller;
    protected TService service;
    protected Context context;

    private bool _isInitialized = false;
    public bool IsInintialized => _isInitialized;

    protected virtual void Awake()
    {
        MVCS();
        Initialize(context);
    }

    /// <summary>
    /// Create instances of model, view, controller and service
    /// </summary>
    /// <param name="context"></param>
    abstract protected void MVCS();

    protected virtual void Initialize(IContext context)
    {
        if (context == null || model == null || view == null || controller == null || service == null)
        {
            throw new Exception("Consider calling MVCS to create instances of model, view, controller and service");
        }

        if (IsInintialized)
        {
            return;
        }

        model.Initialize(context);
        view.Initialize(context);
        service.Initialize(context);
        controller.Initialize(context);

        _isInitialized = true;
    }
}
