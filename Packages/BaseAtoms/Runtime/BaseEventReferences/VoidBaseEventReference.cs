using System;
using UnityEngine;

namespace UnityAtoms.BaseAtoms
{
    public class VoidBaseEventReferenceUsage
    {
        public const int EVENT = 0;
        public const int EVENT_INSTANCER = 1;
        public const int COLLECTION_CLEARED_EVENT = 2;
        public const int LIST_CLEARED_EVENT = 3;
        public const int COLLECTION_INSTANCER_CLEARED_EVENT = 4;
        public const int LIST_INSTANCER_CLEARED_EVENT = 5;
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
                switch (Usage)
                {
                    case (VoidBaseEventReferenceUsage.EVENT_INSTANCER):
                    {
                        return instancer.GetInstance(_event);
                    }
                    case (VoidBaseEventReferenceUsage.EVENT):
                        return _event;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(Usage));
                }
            }
            set
            {
                switch (Usage)
                {
                    case (VoidBaseEventReferenceUsage.LIST_CLEARED_EVENT):
                        {
                            _list.Cleared = value;
                            break;
                        }
                    case (VoidBaseEventReferenceUsage.EVENT):
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
        /// List used if `Usage` is set to `LIST_CLEARED_EVENT`.
        /// </summary>
        [SerializeField]
        private AtomList _list = default(AtomList);
    }
}
