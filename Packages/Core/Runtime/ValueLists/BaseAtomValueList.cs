using System.Collections;
using Sirenix.OdinInspector;

namespace UnityAtoms
{
    /// <summary>
    /// None generic base class of Lists. Inherits from `BaseAtom`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    public abstract class BaseAtomValueList : BaseAtom, IWithCollectionEventsBase
    {
        private string ClearedButtonLabel => Cleared == null ? "Create" : "Destroy";

        /// <summary>
        /// Event for when the list is cleared.
        /// </summary>
        [InlineButton(nameof(CreateClearedEvent), "$ClearedButtonLabel")]
        public AtomEventBase Cleared;

        protected abstract IList IList { get; }

        public virtual AtomEventBase BaseAdded
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        public virtual AtomEventBase BaseRemoved
        {
            get => throw new System.NotImplementedException();
            set => throw new System.NotImplementedException();
        }

        AtomEventBase IWithCollectionEventsBase.BaseCleared
        {
            get => Cleared;
            set => Cleared = value;
        }

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
