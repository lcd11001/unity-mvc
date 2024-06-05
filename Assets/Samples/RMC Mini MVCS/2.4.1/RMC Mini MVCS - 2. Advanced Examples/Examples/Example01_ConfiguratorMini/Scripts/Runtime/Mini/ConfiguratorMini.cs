using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Controller;
using RMC.Core.Architectures.Mini.Model;
using RMC.Core.Architectures.Mini.Modules.SceneSystemModule;
using RMC.Core.Architectures.Mini.Service;
using RMC.Core.Architectures.Mini.View;
using RMC.Core.Experimental.Architectures.Mini;
using RMC.MiniMvcs.Samples.Configurator.Mini.Controller;
using RMC.MiniMvcs.Samples.Configurator.Mini.Model;
using RMC.MiniMvcs.Samples.Configurator.Mini.Service;
using RMC.MiniMvcs.Samples.Configurator.Mini.View;
using UnityEngine;

namespace RMC.MiniMvcs.Samples.Configurator.Mini
{
    /// <summary>
    /// The ComplexMini is the parent object containing
    /// all <see cref="IConcern"/>s as children. It
    /// defines one instance of the Mvcs architectural
    /// framework within an application.
    /// </summary>
    public class ConfiguratorMini: MiniMvcsComplex
            <Context, 
                IModel, 
                IView, 
                IController,
                IService>
    {

        //  Initialization  -------------------------------
        public override void Initialize()
        {
            if (!IsInitialized)
            {
                _isInitialized = true;
                
                _context = new Context();

                //
                ConfiguratorModel model = new ConfiguratorModel();
                ConfiguratorService service = new ConfiguratorService();
                    
                // ModelLocator is created in superclass
                _viewLocator = new Locator<IView>();
                _controllerLocator = new Locator<IController>();
                _serviceLocator = new Locator<IService>();
                
                // Model item is already added in superclass
                ServiceLocator.AddItem(service);
                
                //
                model.Initialize(_context);
                service.Initialize(_context);
            }
        }

        
        //  Methods ---------------------------------------
        public void UpdateMiniFor_MenuScene(HudView hudView, MenuView menuView)
        {
            ConfiguratorModel model = ModelLocator.GetItem<ConfiguratorModel>();
            
            // Set Mode
            model.GameMode.Value = GameMode.Menu;
            
            AddFeaturesForHud(hudView);
            AddFeaturesForMenu(menuView);
        }
        
        
        public void UpdateMiniFor_CustomizeCharacterScene(HudView hudView, CustomizeCharacterView customizeCharacterView)
        {
            ConfiguratorModel model = ModelLocator.GetItem<ConfiguratorModel>();
            
            // Set Mode
            model.GameMode.Value = GameMode.CustomizeCharacter;
            
            AddFeaturesForHud(hudView);
            AddFeaturesForCustomizeCharacter(customizeCharacterView);
        }
        
        
        public void UpdateMiniFor_CustomizeEnvironmentScene(HudView hudView, CustomizeEnvironmentView customizeEnvironmentView)
        {
            ConfiguratorModel model = ModelLocator.GetItem<ConfiguratorModel>();
            
            // Set Mode
            model.GameMode.Value = GameMode.CustomizeEnvironment;
            
            AddFeaturesForHud(hudView);
            AddFeaturesForCustomizeEnvironment(customizeEnvironmentView);
        }
        
        
        public void UpdateMiniFor_GameScene(HudView hudView, GameView gameView)
        {
            ConfiguratorModel model = ModelLocator.GetItem<ConfiguratorModel>();
            
            // Set Mode
            model.GameMode.Value = GameMode.Game;
            
            AddFeaturesForHud(hudView);
            AddFeaturesForGame(gameView);
        }

        
        private void AddFeaturesForGame(GameView gameView)
        {
            ConfiguratorModel model = ModelLocator.GetItem<ConfiguratorModel>();
            ConfiguratorService service = ServiceLocator.GetItem<ConfiguratorService>();

            ClearStaleFeatures();
            SceneSystemModule.Initialize(Context);
            
            //////////////////////////////////////////////
            //MENU: Now setup controller and view
            GameController gameController = new GameController(model, gameView, service);
            //
            ControllerLocator.AddItem(gameController);
            ViewLocator.AddItem(gameView);
            //
            gameView.Initialize(Context);
            gameController.Initialize(Context);
        }
        
        
        private void AddFeaturesForCustomizeCharacter(CustomizeCharacterView customizeCharacterView)
        {
            ConfiguratorModel model = ModelLocator.GetItem<ConfiguratorModel>();
            ConfiguratorService service = ServiceLocator.GetItem<ConfiguratorService>();
            
            ClearStaleFeatures();
            SceneSystemModule.Initialize(Context);
            
            //////////////////////////////////////////////
            //MENU: Now setup controller and view
            CustomizeCharacterController customizeCharacterController = 
                new CustomizeCharacterController(model, customizeCharacterView, service);
            //
            ControllerLocator.AddItem(customizeCharacterController);
            ViewLocator.AddItem(customizeCharacterView);
            //
            customizeCharacterView.Initialize(Context);
            customizeCharacterController.Initialize(Context);
        }

        
        private void AddFeaturesForCustomizeEnvironment(CustomizeEnvironmentView customizeEnvironmentView)
        {
            ConfiguratorModel model = ModelLocator.GetItem<ConfiguratorModel>();
            ConfiguratorService service = ServiceLocator.GetItem<ConfiguratorService>();
            
            ClearStaleFeatures();
            SceneSystemModule.Initialize(Context);
            
            //////////////////////////////////////////////
            //MENU: Now setup controller and view
            CustomizeEnvironmentController customizeEnvironmentController = 
                new CustomizeEnvironmentController(model, customizeEnvironmentView, service);
            //
            ControllerLocator.AddItem(customizeEnvironmentController);
            ViewLocator.AddItem(customizeEnvironmentView);
            //
            customizeEnvironmentView.Initialize(Context);
            customizeEnvironmentController.Initialize(Context);
        }
        
        
        private void AddFeaturesForMenu(MenuView menuView)
        {
            ConfiguratorModel model = ModelLocator.GetItem<ConfiguratorModel>();
            ConfiguratorService service = ServiceLocator.GetItem<ConfiguratorService>();

            ClearStaleFeatures();
            SceneSystemModule.Initialize(Context);
            
            //////////////////////////////////////////////
            //MENU: Now setup controller and view
            MenuController menuController = new MenuController(model, menuView, service);
            //
            ControllerLocator.AddItem(menuController);
            ViewLocator.AddItem(menuView);
            //
            menuView.Initialize(Context);
            menuController.Initialize(Context);
        }

        
        private void ClearStaleFeatures()
        {
            //////////////////////////////////////////////
            //Clear: Menu
            if (ControllerLocator.HasItem<MenuController>())
            {
                ControllerLocator.RemoveItem<MenuController>();
                ViewLocator.RemoveItem<MenuView>();
            }
            
            //////////////////////////////////////////////
            //Clear: Customize Character
            if (ControllerLocator.HasItem<CustomizeCharacterController>())
            {
                ControllerLocator.RemoveItem<CustomizeCharacterController>();
                ViewLocator.RemoveItem<CustomizeCharacterView>();
            }
            
            //////////////////////////////////////////////
            //Clear: Customize Environment
            if (ControllerLocator.HasItem<CustomizeEnvironmentController>())
            {
                ControllerLocator.RemoveItem<CustomizeEnvironmentController>();
                ViewLocator.RemoveItem<CustomizeEnvironmentView>();
            }
            
            //////////////////////////////////////////////
            //Clear: Game
            if (ControllerLocator.HasItem<GameController>())
            {
                ControllerLocator.RemoveItem<GameController>();
                ViewLocator.RemoveItem<GameView>();
            }
            
            //////////////////////////////////////////////
            //Clear: Hud
            if (ControllerLocator.HasItem<HudController>())
            {
                ControllerLocator.RemoveItem<HudController>();
                ViewLocator.RemoveItem<HudView>();
            }
        }

        private void AddFeaturesForHud(HudView hudView)
        {
            ConfiguratorModel model = ModelLocator.GetItem<ConfiguratorModel>();
            ConfiguratorService service = ServiceLocator.GetItem<ConfiguratorService>();
            
            //NOTE: Do not clear stale features here.
            
            //////////////////////////////////////////////
            //HUD: Now setup controller and view
            HudController hudController = new HudController(model, hudView, service);
            //
            ControllerLocator.AddItem(hudController);
            ViewLocator.AddItem(hudView);
            //
            hudView.Initialize(Context);
            hudController.Initialize(Context);
        }

        
        //  Event Handlers --------------------------------

    }
}