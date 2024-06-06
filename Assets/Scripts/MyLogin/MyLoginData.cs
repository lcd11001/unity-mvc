using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public partial class MyLoginModel
{
    // only MyLoginModel can access this
    internal class MyLoginData
    {
        [JsonProperty("u")]
        public string UserName { get; private set; }
        [JsonProperty("p")]
        public string Password { get; private set; }

        internal MyLoginData(string userName, string password)
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

        static public MyLoginData FromJson(string json)
        {
            return JsonConvert.DeserializeObject<MyLoginData>(json);
        }
    }
}
