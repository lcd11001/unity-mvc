using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Modules.SceneSystemModule;
using RMC.MiniMvcs.Samples.Configurator.Mini.Model;
using RMC.MiniMvcs.Samples.Configurator.Mini.Service;
using RMC.MiniMvcs.Samples.Configurator.Mini.View;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RMC.MiniMvcs.Samples.Configurator.Mini.Controller
{
    /// <summary>
    /// The Controller coordinates everything between
    /// the <see cref="IConcern"/>s and contains the core app logic 
    /// </summary>
    public class HudController: BaseController // Extending 'base' is optional
        <ConfiguratorModel, HudView, ConfiguratorService> 
    {
        public HudController(
            ConfiguratorModel model, HudView view, ConfiguratorService service) 
            : base(model, view, service)
        {
            
        }

        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context)
        {
            if (!IsInitialized)
            {
                base.Initialize(context);
                
                Debug.Log("1 listen");
                _view.OnBack.AddListener(View_OnBack);
            }
        }

        
        //  Methods ---------------------------------------
        
        
        //  Event Handlers --------------------------------
        private void View_OnBack()
        {
            RequireIsInitialized();
            Debug.Log("2 invoke");
            Context.CommandManager.InvokeCommand(new SceneSystemLoadSceneCommand(ConfiguratorModel.Scene01_Menu));
        }
    }
}