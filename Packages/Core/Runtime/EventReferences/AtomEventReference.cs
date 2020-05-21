using System;
using Sirenix.OdinInspector.Editor;
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
                    case AtomEventReferenceUsage.Variable:
                    {
                        return variable.GetEvent<E>();
                    }
                    case AtomEventReferenceUsage.VariableInstancer:
                    {
                        V v = Instancer.GetInstance(variable);
                        return v.GetEvent<E>();
                    }
                    case AtomEventReferenceUsage.EventInstancer:
                    {
                        return Instancer.GetInstance(RawEvent);
                    }
                    case AtomEventReferenceUsage.Event:
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
                switch (Usage)
                {
                    case AtomEventReferenceUsage.Variable:
                    {
                        variable.SetEvent(value);
                        break;
                    }
                    case AtomEventReferenceUsage.VariableInstancer:
                    {
                        V instancedVariable = Instancer.GetInstance(variable);
                        instancedVariable.SetEvent(value);
                        break;
                    }
                    case AtomEventReferenceUsage.Event:
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
        ///     Variable used if `Usage` is set to `Variable`.
        /// </summary>
        [SerializeField]
        private V variable = default(V);

        public V Variable
        {
            get => variable;
            set => variable = value;
        }

        protected AtomEventReference()
        {
            Usage = AtomEventReferenceUsage.Event;
        }
    }
}
