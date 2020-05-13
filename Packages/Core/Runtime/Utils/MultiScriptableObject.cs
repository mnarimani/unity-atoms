using System.Diagnostics;
using UnityEngine;

namespace UnityAtoms
{
    public static class MultiScriptableObject
    {
        [Conditional("UNITY_EDITOR")]
        public static void AddScriptableObject<T>(ScriptableObject obj, ref T field, string soName)
            where T : ScriptableObject
        {
#if UNITY_EDITOR
            var instance = ScriptableObject.CreateInstance<T>();
            instance.name = soName;
            UnityEditor.AssetDatabase.AddObjectToAsset(instance, obj);
            UnityEditor.EditorUtility.SetDirty(obj);
            UnityEditor.AssetDatabase.SaveAssets();
            field = instance;
#endif
        }

        [Conditional("UNITY_EDITOR")]
        public static void RemoveScriptableObject<T>(ScriptableObject obj, T field) where T : ScriptableObject
        {
#if UNITY_EDITOR
            UnityEditor.AssetDatabase.RemoveObjectFromAsset(field);
            Object.DestroyImmediate(field, true);
            UnityEditor.EditorUtility.SetDirty(obj);
            UnityEditor.AssetDatabase.SaveAssets();
#endif
        }
    }
}
