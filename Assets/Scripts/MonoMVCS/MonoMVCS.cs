using NaughtyAttributes;
using RMC.Core.Architectures.Mini;
using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace MonoMVCS
{
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

        protected virtual void OnDestroy()
        {
            if (IsInintialized)
            {
                model.Dispose();
                view.Dispose();
                controller.Dispose();
                service.Dispose();
            }
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
            Assert.IsNotNull(context, $"{nameof(context)} is null. Consider calling MVCS to create an instance of it.");
            Assert.IsNotNull(model, $"{nameof(model)} is null. Consider calling MVCS to create an instance of it.");
            Assert.IsNotNull(view, $"{nameof(view)} is null. Consider calling MVCS to create an instance of it.");
            Assert.IsNotNull(controller, $"{nameof(controller)} is null. Consider calling MVCS to create an instance of it.");
            Assert.IsNotNull(service, $"{nameof(service)} is null. Consider calling MVCS to create an instance of it.");

            model.Initialize(context);
            view.Initialize(context);
            service.Initialize(context);
            controller.Initialize(context);

            _isInitialized = true;
        }

    }
}