using RMC.MiniMvcs.Samples.Configurator.Mini.View;
using UnityEngine;
using UnityEngine.Serialization;

namespace RMC.MiniMvcs.Samples.Configurator.Mini
{
    /// <summary>
    /// This is the main entry point to one of the scenes
    /// </summary>
    public class Scene03_CustomizeEnvironment : MonoBehaviour
    {
        //  Fields ----------------------------------------
        
        [SerializeField] 
        private HudView _hudView;
        
        [SerializeField] 
        private CustomizeEnvironmentView _customizeEnvironmentView;

        
        //  Unity Methods  --------------------------------
        protected void Start()
        {
            UpdateMiniFor_CustomizeEnvironmentScene();
        }

        
        //  Methods ---------------------------------------
        private void UpdateMiniFor_CustomizeEnvironmentScene()
        {
            if (!ConfiguratorMiniSingleton.IsInstantiated)
            {
                // Create the mini as you typically do  
                // Store the mini on the ComplexMiniSingleton 
                ConfiguratorMiniSingleton.Instance.ConfiguratorMini = new ConfiguratorMini();
                ConfiguratorMiniSingleton.Instance.ConfiguratorMini.Initialize();
            }
            
            ConfiguratorMini mini = ConfiguratorMiniSingleton.Instance.ConfiguratorMini;
            mini.UpdateMiniFor_CustomizeEnvironmentScene(_hudView, _customizeEnvironmentView);
        }


        //  Event Handlers --------------------------------
    }
}