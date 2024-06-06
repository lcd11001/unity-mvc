using System.Collections;
using System.Collections.Generic;
using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Model;
using UnityEngine;

public partial class MyLoginModel : BaseModel
{
    private Observable<bool> isLoggedIn = new Observable<bool>();
    private Observable<MyLoginData> loggedInUserData = new Observable<MyLoginData>();


    override public void Initialize(IContext context)
    {
        if (!IsInitialized)
        {
            base.Initialize(context);

            // Set Defaults
            isLoggedIn.Value = false;
            loggedInUserData.Value = null;
        }
    }
}
