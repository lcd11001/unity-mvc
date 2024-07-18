using MonoMVCS;
using RMC.Mini;
using UnityEngine;

[CreateAssetMenu(fileName = "MyLoginMonoModel", menuName = "Mono MVCS/My Login Mono/Model")]
public class MyLoginMonoModel : MonoModel
{
    public MonoField<bool> IsLoggedIn = new MonoField<bool>();

    [field: SerializeField]
    public string StatusMessage { get; set; }
    [field: SerializeField]
    public string Username { get; set; }
    [field: SerializeField]
    public string Password { get; set; }

    override public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            base.Initialize(context);
            IsLoggedIn.Value = false;

            IsLoggedIn.OnValueChanged.AddListener(OnIsLoggedInChanged);
        }
    }

    private void OnIsLoggedInChanged(bool val)
    {
        OnModelChanged();
    }

    override public void Dispose()
    {
        base.Dispose();
        IsLoggedIn.OnValueChanged.RemoveListener(OnIsLoggedInChanged);
    }
}
