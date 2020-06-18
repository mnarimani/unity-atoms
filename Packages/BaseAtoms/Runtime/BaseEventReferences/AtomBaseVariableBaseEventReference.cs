using System;
using UnityEngine;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Different types of Event Reference usages.
    /// </summary>
    public enum AtomBaseVariableEventReferenceUsage
    {
        Event = 0,
        EventInstancer = 1,
        CollectionAddedEvent = 2,
        CollectionRemovedEvent = 3,
        ListAddedEvent = 4,
        ListRemovedEvent = 5,
        CollectionInstancerAddedEvent = 6,
        CollectionInstancerRemovedEvent = 7,
        ListInstancerAddedEvent = 8,
        ListInstancerRemovedEvent = 9,
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
                switch ((AtomBaseVariableEventReferenceUsage) Usage)
                {
                    case (AtomBaseVariableEventReferenceUsage.ListAddedEvent):
                    {
                        return list != null ? list.Added : null;
                    }
                    case (AtomBaseVariableEventReferenceUsage.ListRemovedEvent):
                    {
                        return list != null ? list.Removed : null;
                    }
                    case (AtomBaseVariableEventReferenceUsage.EventInstancer):
                    {
                        return Instancer.GetInstance(RawEvent);
                    }
                    case (AtomBaseVariableEventReferenceUsage.Event):
                    {
                        return RawEvent;
                    }
                    default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(Usage));
                    }
                }
            }
            set
            {
                switch ((AtomBaseVariableEventReferenceUsage) Usage)
                {
                    case (AtomBaseVariableEventReferenceUsage.ListAddedEvent):
                        {
                            list.Added = value;
                            break;
                        }
                    case (AtomBaseVariableEventReferenceUsage.ListRemovedEvent):
                        {
                            list.Removed = value;
                            break;
                        }
                    case (AtomBaseVariableEventReferenceUsage.Event):
                        {
                            RawEvent = value;
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
        private AtomList list = default(AtomList);
    }
}
