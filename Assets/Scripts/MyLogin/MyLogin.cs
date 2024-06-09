using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini;
using RMC.Core.Architectures.Mini.Structure.Simple;
using UnityEngine;

public class MyLogin : SimpleMiniMvcs<
    Context,
    MyLoginModel<MyLoginData>,
    MyLoginView,
    MyLoginController,
    MyLoginService
>
{
    public MyLogin(MyLoginView view, MyLoginData data)
    {
        _view = view;

        _context = new Context();
        _model = new MyLoginModel<MyLoginData>(data);
        _service = new MyLoginService();
        _controller = new MyLoginController(_model, _view, _service);
    }

    public override void Initialize()
    {
        if (!IsInitialized)
        {
            _model.Initialize(_context);
            _view.Initialize(_context);
            _service.Initialize(_context);
            _controller.Initialize(_context);
        }
    }
}
