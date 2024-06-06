using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MyLoginModel
{
    // only MyLoginModel can access this
    internal class MyLoginData
    {
        public string UserName { get; private set; }
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
    }
}
