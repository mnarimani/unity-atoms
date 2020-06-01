using System;
using ShipClient.Instancers;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace UnityAtoms.Editor.Drawers
{
    public class AtomVariableReferenceDrawer<Target, T, P, C, V, E1, E2, F> : BaseReferenceDrawer<Target>
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

            InspectorProperty field = GetInspectorProperty();

            CheckInstancing();

            bool isBox = false;

            if (value.Usage == AtomReferenceUsage.VariableInstancer)
            {
                isBox = true;
                SirenixEditorGUI.BeginBox();
            }

            EditorGUILayout.BeginHorizontal();

            if (label != null)
                EditorGUILayout.PrefixLabel(label);

            value.Usage = (AtomReferenceUsage) EditorGUILayout.EnumPopup(value.Usage, GUILayout.Width(20));

            field.Draw(null);

            EditorGUILayout.EndHorizontal();

            if (value.Usage == AtomReferenceUsage.VariableInstancer)
            {
                DrawInstancer();
            }

            if (isBox)
            {
                SirenixEditorGUI.EndBox();
            }

            ValueEntry.SmartValue = value;
        }

        private void CheckInstancing()
        {
            Target value = ValueEntry.SmartValue;

            if (value.Usage == AtomReferenceUsage.VariableInstancer)
            {
                if (value.Variable != null)
                {
                    if (value.Variable.RequiresInstancing == false)
                    {
                        SirenixEditorGUI.ErrorMessageBox($"Variable {value.Variable.name} cannot be instanced.");
                    }
                }
            }

            if (value.Usage == AtomReferenceUsage.Variable)
            {
                if (value.Variable != null)
                {
                    if (value.Variable.RequiresInstancing)
                    {
                        SirenixEditorGUI.ErrorMessageBox(
                            $"Variable {value.Variable.name} should be used with AtomInstancer. Use Reference classes instead of raw Event to declare your field. And set the usage to 'Variable Instancer'");
                    }
                }
            }
        }

        private InspectorProperty GetInspectorProperty()
        {
            InspectorProperty field;

            switch (ValueEntry.SmartValue.Usage)
            {
                case AtomReferenceUsage.Value:
                    field = Property.FindChild(x => x.Name == "value", false);
                    break;
                case AtomReferenceUsage.Constant:
                    field = Property.FindChild(x => x.Name == "constant", false);
                    break;
                case AtomReferenceUsage.Variable:
                    field = Property.FindChild(x => x.Name == "variable", false);
                    break;
                case AtomReferenceUsage.VariableInstancer:
                    field = Property.FindChild(x => x.Name == "variable", false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return field;
        }

        protected override void SetInstancerForValueEntry(int entryIndex, AtomInstancer instancer)
        {
            ValueEntry.Values[entryIndex].Instancer = instancer;
        }
    }
}
