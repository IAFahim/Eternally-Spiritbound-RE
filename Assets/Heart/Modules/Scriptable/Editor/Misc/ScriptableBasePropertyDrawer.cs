﻿using Pancake.ExLibEditor;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Pancake.ScriptableEditor
{
    [CustomPropertyDrawer(typeof(ScriptableObject), true)]
    public class ScriptableBasePropertyDrawer : PropertyDrawer
    {
        private Editor _editor;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var targetObject = property.objectReferenceValue;
            if (targetObject == null)
            {
                DrawIfNull(position, property, label);
                return;
            }

            bool isInCollection = fieldInfo.FieldType.IsCollectionType();
            DrawIfNotNull(position,
                property,
                label,
                targetObject,
                isInCollection);

            EditorGUI.EndProperty();
        }

        protected void DrawIfNull(Rect position, SerializedProperty property, GUIContent label)
        {
            if (fieldInfo.FieldType.IsAbstract)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            //Draw property and a create button
            var rect = DrawPropertyField(position, property, label);
            var guiContent = new GUIContent("Create", "Creates the SO at default storage generate path");

            if (GUI.Button(rect, guiContent))
            {
                string newName = GetFieldName().ToSnakeCase();
                var typeCreate = fieldInfo.FieldType;

                var elementType = fieldInfo.FieldType.GetCorrectElementType();
                if (elementType != null) typeCreate = elementType;
#pragma warning disable CS0612
                property.objectReferenceValue = EditorCreator.CreateScriptableAt(typeCreate,
                    newName,
                    ProjectDatabase.DEFAULT_PATH_SCRIPTABLE_ASSET_GENERATED,
                    HeartEditorSettings.EditorNameCreationMode == ECreationMode.Auto);
#pragma warning restore CS0612
            }

            EditorGUI.EndProperty();
        }

        protected Rect DrawPropertyField(Rect position, SerializedProperty property, GUIContent label)
        {
            var rectPosition = position;
            const float propertyWidthRatio = 0.82f;
            rectPosition.width = position.width * propertyWidthRatio;
            EditorGUI.PropertyField(rectPosition, property, label);

            rectPosition.x += rectPosition.width + 5f;
            rectPosition.width = position.width * (1 - propertyWidthRatio) - 5f;
            return rectPosition;
        }

        protected void DrawIfNotNull(Rect position, SerializedProperty property, GUIContent label, Object targetObject, bool isInCollection)
        {
            var rect = position;
            var labelRect = position;
            labelRect.width = position.width * 0.4f; //only expands on the first half on the window when clicked

            if (!isInCollection)
            {
                property.isExpanded = EditorGUI.Foldout(labelRect, property.isExpanded, new GUIContent(""), true);
                int indent = EditorGUI.indentLevel;
                if (property.isExpanded)
                {
                    //Draw an embedded inspector
                    rect.width = position.width;
                    EditorGUI.PropertyField(rect, property, label);
                    var cacheBgColor = GUI.backgroundColor;
                    GUI.backgroundColor = Uniform.FieryRose;
                    GUILayout.BeginVertical(GUI.skin.box);
                    if (_editor == null) Editor.CreateCachedEditor(targetObject, null, ref _editor);
                    _editor.OnInspectorGUI();
                    GUI.backgroundColor = cacheBgColor;
                    GUILayout.EndVertical();
                    EditorGUI.indentLevel = indent;
                }
                else
                {
                    _editor = null;
                    DrawUnExpanded(position, property, label, targetObject);
                }
            }
            else DrawUnExpanded(position, property, label, targetObject);
        }

        protected virtual string GetFieldName() { return fieldInfo.Name; }

        protected virtual void DrawUnExpanded(Rect position, SerializedProperty property, GUIContent label, Object targetObject)
        {
            if (property.serializedObject.isEditingMultipleObjects || property.objectReferenceValue == null)
            {
                base.OnGUI(position, property, label);
                return;
            }

            var inner = new SerializedObject(property.objectReferenceValue);
            var valueProp = inner.FindProperty("value");

            if (valueProp == null || valueProp.propertyType == SerializedPropertyType.Generic)
            {
                EditorGUI.PropertyField(position, property, label);
                inner.ApplyModifiedProperties();
                return;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            var previewRect = new Rect(position) {width = GetPreviewSpace(valueProp.type)};
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            position.xMin = previewRect.xMax;
            EditorGUI.PropertyField(previewRect, valueProp, GUIContent.none, false);

            position.x += 6f;
            position.width -= 6f;
            EditorGUI.PropertyField(position, property, GUIContent.none);

            EditorGUI.indentLevel = indent;

            inner.ApplyModifiedProperties();
            EditorGUI.EndProperty();
        }

        private float GetPreviewSpace(string type)
        {
            switch (type)
            {
                case "Vector2":
                case "Vector2Int":
                case "Vector3":
                    return 128;
                default:
                    return 58;
            }
        }
    }
}