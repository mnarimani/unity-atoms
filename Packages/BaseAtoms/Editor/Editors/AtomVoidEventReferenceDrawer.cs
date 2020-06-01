using System;
using ShipClient.Instancers;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms.Editor.Drawers
{
    public class AtomVoidEventReferenceDrawer : BaseReferenceDrawer<VoidBaseEventReference>
    {
        private const int AbbreviationThreshold = 350;

        protected override void DrawPropertyLayout(GUIContent label)
        {
            VoidBaseEventReference value = ValueEntry.SmartValue;

            InspectorProperty field = GetInspectorProperty();

            CheckInstancing();

            bool isBox = false;

            if (value.VoidUsage == VoidBaseEventReferenceUsage.EventInstancer)
            {
                isBox = true;
                SirenixEditorGUI.BeginBox();
            }

            EditorGUILayout.BeginHorizontal();

            if (label != null)
                EditorGUILayout.PrefixLabel(label);

            value.VoidUsage =
                (VoidBaseEventReferenceUsage) EditorGUILayout.EnumPopup(value.VoidUsage, GUILayout.Width(20));

            field.Draw(null);

            EditorGUILayout.EndHorizontal();

            if (value.VoidUsage == VoidBaseEventReferenceUsage.EventInstancer)
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
            VoidBaseEventReference value = ValueEntry.SmartValue;

            if (value.VoidUsage == VoidBaseEventReferenceUsage.EventInstancer)
            {
                if (value.RawEvent != null)
                {
                    if (value.RawEvent.RequiresInstancing == false)
                    {
                        SirenixEditorGUI.ErrorMessageBox($"Event {value.RawEvent.name} cannot be instanced.");
                    }
                }
            }

            if (value.VoidUsage == VoidBaseEventReferenceUsage.Event)
            {
                if (value.RawEvent != null)
                {
                    if (value.RawEvent.RequiresInstancing)
                    {
                        SirenixEditorGUI.ErrorMessageBox(
                            $"Event {value.RawEvent.name} should be used with AtomInstancer. Use Reference classes instead of raw Event to declare your field. And set the usage to 'Event Instancer' or 'Variable Instancer'");
                    }
                }
            }
        }

        private InspectorProperty GetInspectorProperty()
        {
            return Property.FindChild(x => x.Name == "targetEvent", false);
        }


        protected override void SetInstancerForValueEntry(int entryIndex, AtomInstancer instancer)
        {
            ValueEntry.Values[entryIndex].Instancer = instancer;
        }
    }
}
