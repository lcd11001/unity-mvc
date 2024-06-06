using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MyLoginMonoMvcs : MonoBehaviour
{
    [SerializeField] private MyLoginView view;

    void Start()
    {
        MyLogin myLoginMVCS = new MyLogin(view);
        myLoginMVCS.Initialize();
    }
}
