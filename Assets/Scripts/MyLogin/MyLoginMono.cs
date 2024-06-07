using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyLoginMono : MonoBehaviour
{
    [SerializeField] private MyLoginView view;
    [SerializeField] private MyLoginData userData;

    void Start()
    {
        if (userData == null)
        {
            userData = ScriptableObject.CreateInstance<MyLoginData>();
        }
        MyLogin myLoginMVCS = new MyLogin(view, userData);
        myLoginMVCS.Initialize();

        view.Context.CommandManager.AddCommandListener<MyLoginCommands.LoginCompleteCommand<MyLoginData>>(OnLoginComplete);
    }

    private void OnDestroy()
    {
        view.Context.CommandManager.RemoveCommandListener<MyLoginCommands.LoginCompleteCommand<MyLoginData>>(OnLoginComplete);
    }

    private void OnLoginComplete(MyLoginCommands.LoginCompleteCommand<MyLoginData> cmd)
    {
        if (cmd.IsSuccess)
        {
            userData = cmd.UserData;
        }
    }
}
