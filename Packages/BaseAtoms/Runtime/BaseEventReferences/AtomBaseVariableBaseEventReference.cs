using System;
using UnityEngine;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Different types of Event Reference usages.
    /// </summary>
    public class AtomBaseVariableEventReferenceUsage
    {
        public const int EVENT = 0;
        public const int EVENT_INSTANCER = 1;
        public const int COLLECTION_ADDED_EVENT = 2;
        public const int COLLECTION_REMOVED_EVENT = 3;
        public const int LIST_ADDED_EVENT = 4;
        public const int LIST_REMOVED_EVENT = 5;
        public const int COLLECTION_INSTANCER_ADDED_EVENT = 6;
        public const int COLLECTION_INSTANCER_REMOVED_EVENT = 7;
        public const int LIST_INSTANCER_ADDED_EVENT = 8;
        public const int LIST_INSTANCER_REMOVED_EVENT = 9;
    }

    /// <summary>
    /// Event Reference of type `AtomBaseVariable`. Inherits from `AtomBaseEventReference&lt;AtomBaseVariable, AtomBaseVariableEvent, AtomBaseVariableEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class AtomBaseVariableBaseEventReference : AtomBaseEventReference<
        AtomBaseVariable,
        AtomBaseVariableEvent>, IGetEvent
    {
        /// <summary>
        /// Get or set the Event used by the Event Reference.
        /// </summary>
        /// <value>The event of type `E`.</value>
        public override AtomBaseVariableEvent Event
        {
            get
            {
                switch (Usage)
                {
                    case (AtomBaseVariableEventReferenceUsage.LIST_ADDED_EVENT):
                    {
                        return _list != null ? _list.Added : null;
                    }
                    case (AtomBaseVariableEventReferenceUsage.LIST_REMOVED_EVENT):
                    {
                        return _list != null ? _list.Removed : null;
                    }
                    case (AtomBaseVariableEventReferenceUsage.EVENT_INSTANCER):
                    {
                        return instancer.GetInstance(_event);
                    }
                    case (AtomBaseVariableEventReferenceUsage.EVENT):
                    {
                        return _event;
                    }
                    default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(Usage));
                    }
                }
            }
            set
            {
                switch (Usage)
                {
                    case (AtomBaseVariableEventReferenceUsage.LIST_ADDED_EVENT):
                        {
                            _list.Added = value;
                            break;
                        }
                    case (AtomBaseVariableEventReferenceUsage.LIST_REMOVED_EVENT):
                        {
                            _list.Removed = value;
                            break;
                        }
                    case (AtomBaseVariableEventReferenceUsage.EVENT):
                        {
                            _event = value;
                            break;
                        }
                    default:
                        throw new NotSupportedException($"Event not reassignable for usage {Usage}.");
                }
            }
        }

        /// <summary>
        /// List used if `Usage` is set to `LIST_ADDED_EVENT` or `LIST_REMOVED_EVENT`.
        /// </summary>
        [SerializeField]
        private AtomList _list = default(AtomList);
    }
}
