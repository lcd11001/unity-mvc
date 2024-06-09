using System;
using RMC.Core.Architectures.Mini;
using RMC.Core.Architectures.Mini.Controller;
using UnityEngine;

public class MyLoginController : BaseController<MyLoginModel<MyLoginData>, MyLoginView, MyLoginService>
{
    public MyLoginController(MyLoginModel<MyLoginData> model, MyLoginView view, MyLoginService service) : base(model, view, service)
    {
        Application.quitting += Dispose;
    }

    public override void Dispose()
    {
        base.Dispose();
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
            MainThreadDispatcher.RunOnMainThread(() =>
            {
                // fixed: CreateScriptableObjectInstanceFromType can only be called from the main thread.
                _model.SetLoggedInUserData(MyLoginData.FromJson(cmd.Message));
            });
        }
        else
        {
            Debug.LogError($"MyLoginController.OnLoginResponseCommand: FAIL {cmd.Message}");
            Context.CommandManager.InvokeCommand(new MyLoginCommands.LoginCompleteCommand<MyLoginData>(false, null));
        }
    }

    private void OnLoginRequestCommand(MyLoginCommands.LoginRequestCommand cmd)
    {
        Debug.Log($"MyLoginController.OnLoginRequestCommand: {cmd.Username} {cmd.Password}");
        _service.Login(cmd.Username, cmd.Password);
    }
}
