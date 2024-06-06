using System;
using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using Unity.VisualScripting;
using UnityEngine;

public class MyLoginController : BaseController<MyLoginModel, MyLoginView, MyLoginService>, IDisposable
{
    public MyLoginController(MyLoginModel model, MyLoginView view, MyLoginService service) : base(model, view, service)
    {
        Application.quitting += Dispose;
    }

    public void Dispose()
    {
        Debug.Log("MyLoginController.Dispose");
        Context.CommandManager.RemoveCommandListener<MyLoginCommands.LoginRequestCommand>(OnLoginRequestCommand);
        Context.CommandManager.RemoveCommandListener<MyLoginCommands.LoginResponseCommand>(OnLoginResponseCommand);
    }

    public override void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            base.Initialize(context);
            Context.CommandManager.AddCommandListener<MyLoginCommands.LoginRequestCommand>(OnLoginRequestCommand);
            Context.CommandManager.AddCommandListener<MyLoginCommands.LoginResponseCommand>(OnLoginResponseCommand);
        }
    }

    private void OnLoginResponseCommand(MyLoginCommands.LoginResponseCommand cmd)
    {
        if (cmd.IsSuccess)
        {
            Debug.Log($"MyLoginController.OnLoginResponseCommand: SUCCESS {cmd.Message}");
        }
        else
        {
            Debug.LogError($"MyLoginController.OnLoginResponseCommand: FAIL {cmd.Message}");
        }
    }

    private void OnLoginRequestCommand(MyLoginCommands.LoginRequestCommand cmd)
    {
        Debug.Log($"MyLoginController.OnLoginRequestCommand: {cmd.Username} {cmd.Password}");
        _service.Login(cmd.Username, cmd.Password);
    }
}
