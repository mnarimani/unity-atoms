using System;
using ShipClient.Instancers;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Different types of Event Reference usages.
    /// </summary>
    public enum AtomEventReferenceUsage
    {
         Event,
         EventInstancer,
         Variable,
         VariableInstancer,
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
        private AtomEventReferenceUsage usage;

        public AtomEventReferenceUsage Usage
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
                    case (AtomEventReferenceUsage.EventInstancer):
                        return instancer.GetInstance(targetEvent);
                    case (AtomEventReferenceUsage.Event):
                        return targetEvent;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(Usage));
                }
            }
            set
            {
                switch (Usage)
                {
                    case (AtomEventReferenceUsage.Event):
                        {
                            targetEvent = value;
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
        private E targetEvent = default(E);

        /// <summary>
        /// EventInstancer used if `Usage` is set to `EventInstancer`.
        /// </summary>
        [SerializeField]
        private AtomInstancer instancer = default(AtomInstancer);

        public AtomInstancer Instancer
        {
            get => instancer;
            set => instancer = value;
        }

        public E RawEvent
        {
            get => targetEvent;
            set => targetEvent = value;
        }

        protected AtomBaseEventReference()
        {
            Usage = AtomEventReferenceUsage.Event;
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
