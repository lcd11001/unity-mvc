using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Controller.Commands;
using UnityEngine;

public class MyLoginCommands
{
    public class LoginCommand : ICommand
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public LoginCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public class LogoutCommand : ICommand
    {
    }

    public class ClearCommand : ICommand
    {
    }
}
