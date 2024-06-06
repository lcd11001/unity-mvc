using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini;
using RMC.Core.Architectures.Mini.Context;
using UnityEngine;

public class MyLogin : MiniMvcs<
    Context,
    MyLoginModel,
    MyLoginView,
    MyLoginController,
    MyLoginService
>
{
    public MyLogin(MyLoginView view)
    {
        _view = view;

        _context = new Context();
        _model = new MyLoginModel();
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
