using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// Generic base class for Events. Inherits from `AtomEventBase`.
    /// </summary>
    /// <typeparam name="T">The type for this Event.</typeparam>
    [EditorIcon("atom-icon-cherry")]
    public class AtomEvent<T> : AtomEventBase
    {
        public T InspectorRaiseValue
        {
            get
            {
                CheckInstancing();
                return inspectorRaiseValue;
            }
        }

        protected List<Action<T>> OnEvent = new List<Action<T>>();

        private void OnDisable()
        {
            OnEvent.Clear();
        }

        /// <summary>
        /// Used when raising values from the inspector for debugging purposes.
        /// </summary>
        [SerializeField]
        [Tooltip("Value that will be used when using the Raise button in the editor inspector.")]
        private T inspectorRaiseValue;

        /// <summary>
        /// Raise the Event.
        /// </summary>
        /// <param name="item">The value associated with the Event.</param>
        public void Invoke(T item)
        {
            base.Invoke();

            foreach (Action<T> action in OnEvent)
            {
                try
                {
                    action?.Invoke(item);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        [Button("Raise")]
        [DisableInEditorMode]
        private void RaiseEditor() => Invoke(inspectorRaiseValue);

        /// <summary>
        /// Register handler to be called when the Event triggers.
        /// </summary>
        /// <param name="action">The handler.</param>
        public void AddListener(Action<T> action)
        {
            if (!CheckInstancing())
                return;

            OnEvent.Add(action);
        }

        /// <summary>
        /// Unregister handler that was registered using the `Register` method.
        /// </summary>
        /// <param name="action">The handler.</param>
        public void RemoveListener(Action<T> action)
        {
            if (!CheckInstancing())
                return;

            OnEvent.Remove(action);
        }

        /// <summary>
        /// Unregister all handlers that were registered using the `Register` method.
        /// </summary>
        public void RemoveAllListeners()
        {
            if (!CheckInstancing())
                return;

            OnEvent = null;
        }

        /// <summary>
        /// Register a Listener that in turn trigger all its associated handlers when the Event triggers.
        /// </summary>
        /// <param name="listener">The Listener to register.</param>
        public void AddListener(IAtomListener<T> listener, bool replayEventsBuffer = true)
        {
            if (!CheckInstancing())
                return;

            OnEvent.Add(listener.OnEventRaised);
        }

        /// <summary>
        /// Unregister a listener that was registered using the `RegisterListener` method.
        /// </summary>
        /// <param name="listener">The Listener to unregister.</param>
        public void RemoveListener(IAtomListener<T> listener)
        {
            if (!CheckInstancing())
                return;

            OnEvent.Remove(listener.OnEventRaised);
        }

        #region Observable

        /// <summary>
        /// Turn the Event into an `IObservable&lt;T&gt;`. Makes Events compatible with for example UniRx.
        /// </summary>
        /// <returns>The Event as an `IObservable&lt;T&gt;`.</returns>
        public IObservable<T> Observe()
        {
            if (!CheckInstancing())
                return null;

            return new ObservableEvent<T>(AddListener, RemoveListener);
        }

        #endregion // Observable
    }
}
