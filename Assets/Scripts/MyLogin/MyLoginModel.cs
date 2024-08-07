using RMC.Core.Observables;
using RMC.Mini;
using RMC.Mini.Model;
using UnityEngine;

public partial class MyLoginModel<T> : BaseModel
    where T : ScriptableObject
{
    public bool IsLoggedIn => loggedInUserData.Value != null;
    public Observable<T> loggedInUserData = null;

    public MyLoginModel(T data)
    {
        loggedInUserData = new()
        {
            Value = data
        };
    }

    override public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            base.Initialize(context);

            // Set Defaults
            loggedInUserData.Value = null;

            loggedInUserData.OnValueChanged.AddListener(OnUserDataChanged);
        }
    }

    private void OnUserDataChanged(T prev, T curr)
    {
        RequireIsInitialized();
        Debug.Log($"MyLoginModel.OnUserDataChanged: prev {prev} curr {curr}");
        Context.CommandManager.InvokeCommand(new MyLoginCommands.LoginCompleteCommand<T>(true, curr));
    }

    public void SetLoggedInUserData(T data)
    {
        loggedInUserData.Value = data;
    }
}
