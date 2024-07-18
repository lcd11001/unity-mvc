using RMC.Mini.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MonoMVCS
{
    public class MonoController<TModel, TView, TService> : BaseController<TModel, TView, TService>
        where TModel : MonoModel
        where TView : MonoView
        where TService : MonoService
    {
        public MonoController(TModel model, TView view, TService service) : base(model, view, service)
        {
            _model.OnModelInitializedEvent += OnModelInitialized;
            _model.OnModelChangedEvent += OnModelChanged;
        }

        public override void Dispose()
        {
            base.Dispose();
            _model.OnModelChangedEvent -= OnModelChanged;
            _model.OnModelInitializedEvent -= OnModelInitialized;
        }

        private void OnModelInitialized(MonoModel model)
        {
            MainThreadDispatcher.RunOnMainThread(() =>
            {
                if (_view != null && _model != null)
                {
                    _view.InitView(model);
                }
            });
        }

        protected virtual void OnModelChanged(MonoModel model)
        {
            MainThreadDispatcher.RunOnMainThread(() =>
            {
                if (_view != null && _model != null)
                {
                    _view.UpdateView(model);
                }
            });
        }
    }
}