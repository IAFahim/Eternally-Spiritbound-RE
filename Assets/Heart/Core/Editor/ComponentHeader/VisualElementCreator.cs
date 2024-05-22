﻿using System;
using System.Reflection;
using Pancake;
using PancakeEditor.Common;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

namespace PancakeEditor.ComponentHeader
{
    internal static class VisualElementCreator
    {
        private static Texture2D GetIcon(ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.Remove:
                    return EditorResources.IconRemoveComponent(Uniform.Theme);
                case ButtonType.MoveUp:
                    return EditorResources.IconMoveUp(Uniform.Theme);
                case ButtonType.MoveDown:
                    return EditorResources.IconMoveDown(Uniform.Theme);
                case ButtonType.PasteComponentValue:
                    return EditorResources.IconPasteComponentValues(Uniform.Theme);
                case ButtonType.CopyComponent:
                    return EditorResources.IconCopyComponent(Uniform.Theme);
                case ButtonType.LoadComponent:
                    return EditorResources.IconLightComponent(Uniform.Theme);
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, null);
            }
        }

        public static VisualElement CreateContainer(string headerElementName, Action onRefresh)
        {
            var container = new VisualElement {pickingMode = PickingMode.Ignore, style = {flexDirection = FlexDirection.RowReverse, height = 22,}};

            var imageCreator = new ImageCreator(headerElementName, onRefresh);
            var removeComponentImage = imageCreator.CreateButton(ButtonType.Remove, Undo.DestroyObjectImmediate);
            var moveUpElement = imageCreator.CreateButton(ButtonType.MoveUp, x => ComponentUtility.MoveComponentUp(x));
            var moveDownElement = imageCreator.CreateButton(ButtonType.MoveDown, x => ComponentUtility.MoveComponentDown(x));
            var copyComponentElement = imageCreator.CreateButton(ButtonType.CopyComponent, CopyComponent);
            var pasteComponentValuesElement = imageCreator.CreateButton(ButtonType.PasteComponentValue, PasteComponentValues);
            var loadComponentElement = imageCreator.CreateButton(ButtonType.LoadComponent, LoadComponent);

            container.Add(removeComponentImage);
            container.Add(moveUpElement);
            container.Add(moveDownElement);
            container.Add(pasteComponentValuesElement);
            container.Add(copyComponentElement);
            container.Add(loadComponentElement);

            return container;
        }

        private static void LoadComponent(Component component)
        {
            if (component.GetType().IsSubclassOf(typeof(GameComponent)))
            {
                var methodInfo = component.GetType().GetMethod("OnLoadComponents", BindingFlags.Instance | BindingFlags.NonPublic);
                if (methodInfo != null)
                {
                    methodInfo.Invoke(component, null);
                    TooltipWindow.Show("Component Loaded!");
                }

                EditorUtility.SetDirty(component);
            }
            else
            {
                TooltipWindow.Show("Nothing happens, Need inherit GameComponent!");
            }
        }

        private static void CopyComponent(Component component)
        {
            ComponentUtility.CopyComponent(component);
            Debug.Log($"Copied! '{component.GetType().Name}'");
            TooltipWindow.Show("Copied!");
        }

        private static void PasteComponentValues(Component component)
        {
            ComponentUtility.PasteComponentValues(component);
            Debug.Log($"Pasted! '{component.GetType().Name}'");
            TooltipWindow.Show("Pasted!");
        }

        private sealed class ImageCreator
        {
            private readonly string _headerElementName;
            private readonly Action _onRefresh;

            public ImageCreator(string headerElementName, Action onRefresh)
            {
                _headerElementName = headerElementName;
                _onRefresh = onRefresh;
            }

            public Button CreateButton(ButtonType buttonType, Action<Component> action)
            {
                var button = new Button(() =>
                {
                    string componentName = _headerElementName switch
                    {
                        "TextMeshPro - TextHeader" => "TextMeshPro",
                        "TextMeshPro - Text (UI)Header" => "TextMeshProUGUI",

                        _ => _headerElementName.Remove(_headerElementName.Length - 6, 6).Replace(" ", "").Replace("(Script)", "")
                    };

                    foreach (var gameObject in Selection.gameObjects)
                    {
                        var component = gameObject.GetComponent(componentName);

                        action(component);
                    }

                    _onRefresh();
                })
                {
                    style =
                    {
                        position = Position.Relative,
                        backgroundImage = GetIcon(buttonType),
                        backgroundColor = Color.clear,
                        borderTopWidth = 0,
                        borderBottomWidth = 0,
                        borderLeftWidth = 0,
                        borderRightWidth = 0,
                        paddingLeft = 0,
                        paddingRight = 0,
                        paddingTop = 0,
                        paddingBottom = 0,
                        marginLeft = 0,
                        marginRight = 0,
                        marginTop = 0,
                        marginBottom = 0,
                        top = 3,
                        right = 63 + (int) buttonType * 3,
                        width = 16,
                        height = 16,
                    }
                };

                return button;
            }
        }
    }
}