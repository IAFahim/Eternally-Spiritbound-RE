﻿using System.IO;
using PancakeEditor.Common;
using UnityEditor;
using UnityEngine;

namespace PancakeEditor.Scriptable
{
    public class CreateTypePopUpWindow : PopupWindowContent
    {
        private readonly Rect _position;
        private string _typeText = "Type";
        private bool _baseClass;
        private bool _monoBehaviour;
        private bool _variable = true;
        private bool _event = true;
        private bool _eventListener = true;
        private bool _list = true;
        private bool _invalidTypeName = true;
        private string _path;
        private readonly Vector2 _dimensions = new Vector2(350, 350);
        private int _destinationFolderIndex = 0;
        private readonly string[] _destinationFolderOptions = {"Default", "Custom"};
        private const string DESTINATION_FOLDER_INDEX_KEY = "editor_scriptable_destination_folder_index";
        private const string DESTINATION_FOLDER_PATH_KEY = "editor_scriptable_destination_folder_path";

        public override Vector2 GetWindowSize() => _dimensions;

        public CreateTypePopUpWindow(Rect origin) { _position = origin; }

        public override void OnGUI(Rect rect)
        {
            editorWindow.position = Uniform.CenterInWindow(editorWindow.position, _position);

            //Uniform.DrawHeader("Create new Type");
            DrawTextField();
            DrawTypeToggles();
            GUILayout.Space(20);
            DrawPath();
            GUILayout.Space(5);
            DrawButtons();
        }

        private void DrawPath()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Destination Path:", EditorStyles.boldLabel);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();

            EditorGUI.BeginChangeCheck();
            _destinationFolderIndex = GUILayout.SelectionGrid(_destinationFolderIndex, _destinationFolderOptions, 2);
            if (EditorGUI.EndChangeCheck()) EditorPrefs.SetInt(DESTINATION_FOLDER_INDEX_KEY, _destinationFolderIndex);
            if (_destinationFolderIndex == 0)
            {
                _path = ProjectDatabase.DEFAULT_PATH_SCRIPT_GENERATED;
                EditorGUILayout.LabelField($"{_path}", new GUIStyle(EditorStyles.label) {fontStyle = FontStyle.Italic});
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                _path = EditorGUILayout.TextField(_path);
                if (EditorGUI.EndChangeCheck()) EditorPrefs.SetString(DESTINATION_FOLDER_PATH_KEY, _path);
            }
        }

        private void DrawTextField()
        {
            EditorGUI.BeginChangeCheck();
            _typeText = EditorGUILayout.TextField(_typeText, EditorStyles.textField);
            if (EditorGUI.EndChangeCheck()) _invalidTypeName = !IsTypeNameValid();

            var guiStyle = new GUIStyle(EditorStyles.label) {normal = {textColor = _invalidTypeName ? Uniform.SunsetOrange : Color.white}, fontStyle = FontStyle.Bold};
            string errorMessage = _invalidTypeName ? "Invalid type name." : "";
            EditorGUILayout.LabelField(errorMessage, guiStyle);
        }

        private void DrawTypeToggles()
        {
            var nameType = $"{_typeText.ToCamelCase()}";
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(10);
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            if (!PancakeEditor.Common.Editor.IsBuiltInType(_typeText))
            {
                DrawToggle(ref _baseClass,
                    nameType,
                    "",
                    EditorGUIUtility.IconContent("cs Script Icon").image,
                    true,
                    140);
                if (_baseClass) _monoBehaviour = GUILayout.Toggle(_monoBehaviour, "MonoBehaviour?");
            }

            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
            DrawToggle(ref _variable,
                nameType,
                "Variable",
                EditorResources.IconVariable,
                true);
            GUILayout.Space(5);
            DrawToggle(ref _event, "ScriptableEvent", nameType, EditorResources.IconEvent);
            GUILayout.Space(5);
            if (!_event) _eventListener = false;
            GUI.enabled = _event;
            DrawToggle(ref _eventListener, "EventListener", nameType, EditorResources.IconEventListener);
            GUI.enabled = true;
            GUILayout.Space(5);
            DrawToggle(ref _list, "ScriptableList", nameType, EditorResources.IconList);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
        }

        private void DrawToggle(ref bool toggleValue, string typeName, string second, Texture icon, bool isFirstRed = false, int maxWidth = 200)
        {
            EditorGUILayout.BeginHorizontal();
            var style = new GUIStyle(GUIStyle.none);
            GUILayout.Box(icon, style, GUILayout.Width(18), GUILayout.Height(18));
            toggleValue = GUILayout.Toggle(toggleValue, "", GUILayout.Width(maxWidth));
            var firstStyle = new GUIStyle(GUI.skin.label) {padding = {left = 15 - maxWidth}};
            if (isFirstRed) firstStyle.normal.textColor = Uniform.SunsetOrange;
            GUILayout.Label(typeName, firstStyle);
            var secondStyle = new GUIStyle(GUI.skin.label) {padding = {left = -6}};
            if (!isFirstRed) secondStyle.normal.textColor = Uniform.SunsetOrange;
            GUILayout.Label(second, secondStyle);
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
        }

        private void DrawButtons()
        {
            GUI.enabled = !_invalidTypeName;
            if (GUILayout.Button("Create", GUILayout.MaxHeight(ScriptableEditorSetting.BUTTON_HEIGHT)))
            {
                if (!IsTypeNameValid()) return;

                TextAsset newFile = null;
                var progress = 0f;
                EditorUtility.DisplayProgressBar("Progress", "Start", progress);

                if (_baseClass && !PancakeEditor.Common.Editor.IsBuiltInType(_typeText))
                {
                    string template = _monoBehaviour ? EditorResources.MonoBehaviourTemplate.text : EditorResources.ClassTemplate.text;
                    newFile = CreateNewClass(template, _typeText, $"{_typeText}.cs", _path);
                    if (newFile == null)
                    {
                        Close();
                        return;
                    }
                }

                progress += 0.2f;
                EditorUtility.DisplayProgressBar("Progress", "Generating...", progress);

                if (_variable)
                {
                    newFile = CreateNewClass(EditorResources.ScriptableVariableTemplate.text, _typeText, $"{_typeText}Variable.cs", _path);
                    if (newFile == null)
                    {
                        Close();
                        return;
                    }
                }

                progress += 0.2f;
                EditorUtility.DisplayProgressBar("Progress", "Generating...", progress);

                if (_event)
                {
                    newFile = CreateNewClass(EditorResources.ScriptableEventTemplate.text,
                        _typeText,
                        $"{nameof(EditorResources.ScriptableEventTemplate).Replace("Template", _typeText)}.cs",
                        _path);
                    if (newFile == null)
                    {
                        Close();
                        return;
                    }
                }

                progress += 0.2f;
                EditorUtility.DisplayProgressBar("Progress", "Generating...", progress);

                if (_eventListener)
                {
                    newFile = CreateNewClass(EditorResources.ScriptableEventListenerTemplate.text,
                        _typeText,
                        $"{nameof(EditorResources.ScriptableEventListenerTemplate).Replace("Template", _typeText)}.cs",
                        _path);
                    if (newFile == null)
                    {
                        Close();
                        return;
                    }
                }

                progress += 0.2f;
                EditorUtility.DisplayProgressBar("Progress", "Generating...", progress);

                if (_list)
                {
                    newFile = CreateNewClass(EditorResources.ScriptableListTemplate.text,
                        _typeText,
                        $"{nameof(EditorResources.ScriptableListTemplate).Replace("Template", _typeText)}.cs",
                        _path);
                    if (newFile == null)
                    {
                        Close();
                        return;
                    }
                }

                progress += 0.2f;
                EditorUtility.DisplayProgressBar("Progress", "Completed!", progress);

                EditorUtility.DisplayDialog("Success", $"{_typeText} was created!", "OK");
                Close(false);
                EditorGUIUtility.PingObject(newFile);
            }

            if (GUILayout.Button("Cancel", GUILayout.MaxHeight(ScriptableEditorSetting.BUTTON_HEIGHT))) editorWindow.Close();
        }

        private void Close(bool hasError = true)
        {
            EditorUtility.ClearProgressBar();
            editorWindow.Close();
            if (hasError)
                EditorUtility.DisplayDialog("Error", $"Failed to create {_typeText}", "OK");
        }

        private bool IsTypeNameValid()
        {
            var valid = System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(_typeText);
            return valid;
        }

        private TextAsset CreateNewClass(string contentTemplate, string typeName, string fileName, string path)
        {
            contentTemplate = contentTemplate.Replace("#TYPE#", typeName);
            try
            {
                var newFile = EditorCreator.CreateTextFile(contentTemplate, fileName, path);
                return newFile;
            }
            catch (IOException e)
            {
                EditorUtility.DisplayDialog("Could not create class", e.Message, "OK");
                return null;
            }
        }
    }
}