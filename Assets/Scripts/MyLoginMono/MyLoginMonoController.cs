using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLoginMonoController : MonoController<MyLoginMonoModel, MyLoginMonoView, MyLoginMonoService>
{
    public MyLoginMonoController(MyLoginMonoModel model, MyLoginMonoView view, MyLoginMonoService service) : base(model, view, service)
    {
    }

    public override void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            base.Initialize(context);

            Context.CommandManager.AddCommandListener<MyLoginCommands.LoginRequestCommand>(OnViewLoginRequestCommand);
            Context.CommandManager.AddCommandListener<MyLoginCommands.LogoutCommand>(OnViewLogoutCommand);
            Context.CommandManager.AddCommandListener<MyLoginCommands.LoginResponseCommand>(OnServiceLoginResponse);
        }
    }

    private void OnServiceLoginResponse(MyLoginCommands.LoginResponseCommand cmd)
    {
        _model.StatusMessage = cmd.Message;
        _model.IsLoggedIn.Value = cmd.IsSuccess;
    }

    private void OnViewLoginRequestCommand(MyLoginCommands.LoginRequestCommand cmd)
    {
        _service.Login(cmd.Username, cmd.Password);
    }

    private void OnViewLogoutCommand(MyLoginCommands.LogoutCommand cmd)
    {

    }
}
