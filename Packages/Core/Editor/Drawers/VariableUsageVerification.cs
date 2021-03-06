﻿using ShipClient.Instancers;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace UnityAtoms.Editor.Drawers
{
    [DrawerPriority(1)]
    public class VariableUsageVerification<T1, T2, T3, T4, T5, T6> : OdinValueDrawer<T1>
        where T1 : AtomVariable<T2, T4>
        where T4 : AtomEvent<T2>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            bool isNestedInInstancer = Property.FindParent(x => x.ParentType == typeof(AtomInstancer), true) != null;

            if (typeof(AtomBaseReference).IsAssignableFrom(Property.ParentType) == false &&
                typeof(AtomBaseEventReference).IsAssignableFrom(Property.ParentType) == false &&
                isNestedInInstancer == false)
            {
                if(ValueEntry.SmartValue != null && ValueEntry.SmartValue.RequiresInstancing && label.text.EndsWith("Variable") == false)
                {
                    SirenixEditorGUI.ErrorMessageBox(
                        $"Variable {ValueEntry.SmartValue.name} should be used with AtomInstancer. Use Reference classes instead of raw Event to declare your field. And set the usage to 'Variable Instancer'");
                }
            }

            CallNextDrawer(label);
        }
    }
}
