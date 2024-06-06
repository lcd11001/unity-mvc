using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Controller;
using UnityEngine;

public class MyLoginController : BaseController<MyLoginModel, MyLoginView, MyLoginService>
{
    public MyLoginController(MyLoginModel model, MyLoginView view, MyLoginService service) : base(model, view, service)
    {
    }
}
