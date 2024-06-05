using UnityEngine;

namespace RMC.MiniMvcs.Samples.Configurator.Utilities
{
    public static class CustomGameObjectUtility
    {
        //  Methods ---------------------------------------
        public static void DontDestroyOnLoadSafe(GameObject gameObject)
        {
            gameObject.transform.parent = null;
            Object.DontDestroyOnLoad(gameObject);
        }
    }
}
