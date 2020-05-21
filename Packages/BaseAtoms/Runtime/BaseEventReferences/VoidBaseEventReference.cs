using System;
using UnityEngine;

namespace UnityAtoms.BaseAtoms
{
    public enum VoidBaseEventReferenceUsage
    {
        Event,
        EventInstancer,
        CollectionClearedEvent,
        ListClearedEvent,
        CollectionInstancerClearedEvent,
        ListInstancerClearedEvent,
    }

    /// <summary>
    /// Event Reference of type `Void`. Inherits from `AtomBaseEventReference&lt;Void, VoidEvent, VoidEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class VoidBaseEventReference : AtomBaseEventReference<
        Void,
        VoidEvent>, IGetEvent
    {
        /// <summary>
        /// Get or set the Event used by the Event Reference.
        /// </summary>
        /// <value>The event of type `E`.</value>
        public override VoidEvent Event
        {
            get
            {
                switch ((VoidBaseEventReferenceUsage) Usage)
                {
                    case (VoidBaseEventReferenceUsage.EventInstancer):
                    {
                        return Instancer.GetInstance(RawEvent);
                    }
                    case (VoidBaseEventReferenceUsage.Event):
                        return RawEvent;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(Usage));
                }
            }
            set
            {
                switch ((VoidBaseEventReferenceUsage) Usage)
                {
                    case (VoidBaseEventReferenceUsage.ListClearedEvent):
                    {
                        list.Cleared = value;
                        break;
                    }
                    case (VoidBaseEventReferenceUsage.Event):
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
        /// List used if `Usage` is set to `LIST_CLEARED_EVENT`.
        /// </summary>
        [SerializeField]
        private AtomList list = default(AtomList);
    }
}
