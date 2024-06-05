using RMC.MiniMvcs.Samples.Configurator.Mini.View;
using UnityEngine;

namespace RMC.MiniMvcs.Samples.Configurator.Mini
{
    /// <summary>
    /// This is the main entry point to one of the scenes
    /// </summary>
    public class Scene04_Game : MonoBehaviour
    {
        //  Fields ----------------------------------------
        [SerializeField] 
        private HudView _hudView;
        
        [SerializeField] 
        private GameView _gameView;

        
        //  Unity Methods  --------------------------------
        protected void Start()
        {
            UpdateMiniForThisScene();
        }

        
        //  Methods ---------------------------------------
        private void UpdateMiniForThisScene()
        {
            ConfiguratorMini mini = ConfiguratorMiniSingleton.Instance.ConfiguratorMini;
            mini.UpdateMiniFor_GameScene(_hudView, _gameView);
        }

        //  Event Handlers --------------------------------
    }
}