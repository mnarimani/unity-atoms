using System;
using ShipClient.Instancers;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Different types of Event Reference usages.
    /// </summary>
    public class AtomEventReferenceUsage
    {
        public const int EVENT = 0;
        public const int EVENT_INSTANCER = 1;
        public const int VARIABLE = 2;
        public const int VARIABLE_INSTANCER = 3;
    }

    /// <summary>
    /// Base class for Event References.
    /// </summary>
    public abstract class AtomBaseEventReference
    {
        /// <summary>
        /// Describes how we use the Event Reference.
        /// </summary>
        [SerializeField]
        private int usage;

        public int Usage
        {
            get => usage;
            set => usage = value;
        }
    }

    /// <summary>
    /// An Event Reference lets you define an event in your script where you then from the inspector can choose if it's going to use the Event from an Event, Event Instancer, Variable or a Variable Instancer.
    /// </summary>
    /// <typeparam name="T">The type of the event.</typeparam>
    /// <typeparam name="E">Event of type `T`.</typeparam>
    /// <typeparam name="EI">Event Instancer of type `T`.</typeparam>
    public abstract class AtomBaseEventReference<T, E> : AtomBaseEventReference, IGetEvent, ISetEvent
        where E : AtomEvent<T>
    {
        /// <summary>
        /// Get the event for the Event Reference.
        /// </summary>
        /// <value>The event of type `E`.</value>
        public virtual E Event
        {
            get
            {
                switch (Usage)
                {
                    case (AtomEventReferenceUsage.EVENT_INSTANCER):
                        return instancer.GetInstance(_event);
                    case (AtomEventReferenceUsage.EVENT):
                        return _event;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(Usage));
                }
            }
            set
            {
                switch (Usage)
                {
                    case (AtomEventReferenceUsage.EVENT):
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
        /// Event used if `Usage` is set to `Event`.
        /// </summary>
        [SerializeField]
        protected E _event = default(E);

        /// <summary>
        /// EventInstancer used if `Usage` is set to `EventInstancer`.
        /// </summary>
        [SerializeField]
        protected AtomInstancer instancer = default(AtomInstancer);

        protected AtomBaseEventReference()
        {
            Usage = AtomEventReferenceUsage.EVENT;
        }

        /// <summary>
        /// Get event by type.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <returns>The event.</returns>
        public EO GetEvent<EO>() where EO : AtomEventBase
        {
            if (typeof(EO) == typeof(E))
                return (Event as EO);

            throw new Exception($"Event type {typeof(EO)} not supported! Use {typeof(E)}.");
        }

        /// <summary>
        /// Set event by type.
        /// </summary>
        /// <param name="e">The new event value.</param>
        /// <typeparam name="E"></typeparam>
        public void SetEvent<EO>(EO e) where EO : AtomEventBase
        {
            if (typeof(EO) == typeof(E))
            {
                Event = (e as E);
                return;
            }

            throw new Exception($"Event type {typeof(EO)} not supported! Use {typeof(E)}.");
        }
    }
}
