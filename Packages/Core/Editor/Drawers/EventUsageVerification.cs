using ShipClient.Instancers;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

namespace UnityAtoms.Editor.Drawers
{
    [DrawerPriority(1)]
    public class EventUsageVerification<T1, T2> : OdinValueDrawer<T1> where T1 : AtomEvent<T2>
    {
        protected override void DrawPropertyLayout(GUIContent label)
        {
            bool isNestedInInstancer = Property.FindParent(x => x.ParentType == typeof(AtomInstancer), true) != null;

            if (typeof(AtomBaseReference).IsAssignableFrom(Property.ParentType) == false &&
                typeof(AtomBaseEventReference).IsAssignableFrom(Property.ParentType) == false &&
                typeof(AtomBaseVariable).IsAssignableFrom(Property.ParentType) == false &&
                isNestedInInstancer == false)
            {
                if (ValueEntry.SmartValue != null && ValueEntry.SmartValue.RequiresInstancing&& label.text.EndsWith("Event") == false)
                {
                    SirenixEditorGUI.ErrorMessageBox(
                        $"Event {ValueEntry.SmartValue.name} should be used with AtomInstancer. Use Reference classes instead of raw Event to declare your field. And set the usage to 'Event Instancer' or 'Variable Instancer'");
                }
            }

            CallNextDrawer(label);
        }
    }
}
