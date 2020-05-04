#if UNITY_2018_4_OR_NEWER
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// The base Unity Atoms property drawer. Makes it possible to create and add a new Atom via Unity's inspector. Only availble in `UNITY_2018_4_OR_NEWER`.
    /// </summary>
    /// <typeparam name="T">The type of Atom the property drawer should apply to.</typeparam>
    public abstract class AtomDrawer<T> : PropertyDrawer where T : ScriptableObject
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects)
            {
                return EditorGUI.GetPropertyHeight(property);
            }

            return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.serializedObject.isEditingMultipleObjects)
            {
                EditorGUI.PropertyField(position, property, label, true);
                return;
            }

            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            property.objectReferenceValue =
                EditorGUI.ObjectField(position, property.objectReferenceValue, typeof(T), false);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
#endif
