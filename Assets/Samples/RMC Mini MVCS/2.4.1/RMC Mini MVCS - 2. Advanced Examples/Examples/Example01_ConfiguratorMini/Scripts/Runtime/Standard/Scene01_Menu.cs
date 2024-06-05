using RMC.MiniMvcs.Samples.Configurator.Mini.View;
using UnityEngine;

namespace RMC.MiniMvcs.Samples.Configurator.Mini
{
    /// <summary>
    /// This is the main entry point to one of the scenes
    /// </summary>
    public class Scene01_Menu : MonoBehaviour
    {
        //  Fields ----------------------------------------
        [SerializeField] 
        private HudView _hudView;
       
        [SerializeField] 
        private MenuView _menuView;

        
        //  Unity Methods  --------------------------------
        protected void Start()
        {
            UpdateMiniForThisScene();
        }

        
        //  Methods ---------------------------------------
        private void UpdateMiniForThisScene()
        {
            ConfiguratorMini mini = ConfiguratorMiniSingleton.Instance.ConfiguratorMini;
            Debug.Log("ConfiguratorMini2: " + mini);
            mini.UpdateMiniFor_MenuScene(_hudView, _menuView);
        }
        
        //  Event Handlers --------------------------------
    }
}