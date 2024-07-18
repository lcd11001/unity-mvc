using System;
using System.Collections;
using System.Collections.Generic;
using Doozy.Runtime.UIElements.Extensions;
using Doozy.Editor.EditorUI;
using Doozy.Editor.EditorUI.Components;
using MonoMVCS.Runtime;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Doozy.Editor.EditorUI.Utils;
using System.Reflection;
using UnityEngine.Events;
using Doozy.Runtime.Reactor.Extensions;
using UnityEditor.VersionControl;

namespace MonoMVCS.Editor.Editors
{
    [CustomEditor(typeof(MonoMVCSSettings), true)]
    public class MonoMVCSSettingsEditor : UnityEditor.Editor
    {
        private VisualElement root;
        private VisualElement saveSettings;

        public override VisualElement CreateInspectorGUI()
        {
            FindSerializedProperties();
            InitializeEditor();
            Compose();

            return root;
        }

        #region Serialized Properties
        private SerializedProperty propertyClassName { get; set; }
        private SerializedProperty propertyUseModelLocator { get; set; }
        private SerializedProperty propertyTemplateMVCS { get; set; }
        private SerializedProperty propertyTemplateModel { get; set; }
        private SerializedProperty propertyTemplateModelLocator { get; set; }
        private SerializedProperty propertyTemplateView { get; set; }
        private SerializedProperty propertyTemplateController { get; set; }
        private SerializedProperty propertyTemplateService { get; set; }
        private SerializedProperty propertyTemplateCommand { get; set; }

        private SerializedProperty propertySavePath { get; set; }

        private void FindSerializedProperties()
        {
            propertyClassName = serializedObject.FindProperty(nameof(MonoMVCSSettings.className));
            propertyUseModelLocator = serializedObject.FindProperty(nameof(MonoMVCSSettings.useModelLocator));

            propertyTemplateMVCS = serializedObject.FindProperty(nameof(MonoMVCSSettings.templateMVCS));
            propertyTemplateModel = serializedObject.FindProperty(nameof(MonoMVCSSettings.templateModel));
            propertyTemplateModelLocator = serializedObject.FindProperty(nameof(MonoMVCSSettings.templateModelLocator));
            propertyTemplateView = serializedObject.FindProperty(nameof(MonoMVCSSettings.templateView));
            propertyTemplateController = serializedObject.FindProperty(nameof(MonoMVCSSettings.templateController));
            propertyTemplateService = serializedObject.FindProperty(nameof(MonoMVCSSettings.templateService));
            propertyTemplateCommand = serializedObject.FindProperty(nameof(MonoMVCSSettings.templateCommand));

            propertySavePath = serializedObject.FindProperty(nameof(MonoMVCSSettings.savePath));
        }

        #endregion

        #region Initialize Editor

        private FluidComponentHeader fluidHeader { get; set; }

        private FluidField fluidClassName { get; set; }
        private FluidField fluidUseModelLocator { get; set; }

        private FluidField fluidTemplateMVCS { get; set; }
        private FluidField fluidTemplateModel { get; set; }
        private FluidField fluidTemplateModelLocator { get; set; }
        private FluidField fluidTemplateView { get; set; }
        private FluidField fluidTemplateController { get; set; }
        private FluidField fluidTemplateService { get; set; }
        private FluidField fluidTemplateCommand { get; set; }

        private FluidField fluidSavePath { get; set; }

        private void InitializeEditor()
        {
            root = new VisualElement();

            saveSettings = DesignUtils.row
                .AddFlexibleSpace()
                .AddChild(
                    GetButton("Save Settings", EditorSpriteSheets.EditorUI.Icons.Save, () =>
                    {
                        serializedObject.ApplyModifiedProperties();
                        EditorUtility.SetDirty(target);
                        AssetDatabase.SaveAssetIfDirty(target);
                    })
                )
                .AddFlexibleSpace();

            fluidHeader = FluidComponentHeader.Get()
                .SetComponentNameText(nameof(MonoMVCSSettings).GetWords())
                .SetIcon(EditorSpriteSheets.EditorUI.Icons.Settings)
                .SetAccentColor(EditorColors.EditorUI.DeepPurple);

            fluidClassName = InitTextField(propertyClassName, EditorSpriteSheets.UIManager.UIMenu.UIPack);
            //fluidUseModelLocator = InitToggleField(propertyUseModelLocator, EditorSpriteSheets.EditorUI.Icons.QuestionMark);
            fluidUseModelLocator = InitSwitchField(propertyUseModelLocator, EditorSpriteSheets.EditorUI.Icons.QuestionMark);

            fluidTemplateMVCS = InitObjectField(propertyTemplateMVCS, typeof(TextAsset), EditorSpriteSheets.EditorUI.Icons.Scripting);
            fluidTemplateModel = InitObjectField(propertyTemplateModel, typeof(TextAsset), EditorSpriteSheets.EditorUI.Icons.Scripting);
            fluidTemplateModelLocator = InitObjectField(propertyTemplateModelLocator, typeof(TextAsset), EditorSpriteSheets.EditorUI.Icons.Scripting);
            fluidTemplateView = InitObjectField(propertyTemplateView, typeof(TextAsset), EditorSpriteSheets.EditorUI.Icons.Scripting);
            fluidTemplateController = InitObjectField(propertyTemplateController, typeof(TextAsset), EditorSpriteSheets.EditorUI.Icons.Scripting);
            fluidTemplateService = InitObjectField(propertyTemplateService, typeof(TextAsset), EditorSpriteSheets.EditorUI.Icons.Scripting);
            fluidTemplateCommand = InitObjectField(propertyTemplateCommand, typeof(TextAsset), EditorSpriteSheets.EditorUI.Icons.Scripting);

            fluidSavePath = InitRowTextField(propertySavePath, EditorSpriteSheets.EditorUI.Icons.Load, EditorSpriteSheets.EditorUI.Icons.More, () =>
            {
                var path = EditorUtility.OpenFolderPanel("Select Folder", propertySavePath.stringValue, "");
                if (!string.IsNullOrEmpty(path))
                {
                    propertySavePath.stringValue = path;
                    serializedObject.ApplyModifiedProperties();
                }
            });
            if (CheckHideInInspectorAttribute(propertySavePath))
            {
                fluidSavePath.Hide();
            }
        }

        private bool CheckHideInInspectorAttribute(SerializedProperty property)
        {
            // if you're trying to access a serialized field, not a property, 
            // you should use GetField instead of GetProperty
            var propertyInfo = target.GetType().GetField(property.name, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (propertyInfo != null)
            {
                var attributes = propertyInfo.GetCustomAttributes(typeof(HideInInspector), true);
                if (attributes.Length > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public static FluidField InitObjectField(SerializedProperty property, Type type, List<Texture2D> fieldIcons, bool allowSceneObjects = false)
        {
            return FluidField.Get()
                .SetLabelText(property.displayName.GetWords())
                .SetTooltip($"{property.displayName} {type.Name}")
                .SetIcon(fieldIcons)
                .SetStyleFlexGrow(1)
                .AddFieldContent(
                    DesignUtils.row
                        .AddChild(
                            DesignUtils
                            .NewObjectField(
                                property,
                                type,
                                allowSceneObjects)
                            .SetStyleFlexGrow(1)
                        )
                );
        }

        public static FluidField InitTextField(SerializedProperty property, List<Texture2D> fieldIcons, string tooltip = null)
        {
            return FluidField.Get()
                .SetLabelText(property.displayName.GetWords())
                .SetTooltip(tooltip ?? $"{property.displayName} Text")
                .SetIcon(fieldIcons)
                .SetStyleFlexGrow(1)
                .AddFieldContent(
                    DesignUtils.row
                        .AddChild(
                            DesignUtils
                            .NewTextField(
                                property,
                                true)
                            .SetStyleFlexGrow(1)
                        )
                );
        }

        public static FluidField InitRowTextField(SerializedProperty property, List<Texture2D> fieldIcons, List<Texture2D> buttonIcons, UnityAction callback = null)
        {
            var folderButton = GetSmallButton(buttonIcons)
                // .SetAccentColor(EditorSelectableColors.Default.Add)
                .SetOnClick(callback);

            var textField = InitTextField(property, fieldIcons, $"{property.displayName} Folder");
            // insert the folder button at the end of the row
            textField.fieldContent.ElementAt(0).AddChild(folderButton);

            return textField;
        }

        public static FluidField InitToggleField(SerializedProperty property, List<Texture2D> fieldIcons, string tooltip = null)
        {
            return FluidField.Get()
                .SetLabelText(property.displayName.GetWords())
                .SetTooltip(tooltip ?? $"{property.displayName} Toggle")
                .SetIcon(fieldIcons)
                .SetStyleFlexGrow(1)
                .AddFieldContent(
                    DesignUtils.row
                        .AddChild(
                            DesignUtils
                            .NewToggle(property)
                            .SetStyleFlexGrow(1)
                        )
                );
        }

        public static FluidField InitSwitchField(SerializedProperty property, List<Texture2D> fieldIcons, string tooltip = null)
        {
            var toggle = DesignUtils.NewToggle(property);
            var toggleSwitch = DesignUtils.GetEnableDisableSwitch(property, toggle, EditorSelectableColors.Default.Action);

            return FluidField.Get()
                .SetLabelText(property.displayName.GetWords())
                .SetTooltip(tooltip ?? $"{property.displayName} Switch")
                .SetIcon(fieldIcons)
                .SetStyleFlexGrow(1)
                .AddFieldContent(toggleSwitch);
        }

        public static FluidButton GetSmallButton(List<Texture2D> icons) => FluidButton
            .Get()
            .SetButtonStyle(ButtonStyle.Contained)
            .SetElementSize(ElementSize.Tiny)
            .SetStyleFlexShrink(0)
            .SetAccentColor(EditorSelectableColors.Default.ButtonIcon)
            .SetIcon(icons);

        public static FluidButton GetButton(string text, List<Texture2D> icons, UnityAction callback = null) => FluidButton
            .Get()
            .SetButtonStyle(ButtonStyle.Outline)
            .SetElementSize(ElementSize.Normal)
            .SetLabelText(text)
            .SetAccentColor(EditorSelectableColors.Default.Action)
            .SetIcon(icons)
            .SetOnClick(callback);

        #endregion

        #region Compose

        private void Compose()
        {
            root
                .AddChild(fluidHeader)
                .AddSpaceBlock(DesignUtils.k_Spacing2X)

                .AddChild(fluidClassName)
                .AddSpaceBlock(DesignUtils.k_Spacing)

                .AddChild(fluidUseModelLocator)
                .AddSpaceBlock(DesignUtils.k_Spacing2X)

                .AddChild(fluidTemplateMVCS)
                .AddSpaceBlock(DesignUtils.k_Spacing)

                .AddChild(fluidTemplateModel)
                .AddSpaceBlock(DesignUtils.k_Spacing)

                .AddChild(fluidTemplateModelLocator)
                .AddSpaceBlock(DesignUtils.k_Spacing)

                .AddChild(fluidTemplateView)
                .AddSpaceBlock(DesignUtils.k_Spacing)

                .AddChild(fluidTemplateController)
                .AddSpaceBlock(DesignUtils.k_Spacing)

                .AddChild(fluidTemplateService)
                .AddSpaceBlock(DesignUtils.k_Spacing)

                .AddChild(fluidTemplateCommand)
                .AddSpaceBlock(DesignUtils.k_Spacing)

                .AddChild(fluidSavePath)

                .AddChild(saveSettings)
            ;
        }

        #endregion

        #region Chain Methods

        public MonoMVCSSettingsEditor HideHeader()
        {
            fluidHeader.Hide();
            return this;
        }

        public MonoMVCSSettingsEditor ShowHeader()
        {
            fluidHeader.Show();
            return this;
        }

        public MonoMVCSSettingsEditor HideSavePath()
        {
            fluidSavePath.Hide();
            return this;
        }

        public MonoMVCSSettingsEditor ShowSavePath()
        {
            fluidSavePath.Show();
            return this;
        }

        public MonoMVCSSettingsEditor HideSaveSettings()
        {
            saveSettings.Hide();
            return this;
        }

        public MonoMVCSSettingsEditor ShowSaveSettings()
        {
            saveSettings.Show();
            return this;
        }

        public MonoMVCSSettingsEditor SetRootPadding(float value)
        {
            root.SetStylePadding(value);
            return this;
        }

        public MonoMVCSSettingsEditor AddChild(VisualElement child)
        {
            root.Add(child);
            return this;
        }

        public MonoMVCSSettingsEditor AddSpaceBlock(int value)
        {
            root.AddSpaceBlock(value);
            return this;
        }

        #endregion

    }
}
