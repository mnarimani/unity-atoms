using System;
using System.ComponentModel;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityAtoms;
using UnityEditor;
using UnityEngine;

namespace ShipClient.Drawers
{
    public class AtomVariableReferenceDrawer<Target, T, P, C, V, E1, E2, F> : OdinValueDrawer<Target>
        where Target : AtomReference<T, P, C, V, E1, E2, F>
        where P : struct, IPair<T>
        where C : AtomBaseVariable<T>
        where V : AtomVariable<T, P, E1, E2, F>
        where E1 : AtomEvent<T>
        where E2 : AtomEvent<P>
        where F : AtomFunction<T, T>
    {
        private GUIStyle popupStyle;

        protected override void DrawPropertyLayout(GUIContent label)
        {
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            Target value = ValueEntry.SmartValue;

            SirenixEditorGUI.BeginHorizontalPropertyLayout(label);
            // EditorGUILayout.BeginHorizontal();
            // if (label != null)
                // EditorGUILayout.PrefixLabel(label);

            value.Usage = (AtomReferenceUsage) EditorGUILayout.EnumPopup(value.Usage, popupStyle, GUILayout.Width(18));
            InspectorProperty variable = Property.FindChild(x => x.Name == "variable", false);
            // variable.Tree.Draw(true);
            SirenixEditorGUI.EndHorizontalPropertyLayout();
            // EditorGUILayout.EndHorizontal();
            variable.Draw(null);

            // Rect optionsRect = new Rect(rect.x, rect.y, 18, rect.height);
            // Rect valueRect = rect.AddXMin(18);
            //
            //
            // switch (value.Usage)
            // {
            //     case AtomReferenceUsage.Value:
            //        value.Value = SirenixEditorGUI.
            //         break;
            //     case AtomReferenceUsage.Constant:
            //         value.Constant = (C) SirenixEditorFields.UnityObjectField(valueRect,
            //             value.Constant,
            //             typeof(C),
            //             false);
            //         break;
            //     case AtomReferenceUsage.VariableInstancer:
            //     case AtomReferenceUsage.Variable:
            //         value.Variable = (V) SirenixEditorFields.UnityObjectField(valueRect,
            //             value.Variable,
            //             typeof(V),
            //             false);
            //         break;
            //     default:
            //         throw new ArgumentOutOfRangeException();
            // }
            //
            // if (value.Usage == AtomReferenceUsage.VariableInstancer)
            // {
            //     InspectorProperty instancer = Property.FindChild(p => p.Name == "instancer", false);
            //     EditorGUI.indentLevel++;
            //     instancer.Draw(new GUIContent("Instancer"));
            //     EditorGUI.indentLevel--;
            // }

            ValueEntry.SmartValue = value;
        }
    }
}
