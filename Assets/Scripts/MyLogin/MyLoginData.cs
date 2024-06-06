using UnityEngine;
using Newtonsoft.Json;

[CreateAssetMenu(fileName = "UserData", menuName = "Data/Login", order = 1)]
[JsonObject(MemberSerialization.OptIn)]
public class MyLoginData : ScriptableObject
{
    [field: SerializeField]
    [JsonProperty("u")]
    public string UserName { get; private set; }

    [field: SerializeField]
    [JsonProperty("p")]
    public string Password { get; private set; }

    // prevent outside creation with new operator
    private MyLoginData(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public override string ToString()
    {
        //return $"[MyLoginData ({UserName}, {Password})]";
        return $"[MyLoginData {this.ToJson()}]";
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    // only MyLoginModel can create data
    static public MyLoginData FromJson(string json)
    {
        return JsonConvert.DeserializeObject<MyLoginData>(json, new ScriptableObjectJsonConverter<MyLoginData>());
    }
}
