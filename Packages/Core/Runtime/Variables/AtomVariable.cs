using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityAtoms
{
    /// <summary>
    /// Generic base class for Variables. Inherits from `AtomBaseVariable&lt;T&gt;`.
    /// </summary>
    /// <typeparam name="T">The Variable value type.</typeparam>
    /// <typeparam name="E1">Event of type `AtomEvent&lt;T&gt;`.</typeparam>
    [EditorIcon("atom-icon-lush")]
    public abstract class AtomVariable<T, E1> : AtomBaseVariable<T>, IGetEvent, ISetEvent
        where E1 : AtomEvent<T>
    {
        /// <summary>
        /// The Variable value as a property.atom
        /// </summary>
        /// <returns>Get or set the Variable's value.</returns>
        public override T Value
        {
            get
            {
                CheckInstancing();
                return value;
            }
            set => SetValue(value);
        }

        /// <summary>
        /// The value the Variable had before its value got changed last time.
        /// </summary>
        /// <value>Get the Variable's old value.</value>
        public T OldValue
        {
            get
            {
                CheckInstancing();
                return oldValue;
            }
        }

        internal override AtomEventBase BaseChanged
        {
            get => changed;
            set => changed = (E1) value;
        }

        /// <summary>
        /// Event that gets called when value changes. Do NOT assign runtime "Created" scriptable objects.
        /// </summary>
        public E1 Changed
        {
            get
            {
                CheckInstancing();
                return changed;
            }
            set
            {
                if (!CheckInstancing())
                    return;

                changed = value;
            }
        }

        private string ChangedEventButtonLabel => changed == null ? "Create" : "Destroy";

        /// <summary>
        /// Changed Event triggered when the Variable value gets changed.
        /// </summary>
        [SerializeField]
        [PropertyOrder(6)]
        [FormerlySerializedAs("_changed")]
        [InlineButton(nameof(CreateNestedChangedEvent), "$ChangedEventButtonLabel")]
        private E1 changed;

        private T oldValue;

        protected abstract bool ValueEquals(T other);

        private void OnValidate()
        {
            if (changed != null && changed.RequiresInstancing != RequiresInstancing)
                Debug.LogError("Variable Event instancing settings doesn't match with variable", this);
        }

        /// <summary>
        /// Set the Variable value.
        /// </summary>
        /// <param name="newValue">The new value to set.</param>
        /// <returns>`true` if the value got changed, otherwise `false`.</returns>
        public bool SetValue(T newValue)
        {
            if (!CheckInstancing())
                return false;

            if (!ValueEquals(newValue))
            {
                value = newValue;

                if (Changed != null)
                {
                    Changed.Invoke(value);
                }

                oldValue = value;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Set the Variable value.
        /// </summary>
        /// <param name="variable">The value to set provided from another Variable.</param>
        /// <returns>`true` if the value got changed, otherwise `false`.</returns>
        public bool SetValue(AtomVariable<T, E1> variable)
        {
            return SetValue(variable.Value);
        }

        #region Observable

        /// <summary>
        /// Turn the Variable's change Event into an `IObservable&lt;T&gt;`. Makes the Variable's change Event compatible with for example UniRx.
        /// </summary>
        /// <returns>The Variable's change Event as an `IObservable&lt;T&gt;`.</returns>
        public IObservable<T> ObserveChange()
        {
            if (!CheckInstancing())
                return null;

            if (Changed == null)
            {
                throw new Exception("You must assign a Changed event in order to observe variable changes.");
            }

            return new ObservableEvent<T>(Changed.AddListener, Changed.RemoveListener);
        }

        #endregion // Observable

        /// <summary>
        /// Get event by type.
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <returns>The event.</returns>
        public E GetEvent<E>() where E : AtomEventBase
        {
            if (!CheckInstancing())
                return null;

            if (typeof(E) == typeof(E1))
                return (Changed as E);

            throw new Exception($"Event type {typeof(E)} not supported! Use {typeof(E1)}.");
        }

        /// <summary>
        /// Set event by type.
        /// </summary>
        /// <param name="e">The new event value.</param>
        /// <typeparam name="E"></typeparam>
        public void SetEvent<E>(E e) where E : AtomEventBase
        {
            if (!CheckInstancing())
                return;

            if (typeof(E) == typeof(E1))
            {
                Changed = (e as E1);
                return;
            }

            throw new Exception($"Event type {typeof(E)} not supported! Use {typeof(E1)}.");
        }

        protected override void OnInspectorValueChanged()
        {
            if (Changed != null)
            {
                Changed.Invoke(value);
            }

            oldValue = value;
        }

        private void CreateNestedChangedEvent()
        {
            if (changed == null)
            {
                MultiScriptableObject.AddScriptableObject(this, ref changed, "Changed");
                changed.RequiresInstancing = RequiresInstancing;
            }
            else
            {
                MultiScriptableObject.RemoveScriptableObject(this, changed);
            }
        }
    }
}
