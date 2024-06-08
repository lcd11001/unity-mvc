using RMC.Core.Architectures.Mini.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoController<TModel, TView, TService> : BaseController<TModel, TView, TService>
    where TModel : MonoModel
    where TView : MonoView
    where TService : MonoService
{
    public MonoController(TModel model, TView view, TService service) : base(model, view, service)
    {
        model.OnModelChangedEvent += view.UpdateView;
    }
}
