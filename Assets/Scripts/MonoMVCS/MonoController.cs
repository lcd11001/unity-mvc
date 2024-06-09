using RMC.Core.Architectures.Mini.Controller;
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
            _model.OnModelChangedEvent += _view.UpdateView;
        }

        public override void Dispose()
        {
            base.Dispose();
            _model.OnModelChangedEvent -= _view.UpdateView;
        }
    }
}