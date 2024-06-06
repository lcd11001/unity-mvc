using System.Collections;
using System.Collections.Generic;
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
        return $"[MyLoginData ({UserName}, {Password})]";
    }

    public string ToJson()
    {
        return JsonConvert.SerializeObject(this);
    }

    // only MyLoginModel can create data
    static public MyLoginData FromJson(string json)
    {
        return JsonConvert.DeserializeObject<MyLoginData>(json, new MyLoginDataConverter());
    }


    internal class MyLoginDataConverter : JsonConverter<MyLoginData>
    {
        public override MyLoginData ReadJson(JsonReader reader, System.Type objectType, MyLoginData existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            string userName = null;
            string password = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName)
                {
                    string propertyName = (string)reader.Value;
                    reader.Read();
                    switch (propertyName)
                    {
                        case "u":
                            userName = (string)reader.Value;
                            break;
                        case "p":
                            password = (string)reader.Value;
                            break;
                    }
                }
            }

            try
            {
                MyLoginData data = ScriptableObject.CreateInstance<MyLoginData>();
                data.UserName = userName;
                data.Password = password;

                return data;
            }
            catch (System.Exception e)
            {
                Debug.LogError(e);
            }
            return null;
        }

        public override void WriteJson(JsonWriter writer, MyLoginData value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("u");
            writer.WriteValue(value.UserName);
            writer.WritePropertyName("p");
            writer.WriteValue(value.Password);
            writer.WriteEndObject();
        }
    }
}
