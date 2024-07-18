using RMC.Mini.Controller.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLoginCommands
{
    public class LoginRequestCommand : ICommand
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public LoginRequestCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    public class LoginResponseCommand : ICommand
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }

        public LoginResponseCommand(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }

    public class LoginCompleteCommand<T> : ICommand
    {
        public bool IsSuccess { get; private set; }
        public T UserData { get; private set; }

        public LoginCompleteCommand(bool isSuccess, T userData)
        {
            IsSuccess = isSuccess;
            UserData = userData;
        }
    }

    public class LogoutCommand : ICommand
    {
    }

    public class ClearCommand : ICommand
    {
    }

    public class ModelChangedCommand<T> : ICommand
    {
        public T Model { get; private set; }

        public ModelChangedCommand(T model)
        {
            Model = model;
        }
    }
}
