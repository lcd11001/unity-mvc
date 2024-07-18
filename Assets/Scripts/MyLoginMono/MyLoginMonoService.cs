using MonoMVCS;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MyLoginMonoService : MonoService
{
    public void Login(string username, string password)
    {
        Task.Delay(1000).ContinueWith((task) =>
        {
            if (username != "admin" || password != "admin")
            {
                Context.CommandManager.InvokeCommand(new MyLoginCommands.LoginResponseCommand(false, "wrong username or password"));
            }
            else
            {
                Context.CommandManager.InvokeCommand(new MyLoginCommands.LoginResponseCommand(true, $"{{\"u\": {username}, \"p\": {password}}}"));
            }
        });

    }
}
