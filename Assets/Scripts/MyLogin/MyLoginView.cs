using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Doozy.Runtime.UIManager.Components;
using RMC.Core.Architectures.Mini.View;
using RMC.Core.Architectures.Mini.Context;
using Unity.VisualScripting;
using System;

public class MyLoginView : MonoBehaviour, IView
{
    [SerializeField] private TMP_InputField txtUsername;
    [SerializeField] private TMP_InputField txtPassword;
    [SerializeField] private UIButton btnLogin;
    [SerializeField] private UIButton btnClear;
    [SerializeField] private UIButton btnLogout;

    private bool isInitialized = false;
    private IContext context;

    public bool IsInitialized => isInitialized;

    public IContext Context => context;

    public void Initialize(IContext context)
    {
        if (!isInitialized)
        {
            this.context = context;
            isInitialized = true;
        }
    }

    public void RequireIsInitialized()
    {
        if (!IsInitialized)
        {
            throw new System.Exception("Not initialized");
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
        Context.CommandManager.InvokeCommand(new MyLoginCommands.LoginCommand(txtUsername.text, txtPassword.text));
    }
}
