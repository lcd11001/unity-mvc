using RMC.MiniMvcs.Samples.Configurator.Mini.View;
using UnityEngine;

namespace RMC.MiniMvcs.Samples.Configurator.Mini
{
    /// <summary>
    /// This is the main entry point to one of the scenes
    /// </summary>
    public class Scene02_CustomizeCharacter : MonoBehaviour
    {
        //  Fields ----------------------------------------
        [SerializeField] 
        private HudView _hudView;
        
        [SerializeField] 
        private CustomizeCharacterView _customizeCharacterView;

        
        //  Unity Methods  --------------------------------
        protected void Start()
        {
            UpdateMiniFor_CustomizeCharacterScene();
        }

        
        //  Methods ---------------------------------------
        private void UpdateMiniFor_CustomizeCharacterScene()
        {
            if (!ConfiguratorMiniSingleton.IsInstantiated)
            {
                // Create the mini as you typically do  
                // Store the mini on the ComplexMiniSingleton 
                ConfiguratorMiniSingleton.Instance.ConfiguratorMini = new ConfiguratorMini();
                ConfiguratorMiniSingleton.Instance.ConfiguratorMini.Initialize();
            }
            
            ConfiguratorMini mini = ConfiguratorMiniSingleton.Instance.ConfiguratorMini;
            mini.UpdateMiniFor_CustomizeCharacterScene(_hudView, _customizeCharacterView);
        }


        //  Event Handlers --------------------------------
    }
}