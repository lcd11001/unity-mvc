using UnityEngine;

namespace MonoMVCS.Runtime
{
    [CreateAssetMenu(fileName = "MonoMVCSSettings", menuName = "Mono MVCS/Settings", order = -1000)]
    public class MonoMVCSSettings : ScriptableObject
    {
        #region Fields
        public string className = "MVCS";
        public bool useModelLocator = true;

        [HideInInspector]
        public string savePath = "";

        [Space(20)]
        public TextAsset templateMVCS;
        public TextAsset templateModel;
        public TextAsset templateModelLocator;
        public TextAsset templateView;
        public TextAsset templateController;
        public TextAsset templateService;
        public TextAsset templateCommand;

        #endregion

        #region Properties
        public string TemplateMVCS => templateMVCS.text;
        public string TemplateModel => templateModel.text;
        public string TemplateModelLocator => templateModelLocator.text;
        public string TemplateView => templateView.text;
        public string TemplateController => templateController.text;
        public string TemplateService => templateService.text;
        public string TemplateCommand => templateCommand.text;

        #endregion

        #region Singleton

        private static string assetName => nameof(MonoMVCSSettings);
        private static MonoMVCSSettings _instance;
        public static MonoMVCSSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<MonoMVCSSettings>(assetName);
                    if (_instance == null)
                    {
                        _instance = CreateInstance<MonoMVCSSettings>();
#if UNITY_EDITOR
                        string path = $"Assets/Resources/{assetName}.asset";
                        UnityEditor.AssetDatabase.CreateAsset(_instance, path);
#endif
                    }
                }

                return _instance;
            }
        }

        #endregion
    }
}