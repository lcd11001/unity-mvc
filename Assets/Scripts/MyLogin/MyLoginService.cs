using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Service;
using UnityEngine;

public class MyLoginService : BaseService
{
    public void Login(string username, string password)
    {
        // simulate a login with a delay
        Task.Delay(2000).ContinueWith((task) =>
        {
            if (username == "admin" && password == "admin")
            {
                Context.CommandManager.InvokeCommand(new MyLoginCommands.LoginResponseCommand(true, $"{{\"u\":\"{username}\",\"p\":\"{password}\"}}"));
            }
            else
            {
                Context.CommandManager.InvokeCommand(new MyLoginCommands.LoginResponseCommand(false, "Invalid username or password"));
            }
        });
    }
}
