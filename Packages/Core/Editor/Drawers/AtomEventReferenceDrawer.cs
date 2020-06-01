using System;
using ShipClient.Instancers;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace UnityAtoms.Editor.Drawers
{
    public class AtomEventReferenceDrawer<Target, T, V, E> : BaseReferenceDrawer<Target>
        where Target : AtomEventReference<T, V, E>
        where V : BaseAtom, IGetEvent, ISetEvent
        where E : AtomEvent<T>
    {
        private const int AbbreviationThreshold = 350;

        protected override void DrawPropertyLayout(GUIContent label)
        {
            Target value = ValueEntry.SmartValue;

            InspectorProperty field = GetInspectorProperty();

            CheckInstancing();

            bool isBox = false;

            if (value.Usage == AtomEventReferenceUsage.EventInstancer ||
                value.Usage == AtomEventReferenceUsage.VariableInstancer)
            {
                isBox = true;
                SirenixEditorGUI.BeginBox();
            }

            EditorGUILayout.BeginHorizontal();

            if (label != null)
                EditorGUILayout.PrefixLabel(label);

            value.Usage = (AtomEventReferenceUsage) EditorGUILayout.EnumPopup(value.Usage, GUILayout.Width(20));

            field.Draw(null);

            EditorGUILayout.EndHorizontal();

            if (value.Usage == AtomEventReferenceUsage.VariableInstancer ||
                value.Usage == AtomEventReferenceUsage.EventInstancer)
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

            if (value.Usage == AtomEventReferenceUsage.VariableInstancer)
            {
                if (value.Variable != null)
                {
                    if (value.Variable.RequiresInstancing == false)
                    {
                        SirenixEditorGUI.ErrorMessageBox($"Variable {value.Variable.name} cannot be instanced.");
                    }
                }
            }

            if (value.Usage == AtomEventReferenceUsage.EventInstancer)
            {
                if (value.RawEvent != null)
                {
                    if (value.RawEvent.RequiresInstancing == false)
                    {
                        SirenixEditorGUI.ErrorMessageBox($"Event {value.RawEvent.name} cannot be instanced.");
                    }
                }
            }

            if (value.Usage == AtomEventReferenceUsage.Variable)
            {
                if (value.Variable != null)
                {
                    if (value.Variable.RequiresInstancing)
                    {
                        SirenixEditorGUI.ErrorMessageBox($"Variable {value.Variable.name} should be used with AtomInstancer. Use Reference classes instead of raw Event to declare your field. And set the usage to 'Variable Instancer'");
                    }
                }
            }

            if (value.Usage == AtomEventReferenceUsage.Event)
            {
                if (value.RawEvent != null)
                {
                    if (value.RawEvent.RequiresInstancing)
                    {
                        SirenixEditorGUI.ErrorMessageBox($"Event {value.RawEvent.name} should be used with AtomInstancer. Use Reference classes instead of raw Event to declare your field. And set the usage to 'Event Instancer' or 'Variable Instancer'");
                    }
                }
            }
        }

        private InspectorProperty GetInspectorProperty()
        {
            InspectorProperty field;

            switch (ValueEntry.SmartValue.Usage)
            {
                case AtomEventReferenceUsage.Event:
                    field = Property.FindChild(x => x.Name == "targetEvent", false);
                    break;
                case AtomEventReferenceUsage.EventInstancer:
                    field = Property.FindChild(x => x.Name == "targetEvent", false);
                    break;
                case AtomEventReferenceUsage.Variable:
                    field = Property.FindChild(x => x.Name == "variable", false);
                    break;
                case AtomEventReferenceUsage.VariableInstancer:
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
