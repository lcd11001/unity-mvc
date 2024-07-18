using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Doozy.Editor.EditorUI;
using Doozy.Editor.EditorUI.Components;
using Doozy.Editor.EditorUI.Utils;
using Doozy.Editor.EditorUI.Windows.Internal;
using Doozy.Editor.UIElements;
using Doozy.Runtime.UIElements.Extensions;
using MonoMVCS.Editor.Editors;
using MonoMVCS.Runtime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;

namespace MonoMVCS.Editor.Windows
{
    public class MonoMVCSWindow : FluidWindow<MonoMVCSWindow>
    {
        private static readonly string WindowTitle = "Mono MVCS";

        [MenuItem("Mono MVCS/Create Scripts", priority = -1000)]
        public static void Open() => InternalOpenWindow(WindowTitle);

        protected override void OnEnable()
        {
            base.OnEnable();
            this.minSize = new Vector2(700, 700);
        }

        protected override void CreateGUI()
        {
            root.RecycleAndClear();

            var editor = (MonoMVCSSettingsEditor)UnityEditor.Editor.CreateEditor(MonoMVCSSettings.Instance);
            var editorRoot = editor.CreateInspectorGUI();
            editorRoot.Bind(editor.serializedObject);

            var createScriptButton = MonoMVCSSettingsEditor.GetButton("Create Scripts", EditorSpriteSheets.EditorUI.Icons.MagicWand, CreateScripts);

            editor
                .SetRootPadding(DesignUtils.k_Spacing2X)
                .ShowSavePath()
                .HideSaveSettings()
                .AddSpaceBlock(DesignUtils.k_Spacing)
                .AddChild(
                    DesignUtils.row
                    .AddFlexibleSpace()
                    .AddChild(createScriptButton)
                    .AddFlexibleSpace()
                )
                .AddSpaceBlock(DesignUtils.k_Spacing2X);

            root.Add(editorRoot);
        }

        private void CreateScripts()
        {
            var settings = MonoMVCSSettings.Instance;
            var path = settings.savePath;
            var className = settings.className;

            int fileSaved = 0;

            EnsureValidPath(path);

            fileSaved += CreateScript(path, settings.TemplateMVCS, className, "MVCS");
            fileSaved += CreateScript(path, settings.useModelLocator ? settings.TemplateModelLocator : settings.TemplateModel, className, "Model");
            fileSaved += CreateScript(path, settings.TemplateView, className, "View");
            fileSaved += CreateScript(path, settings.TemplateController, className, "Controller");
            fileSaved += CreateScript(path, settings.TemplateService, className, "Service");
            fileSaved += CreateScript(path, settings.TemplateCommand, className, "Commands");

            if (fileSaved > 0)
            {
                // show dialog box finished
                EditorUtility.DisplayDialog(WindowTitle, $"All template {fileSaved} files have been created!", "Close");
                // refresh Unity assets database at path
                AssetDatabase.Refresh();
            }
        }

        private int CreateScript(string path, string template, string className, string suffix)
        {
            var fileName = className + suffix + ".cs";
            var fullPath = Path.Join(path, fileName);
            var content = template.Replace("{{className}}", className);
            return Save(fullPath, content);
        }

        private void EnsureValidPath(string path)
        {
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private int Save(string fullPath, string content)
        {
            // check file exist
            if (File.Exists(fullPath))
            {
                // show dialog box overwrite content?
                if (!EditorUtility.DisplayDialog("WARNING", $"The file {Path.GetFileName(fullPath)} already exists.\nDo you want to overwrite its content?", "Yes", "No"))
                {
                    return 0;
                }
            }
            File.WriteAllText(fullPath, content);
            return 1;
        }
    }
}
