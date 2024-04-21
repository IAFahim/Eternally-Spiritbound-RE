using _Root.Scripts.Datas.Runtime.Variables;
using UnityEditor;
using UnityEngine;

namespace _Root.Scripts.Datas.Editor.Variables
{
    [CustomPropertyDrawer(typeof(Reactive<>))]
    public class ReactivePropertyDrawer: PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("value");
            return EditorGUI.GetPropertyHeight(valueProperty);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("value");
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.PropertyField(position, valueProperty, label, true);
            EditorGUI.EndDisabledGroup();
            EditorGUI.EndProperty();
        }
    }
}