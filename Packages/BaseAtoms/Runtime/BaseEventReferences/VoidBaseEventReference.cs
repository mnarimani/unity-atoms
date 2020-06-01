using System;
using UnityEngine;

namespace UnityAtoms.BaseAtoms
{
    public enum VoidBaseEventReferenceUsage
    {
        Event,
        EventInstancer
    }

    /// <summary>
    /// Event Reference of type `Void`. Inherits from `AtomBaseEventReference&lt;Void, VoidEvent, VoidEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class VoidBaseEventReference : AtomBaseEventReference<Void, VoidEvent>, IGetEvent
    {
        [SerializeField] private VoidBaseEventReferenceUsage voidUsage;

        public VoidBaseEventReferenceUsage VoidUsage
        {
            get => voidUsage;
            set => voidUsage = value;
        }

        public override AtomEventReferenceUsage Usage
        {
            get => throw new NotSupportedException("Use VoidUsage property in VoidBaseEventReference");
            set => throw new NotSupportedException("Use VoidUsage property in VoidBaseEventReference");
        }

        /// <summary>
        /// Get or set the Event used by the Event Reference.
        /// </summary>
        /// <value>The event of type `E`.</value>
        public override VoidEvent Event
        {
            get
            {
                switch (VoidUsage)
                {
                    case VoidBaseEventReferenceUsage.EventInstancer:
                    {
                        return Instancer.GetInstance(RawEvent);
                    }
                    case VoidBaseEventReferenceUsage.Event:
                        return RawEvent;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(VoidUsage));
                }
            }
            set
            {
                switch (VoidUsage)
                {
                    case VoidBaseEventReferenceUsage.Event:
                    {
                        RawEvent = value;
                        break;
                    }
                    default:
                        throw new NotSupportedException($"Event not reassignable for usage {Usage}.");
                }
            }
        }
    }
}
