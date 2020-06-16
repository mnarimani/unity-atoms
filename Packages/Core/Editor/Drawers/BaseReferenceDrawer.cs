using ShipClient.Instancers;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace UnityAtoms.Editor.Drawers
{
    public abstract class BaseReferenceDrawer<T> : OdinValueDrawer<T>
    {
        private const int AbbreviationThreshold = 350;

        protected void DrawInstancer()
        {
            InspectorProperty instancer = Property.FindChild(p => p.Name == "instancer", false);
            EditorGUI.indentLevel++;

            EditorGUILayout.BeginHorizontal();

            instancer.Draw(new GUIContent("Instancer"));

            string text;
            int width;

            if (EditorGUIUtility.currentViewWidth < AbbreviationThreshold)
            {
                text = "FIP";
                width = 30;
            }
            else
            {
                text = "Find In Parent";
                width = 90;
            }

            if (GUILayout.Button(text, GUILayout.Width(width)))
            {
                for (int i = 0; i < Property.Tree.WeakTargets.Count; ++i)
                {
                    var target = Property.Tree.WeakTargets[i] as Component;

                    if (target == null)
                        continue;

                    var atomInstancer = target.GetComponentInParent<AtomInstancer>();
                    if (atomInstancer == null)
                        atomInstancer = target.GetComponent<AtomInstancer>();

                    SetInstancerForValueEntry(i, atomInstancer);
                    EditorUtility.SetDirty(target);
                    EditorUtility.SetDirty(target.gameObject);
                }
            }

            EditorGUILayout.EndHorizontal();

            EditorGUI.indentLevel--;
        }

        protected abstract void SetInstancerForValueEntry(int entryIndex, AtomInstancer instancer);
    }
}
