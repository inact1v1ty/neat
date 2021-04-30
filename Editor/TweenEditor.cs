using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;

namespace Neat.Tween.Editor
{
    public class TweenEditor<T> : UnityEditor.Editor
        where T: IEquatable<T>
    {
        Tween<T> tween;

        private List<(Component component, FieldInfo[] fields, PropertyInfo[] properties)> components;

        SerializedProperty animationNameProperty;
        SerializedProperty timeProperty;
        SerializedProperty animationCurveProperty;
        SerializedProperty targetProperty;
        SerializedProperty fieldProperty;

        void OnEnable()
        {
            tween = (Tween<T>)target;
            animationNameProperty = serializedObject.FindProperty("animationName");
            timeProperty = serializedObject.FindProperty("time");
            animationCurveProperty = serializedObject.FindProperty("animationCurve");

            targetProperty = serializedObject.FindProperty("target");
            fieldProperty = serializedObject.FindProperty("fieldName");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            Cache();

            var component = targetProperty.objectReferenceValue as Component;
            var field = fieldProperty.stringValue;

            EditorGUILayout.PropertyField(animationNameProperty);
            EditorGUILayout.PropertyField(timeProperty);
            EditorGUILayout.PropertyField(animationCurveProperty);

            GUIContent buttonContent;

            if (component == null || field == null || field == "")
            {
                buttonContent = new GUIContent("No field");
            }
            else
            {
                buttonContent = new GUIContent(component.GetType().Name + "." + field);
            }

            if (GUILayout.Button(buttonContent))
            {
                ShowSelectMenu();
            }

            serializedObject.ApplyModifiedProperties();
        }

        private void Cache()
        {
            components = new List<(Component, FieldInfo[], PropertyInfo[])>();
            foreach (var component in tween.gameObject.GetComponents<Component>())
            {
                if (component.GetType() != typeof(Tween<T>) && !component.GetType().IsSubclassOf(typeof(Tween<T>)))
                {
                    var type = component.GetType();
                    var fields = GetFields(type);
                    var properties = GetProperties(type);
                    if (fields.Length > 0 || properties.Length > 0)
                    {
                        components.Add((component, fields, properties));
                    }
                }
            }
        }

        private FieldInfo[] GetFields(Type type)
        {
            return type.GetAllFields()
                .Where(field => field.FieldType == typeof(T))
                .ToArray();
        }

        private PropertyInfo[] GetProperties(Type type)
        {
            return type.GetAllProperties()
                .Where(property =>
                    property.PropertyType == typeof(T) &&
                    property.CanRead &&
                    property.CanWrite)
                .ToArray();
        }

        private void ShowSelectMenu()
        {
            // Now create the menu, add items and show it
            GenericMenu menu = new GenericMenu();

            menu.AddItem(new GUIContent("No field"), false, OnDeselect);

            if (components.Count > 0)
            {
                menu.AddSeparator("");
            }

            for (int i = 0; i < components.Count; i++)
            {
                for (int j = 0; j < components[i].fields.Length; j++)
                {
                    menu.AddItem(new GUIContent(components[i].component.GetType().Name + "/" + components[i].fields[j].Name), false, OnSelect, (i, j, true));
                }

                if (components[i].fields.Length > 0 && components[i].properties.Length > 0)
                {
                    menu.AddSeparator(components[i].component.GetType().Name + "/");
                }

                for (int j = 0; j < components[i].properties.Length; j++)
                {
                    menu.AddItem(new GUIContent(components[i].component.GetType().Name + "/" + components[i].properties[j].Name), false, OnSelect, (i, j, false));
                }
            }
            menu.ShowAsContext();
            Event.current.Use();
        }

        private void OnDeselect()
        {
            targetProperty.objectReferenceValue = null;
            fieldProperty.stringValue = null;

            serializedObject.ApplyModifiedProperties();
        }

        private void OnSelect(object data)
        {
            var (i, j, isField) = ((int, int, bool))data;

            targetProperty.objectReferenceValue = components[i].component;

            if (isField)
            {
                fieldProperty.stringValue = components[i].fields[j].Name;
            }
            else
            {
                fieldProperty.stringValue = components[i].properties[j].Name;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}
