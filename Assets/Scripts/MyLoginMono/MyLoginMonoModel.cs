using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MyLoginMonoModel", menuName = "Mono MVCS/My Login Mono/Model")]
public class MyLoginMonoModel : BaseScriptableModel
{
    [field: SerializeField]
    public bool IsLoggedIn { get; set; }
    [field: SerializeField]
    public string StatusMessage { get; set; }
    [field: SerializeField]
    public string Username { get; set; }
    [field: SerializeField]
    public string Password { get; set; }

    public override void UpdateModel(ScriptableObject data)
    {
        
    }
}
