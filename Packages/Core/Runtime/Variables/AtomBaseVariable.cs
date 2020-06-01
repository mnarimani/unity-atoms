using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityAtoms
{
    /// <summary>
    /// None generic base class for Variables. Inherits from `BaseAtom`.
    /// </summary>
    [EditorIcon("atom-icon-teal")]
    public abstract class AtomBaseVariable : BaseAtom
    {
        public String Id
        {
            get
            {
                CheckInstancing();
                return id;
            }
            set
            {
                if (CheckInstancing() == false)
                    return;

                id = value;
            }
        }

        /// <summary>
        /// The Variable value as an `object`.abstract Beware of boxing! 🥊
        /// </summary>
        /// <value>The Variable value as an `object`.</value>
        public abstract object BaseValue { get; set; }

        internal abstract AtomEventBase BaseChanged { get; set; }

        internal abstract AtomEventBase BaseChangedWithHistory { get; set; }

        [PropertyOrder(1)]
        [Space]
        [SerializeField]
        [FormerlySerializedAs("_id")]
        private String id = default;
    }

    /// <summary>
    /// Generic base class for Variables. Inherits from `AtomBaseVariable`.
    /// </summary>
    /// <typeparam name="T">The Variable value type.</typeparam>
    [EditorIcon("atom-icon-teal")]
    public abstract class AtomBaseVariable<T> : AtomBaseVariable, IEquatable<AtomBaseVariable<T>>
    {
        /// <summary>
        /// The Variable value as an `object`.abstract Beware of boxing! 🥊
        /// </summary>
        /// <value>The Variable value as an `object`.</value>
        public override object BaseValue
        {
            get { return value; }
            set { Value = (T) value; }
        }

        internal override AtomEventBase BaseChanged
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        internal override AtomEventBase BaseChangedWithHistory
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// The Variable value as a property.
        /// </summary>
        /// <returns>Get or set the Variable value.</returns>
        public virtual T Value
        {
            get
            {
                CheckInstancing();
                return value;
            }
            set => throw new NotImplementedException();
        }

        [SerializeField]
        [PropertyOrder(3)]
        [OnValueChanged(nameof(OnInspectorValueChanged))]
        [FormerlySerializedAs("_value")]
        protected T value;

        /// <summary>
        /// Determines equality between Variables.
        /// </summary>
        /// <param name="other">The other Variable to compare.</param>
        /// <returns>`true` if they are equal, otherwise `false`.</returns>
        public bool Equals(AtomBaseVariable<T> other)
        {
            return other == this;
        }

        protected virtual void OnInspectorValueChanged()
        {
        }
    }
}
