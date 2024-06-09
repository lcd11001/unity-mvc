using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MonoUtils
{
    public static void CheckNull(object component, string name)
    {
        if (component == null)
        {
            Debug.LogError($"{name} is null. Consider calling MVCS to create an instance of it.");
            throw new Exception($"{name} is null. Consider calling MVCS to create an instance of it.");
        }
    }
}
