using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// None generic base class for Events. Inherits from `BaseAtom` and `ISerializationCallbackReceiver`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    public abstract class AtomEventBase : BaseAtom, ISerializationCallbackReceiver
    {
        /// <summary>
        /// Event without value.
        /// </summary>
        protected readonly List<Action> OnEventNoValue = new List<Action>();

        public virtual void Invoke()
        {
            CheckInstancing();
            foreach (Action action in OnEventNoValue)
            {
                try
                {
                    action?.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        /// <summary>
        /// Register handler to be called when the Event triggers.
        /// </summary>
        /// <param name="del">The handler.</param>
        public void AddListener(Action del)
        {
            CheckInstancing();
            OnEventNoValue.Add(del);
        }

        /// <summary>
        /// Unregister handler that was registered using the `Register` method.
        /// </summary>
        /// <param name="del">The handler.</param>
        public void RemoveListener(Action del)
        {
            CheckInstancing();
            // TODO: Does it throw exception if del doesn't exist?
            OnEventNoValue.Remove(del);
        }

        /// <summary>
        /// Register a Listener that in turn trigger all its associated handlers when the Event triggers.
        /// </summary>
        /// <param name="listener">The Listener to register.</param>
        public void AddListener(IAtomListener listener)
        {
            CheckInstancing();
            OnEventNoValue.Add(listener.OnEventRaised);
        }

        /// <summary>
        /// Unregister a listener that was registered using the `RegisterListener` method.
        /// </summary>
        /// <param name="listener">The Listener to unregister.</param>
        public void RemoveListener(IAtomListener listener)
        {
            CheckInstancing();
            OnEventNoValue.Remove(listener.OnEventRaised);
        }

        public void OnBeforeSerialize()
        {
        }

        public virtual void OnAfterDeserialize()
        {
            // Clear all delegates when exiting play mode
            OnEventNoValue.Clear();
        }
    }
}
