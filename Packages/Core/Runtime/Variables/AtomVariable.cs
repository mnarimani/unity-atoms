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
    /// <typeparam name="P">IPair of type `T`.</typeparam>
    /// <typeparam name="E1">Event of type `AtomEvent&lt;T&gt;`.</typeparam>
    /// <typeparam name="E2">Event of type `AtomEvent&lt;T, T&gt;`.</typeparam>
    /// <typeparam name="F">Function of type `FunctionEvent&lt;T, T&gt;`.</typeparam>
    [EditorIcon("atom-icon-lush")]
    public abstract class AtomVariable<T, P, E1, E2, F> : AtomBaseVariable<T>, IGetEvent, ISetEvent
        where P : struct, IPair<T>
        where E1 : AtomEvent<T>
        where E2 : AtomEvent<P>
        where F : AtomFunction<T, T>
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

        internal override AtomEventBase BaseChangedWithHistory
        {
            get => changedWithHistory;
            set => changedWithHistory = (E2) value;
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

        /// <summary>
        /// Event that gets called when value changes. Do NOT assign runtime "Created" scriptable objects.
        /// </summary>
        public E2 ChangedWithHistory
        {
            get
            {
                CheckInstancing();
                return changedWithHistory;
            }
            set
            {
                if (!CheckInstancing())
                    return;

                changedWithHistory = value;
            }
        }

        private string ChangedEventButtonLabel => changed == null ? "Create" : "Destroy";
        private string ChangedWithHistoryEventButtonLabel => changedWithHistory == null ? "Create" : "Destroy";

        /// <summary>
        /// Changed Event triggered when the Variable value gets changed.
        /// </summary>
        [SerializeField]
        [PropertyOrder(6)]
        [FormerlySerializedAs("_changed")]
        [InlineButton(nameof(CreateNestedChangedEvent), "$ChangedEventButtonLabel")]
        private E1 changed;

        /// <summary>
        /// Changed with history Event triggered when the Variable value gets changed.
        /// </summary>
        [SerializeField]
        [PropertyOrder(7)]
        [FormerlySerializedAs("_changedWithHistory")]
        [InlineButton(nameof(CreateNestedChangedWithHistoryEvent), "$ChangedWithHistoryEventButtonLabel")]
        private E2 changedWithHistory;

        private T oldValue;

        /// <summary>
        /// When setting the value of a Variable the new value will be piped through all the pre change transformers, which allows you to create custom logic and restriction on for example what values can be set for this Variable.
        /// </summary>
        /// <value>Get the list of pre change transformers.</value>
        public List<F> PreChangeTransformers
        {
            get
            {
                CheckInstancing();

                return preChangeTransformers;
            }
            set
            {
                if (!CheckInstancing())
                    return;

                if (value == null)
                {
                    preChangeTransformers.Clear();
                }
                else
                {
                    preChangeTransformers = value;
                }
            }
        }

        [SerializeField] [PropertyOrder(8)] private List<F> preChangeTransformers = new List<F>();

        protected abstract bool ValueEquals(T other);

        private void OnValidate()
        {
            value = RunPreChangeTransformers(value);

            if (changed != null && changed.RequiresInstancing != RequiresInstancing)
                Debug.LogError("Variable Event instancing settings doesn't match with variable", this);

            if (changedWithHistory != null && changedWithHistory.RequiresInstancing != RequiresInstancing)
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

            var preProcessedNewValue = RunPreChangeTransformers(newValue);

            if (!ValueEquals(preProcessedNewValue))
            {
                value = preProcessedNewValue;

                if (Changed != null)
                {
                    Changed.Invoke(value);
                }

                if (ChangedWithHistory != null)
                {
                    // NOTE: Doing new P() here, even though it is cleaner, generates garbage.
                    var pair = default(P);
                    pair.Item1 = oldValue;
                    pair.Item2 = value;
                    ChangedWithHistory.Invoke(pair);
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
        public bool SetValue(AtomVariable<T, P, E1, E2, F> variable)
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

        /// <summary>
        /// Turn the Variable's change with history Event into an `IObservable&lt;T, T&gt;`. Makes the Variable's change with history Event compatible with for example UniRx.
        /// </summary>
        /// <returns>The Variable's change Event as an `IObservable&lt;T, T&gt;`.</returns>
        public IObservable<P> ObserveChangeWithHistory()
        {
            if (!CheckInstancing())
                return null;

            if (ChangedWithHistory == null)
            {
                throw new Exception("You must assign a ChangedWithHistory event in order to observe variable changes.");
            }

            return new ObservableEvent<P>(ChangedWithHistory.AddListener, ChangedWithHistory.RemoveListener);
        }

        #endregion // Observable

        private T RunPreChangeTransformers(T value)
        {
            if (preChangeTransformers.Count <= 0)
            {
                return value;
            }

            var preProcessedValue = value;
            for (var i = 0; i < preChangeTransformers.Count; ++i)
            {
                var Transformer = preChangeTransformers[i];
                if (Transformer != null)
                {
                    preProcessedValue = Transformer.Call(preProcessedValue);
                }
            }


            return preProcessedValue;
        }

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
            if (typeof(E) == typeof(E2))
                return (ChangedWithHistory as E);

            throw new Exception($"Event type {typeof(E)} not supported! Use {typeof(E1)} or {typeof(E2)}.");
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

            if (typeof(E) == typeof(E2))
            {
                ChangedWithHistory = (e as E2);
                return;
            }

            throw new Exception($"Event type {typeof(E)} not supported! Use {typeof(E1)} or {typeof(E2)}.");
        }

        protected override void OnInspectorValueChanged()
        {
            value = RunPreChangeTransformers(value);

            if (Changed != null)
            {
                Changed.Invoke(value);
            }

            if (ChangedWithHistory != null)
            {
                // NOTE: Doing new P() here, even though it is cleaner, generates garbage.
                var pair = default(P);
                pair.Item1 = oldValue;
                pair.Item2 = value;
                ChangedWithHistory.Invoke(pair);
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

        private void CreateNestedChangedWithHistoryEvent()
        {
            if (changedWithHistory == null)
            {
                MultiScriptableObject.AddScriptableObject(this, ref changedWithHistory, "Changed With History");
                changedWithHistory.RequiresInstancing = RequiresInstancing;
            }
            else
            {
                MultiScriptableObject.RemoveScriptableObject(this, changedWithHistory);
            }
        }
    }
}
