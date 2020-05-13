using System.Collections;
using Sirenix.OdinInspector;

namespace UnityAtoms
{
    /// <summary>
    /// None generic base class of Lists. Inherits from `BaseAtom`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    public abstract class BaseAtomValueList : BaseAtom
    {
        private string ClearedButtonLabel => Cleared == null ? "Create" : "Destroy";

        /// <summary>
        /// Event for when the list is cleared.
        /// </summary>
        [InlineButton(nameof(CreateClearedEvent), "$ClearedButtonLabel")]
        public AtomEventBase Cleared;
        protected abstract IList IList { get; }

        /// <summary>
        /// Clear the list.
        /// </summary>
        public void Clear()
        {
            IList.Clear();
            if (null != Cleared)
            {
                Cleared.Invoke();
            }
        }

        private void CreateClearedEvent()
        {
            if(Cleared == null)
                MultiScriptableObject.AddScriptableObject(this, ref Cleared, "Cleared");
            else
                MultiScriptableObject.RemoveScriptableObject(this, Cleared);
        }
    }
}
