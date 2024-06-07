using Doozy.Runtime.UIManager.Components;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MyLoginMonoView : BaseView
{
    [SerializeField] private TMP_InputField txtUsername;
    [SerializeField] private TMP_InputField txtPassword;
    [SerializeField] private TMP_Text txtStatus;
    [SerializeField] private UIButton btnLogin;
    [SerializeField] private UIButton btnClear;
    [SerializeField] private UIButton btnLogout;

    void OnEnable()
    {
        btnLogin?.onClickEvent.AddListener(OnBtnLoginClick);
        btnClear?.onClickEvent.AddListener(OnBtnClearClick);
        btnLogout?.onClickEvent.AddListener(OnBtnLogoutClick);
    }

    void OnDisable()
    {
        btnLogin?.onClickEvent.RemoveListener(OnBtnLoginClick);
        btnClear?.onClickEvent.RemoveListener(OnBtnClearClick);
        btnLogout?.onClickEvent.RemoveListener(OnBtnLogoutClick);
    }

    private void OnBtnLogoutClick()
    {
        RequireIsInitialized();
        Context.CommandManager.InvokeCommand(new MyLoginCommands.LogoutCommand());
    }

    private void OnBtnClearClick()
    {
        RequireIsInitialized();
        Context.CommandManager.InvokeCommand(new MyLoginCommands.ClearCommand());
    }

    private void OnBtnLoginClick()
    {
        RequireIsInitialized();
        Context.CommandManager.InvokeCommand(new MyLoginCommands.LoginRequestCommand(txtUsername.text, txtPassword.text));
    }

    public override void UpdateView(ScriptableObject data)
    {
        
    }
}
