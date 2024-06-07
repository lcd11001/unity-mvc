using Doozy.Runtime.UIManager.Components;
using RMC.Core.Architectures.Mini.Context;
using System;
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

    override public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            base.Initialize(context);
            Context.CommandManager.AddCommandListener<MyLoginCommands.ModelChangedCommand<MyLoginMonoModel>>(OnModelChanged);
        }
    }

    private void OnDestroy()
    {
        if (IsInitialized)
        {
            Context.CommandManager.RemoveCommandListener<MyLoginCommands.ModelChangedCommand<MyLoginMonoModel>>(OnModelChanged);
        }
    }

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

    public override void UpdateView(ScriptableObject obj)
    {
        MyLoginMonoModel data = obj as MyLoginMonoModel;
        if ( data.IsLoggedIn.Value)
        {
            txtStatus.text = $"Loggin SUCCESS with {data.Username}";
        }
        else
        {
            txtStatus.text = $"Login FAIL with {data.StatusMessage}";
        }
    }

    private void OnModelChanged(MyLoginCommands.ModelChangedCommand<MyLoginMonoModel> cmd)
    {
        UpdateView(cmd.Model);
    }
}
