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

    public virtual Context Context
    {
        get
        {
            if (context == null)
            {
                context = new Context();
            }
            return context;
        }
        set
        {
            context = value;
        }
    }

    protected virtual void Awake()
    {
        MVCS();
        Initialize(Context);
    }

    /// <summary>
    /// Create instances of model, view, controller and service
    /// </summary>
    /// <param name="context"></param>
    abstract protected void MVCS();

    protected virtual void Initialize(IContext context)
    {
        if (IsInintialized)
        {
            return;
        }

        // default support for all models, views, controllers and services
        // you can override this method to add or remove one of them
        CheckNull(context, nameof(context));
        CheckNull(model, nameof(model));
        CheckNull(view, nameof(view));
        CheckNull(controller, nameof(controller));
        CheckNull(service, nameof(service));

        model.Initialize(context);
        view.Initialize(context);
        service.Initialize(context);
        controller.Initialize(context);

        _isInitialized = true;
    }

    private void CheckNull(object component, string name)
    {
        if (component == null)
        {
            Debug.LogError($"{name} is null. Consider calling MVCS to create an instance of it.");
            throw new Exception($"{name} is null. Consider calling MVCS to create an instance of it.");
        }
    }
}
