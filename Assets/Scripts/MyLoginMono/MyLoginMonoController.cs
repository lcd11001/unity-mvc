using RMC.Core.Architectures.Mini.Controller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLoginMonoController : BaseController<MyLoginMonoModel, MyLoginMonoView, MyLoginMonoService>
{
    public MyLoginMonoController(MyLoginMonoModel model, MyLoginMonoView view, MyLoginMonoService service) : base(model, view, service)
    {
    }
}
