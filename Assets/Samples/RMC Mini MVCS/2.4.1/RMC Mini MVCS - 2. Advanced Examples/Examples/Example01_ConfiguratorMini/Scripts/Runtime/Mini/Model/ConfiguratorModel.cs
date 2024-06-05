using RMC.Core.Architectures.Mini.Context;
using RMC.Core.Architectures.Mini.Model;
using RMC.MiniMvcs.Samples.Configurator.Mini.Model.Data;

namespace RMC.MiniMvcs.Samples.Configurator.Mini.Model
{
    //  Namespace Properties ------------------------------
    public enum GameMode
    {
        Menu,
        Game,
        CustomizeCharacter,
        CustomizeEnvironment
    }
    
    //  Class Attributes ----------------------------------

    /// <summary>
    /// The Model stores runtime data 
    /// </summary>
    public class ConfiguratorModel: BaseModel // Extending 'base' is optional
    {
        //  Properties ------------------------------------
        public Observable<GameMode> GameMode { get { return _gameMode;} }
        public Observable<bool> HasLoadedService { get { return _hasLoadedService;} }
        public Observable<CharacterData> CharacterData { get { return _characterData;} }
        public Observable<EnvironmentData> EnvironmentData { get { return _environmentData;} }
        
        
        //  Fields ----------------------------------------
        private readonly Observable<GameMode> _gameMode = new Observable<GameMode>();
        private readonly Observable<bool> _hasLoadedService = new Observable<bool>();
        private readonly Observable<CharacterData> _characterData = new Observable<CharacterData>();
        private readonly Observable<EnvironmentData> _environmentData = new Observable<EnvironmentData>();
        
        public const string Scene01_Menu = "Scene01_Menu";
        public const string Scene02_CustomizeCharacter = "Scene02_CustomizeCharacter";
        public const string Scene03_CustomizeEnvironment = "Scene03_CustomizeEnvironment";
        public const string Scene04_Game = "Scene04_Game";
        
        
        //  Initialization  -------------------------------
        public override void Initialize(IContext context) 
        {
            if (!IsInitialized)
            {
                base.Initialize(context);

                // Set Defaults
                _hasLoadedService.Value = false;
                _characterData.Value = null;
                _environmentData.Value = null;
            }
        }

        
        //  Methods ---------------------------------------
 

        //  Event Handlers --------------------------------
    }
}