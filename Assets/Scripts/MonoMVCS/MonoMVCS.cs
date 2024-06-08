using NaughtyAttributes;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoMVCS<TModel, TView, TController, TService> : MonoBehaviour
    where TModel : MonoModel
    where TView : MonoView
    where TService : MonoService
    where TController : MonoController<TModel, TView, TService>
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
    protected virtual void MVCS()
    {
        context = new Context();
        service = (TService)Activator.CreateInstance(typeof(TService));
        controller = (TController)Activator.CreateInstance(typeof(TController), model, view, service);
    }

    protected virtual void Initialize(IContext context)
    {
        if (IsInintialized)
        {
            return;
        }

        // default support for all models, views, controllers and services
        // you can override this method to add or remove one of them
        MonoUtils.CheckNull(context, nameof(context));
        MonoUtils.CheckNull(model, nameof(model));
        MonoUtils.CheckNull(view, nameof(view));
        MonoUtils.CheckNull(controller, nameof(controller));
        MonoUtils.CheckNull(service, nameof(service));

        model.Initialize(context);
        view.Initialize(context);
        service.Initialize(context);
        controller.Initialize(context);

        _isInitialized = true;
    }

}
