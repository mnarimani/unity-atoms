using System.Diagnostics;
using UnityEngine;

namespace UnityAtoms
{
    public static class MultiScriptableObject
    {
        [Conditional("UNITY_EDITOR")]
        public static void AddScriptableObject<T>(ScriptableObject obj, ref T field, string soName) where T : ScriptableObject
        {
            var instance = ScriptableObject.CreateInstance<T>();
            instance.name = soName;
            UnityEditor.AssetDatabase.AddObjectToAsset(instance, obj);
            UnityEditor.AssetDatabase.SaveAssets();
            field = instance;
        }

        [Conditional("UNITY_EDITOR")]
        public static void RemoveScriptableObject<T>(T field) where T : ScriptableObject
        {
            UnityEditor.AssetDatabase.RemoveObjectFromAsset(field);
            UnityEditor.AssetDatabase.SaveAssets();
            Object.DestroyImmediate(field, true);
        }
    }
}
