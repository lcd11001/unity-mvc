using RMC.Core.Architectures.Mini.Context;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyLoginMonoMVCS : MonoMVCS<MyLoginMonoModel, MyLoginMonoView, MyLoginMonoController, MyLoginMonoService>
{
    protected override void MVCS()
    {
        // model & view have been initialized via Unity Inspector
        service = new MyLoginMonoService();
        controller = new MyLoginMonoController(model, view, service);
    }
}
