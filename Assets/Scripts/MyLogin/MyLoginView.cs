using UnityEngine;
using TMPro;
using Doozy.Runtime.UIManager.Components;
using RMC.Mini.View;
using RMC.Mini;

public class MyLoginView : MonoBehaviour, IView
{
    [SerializeField] private TMP_InputField txtUsername;
    [SerializeField] private TMP_InputField txtPassword;
    [SerializeField] private TMP_Text txtStatus;
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

            Context.CommandManager.AddCommandListener<MyLoginCommands.LoginCompleteCommand<MyLoginData>>(OnLoginComplete);
        }
    }

    void Start()
    {
        SetViewStatus(false, "");
    }

    void SetViewStatus(bool success, string message)
    {
        txtStatus.text = message;
        btnLogin.interactable = !success;
        btnClear.interactable = !success;
        btnLogout.interactable = success;
    }

    void OnDestroy()
    {
        if (IsInitialized)
        {
            Context.CommandManager.RemoveCommandListener<MyLoginCommands.LoginCompleteCommand<MyLoginData>>(OnLoginComplete);
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
        Context.CommandManager.InvokeCommand(new MyLoginCommands.LoginRequestCommand(txtUsername.text, txtPassword.text));
    }

    private void OnLoginComplete(MyLoginCommands.LoginCompleteCommand<MyLoginData> cmd)
    {
        if (cmd.IsSuccess)
        {
            SetViewStatus(true, $"Logged In SUCCESS {cmd.UserData.UserName}");
        }
        else
        {
            SetViewStatus(false, "Logged In FAILED");
        }
    }

    public void Dispose()
    {

    }
}
