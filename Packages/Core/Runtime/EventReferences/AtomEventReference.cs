using System;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    ///     An Event Reference lets you define an event in your script where you then from the inspector can choose if it's
    ///     going to use the Event from an Event, Event Instancer, Variable or a Variable Instancer.
    /// </summary>
    /// <typeparam name="T">The type of the event.</typeparam>
    /// <typeparam name="V">Variable of type `T`.</typeparam>
    /// <typeparam name="E">Event of type `T`.</typeparam>
    public abstract class AtomEventReference<T, V, E> : AtomBaseEventReference<T, E>, IGetEvent, ISetEvent
        where V : BaseAtom, IGetEvent, ISetEvent
        where E : AtomEvent<T>
    {
        /// <summary>
        ///     Get or set the Event used by the Event Reference.
        /// </summary>
        /// <value>The event of type `E`.</value>
        public override E Event
        {
            get
            {
                switch (Usage)
                {
                    case AtomEventReferenceUsage.VARIABLE:
                    {
                        return _variable.GetEvent<E>();
                    }
                    case AtomEventReferenceUsage.VARIABLE_INSTANCER:
                    {
                        V v = instancer.GetInstance(_variable);
                        return v.GetEvent<E>();
                    }
                    case AtomEventReferenceUsage.EVENT_INSTANCER:
                    {
                        return instancer.GetInstance(_event);
                    }
                    case AtomEventReferenceUsage.EVENT:
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
                    case AtomEventReferenceUsage.VARIABLE:
                    {
                        _variable.SetEvent(value);
                        break;
                    }
                    case AtomEventReferenceUsage.VARIABLE_INSTANCER:
                    {
                        V instancedVariable = instancer.GetInstance(_variable);
                        instancedVariable.SetEvent(value);
                        break;
                    }
                    case AtomEventReferenceUsage.EVENT:
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
        ///     Variable used if `Usage` is set to `Variable`.
        /// </summary>
        [SerializeField]
        private V _variable = default(V);

        protected AtomEventReference()
        {
            Usage = AtomEventReferenceUsage.EVENT;
        }
    }
}
