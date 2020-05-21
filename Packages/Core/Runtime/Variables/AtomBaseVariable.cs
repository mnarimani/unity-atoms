using System;
using Sirenix.OdinInspector;
using UnityEngine;

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
                return _id;
            }
            set
            {
                CheckInstancing();
                _id = value;
            }
        }

        /// <summary>
        /// The Variable value as an `object`.abstract Beware of boxing! 🥊
        /// </summary>
        /// <value>The Variable value as an `object`.</value>
        public abstract object BaseValue { get; set; }

        public abstract AtomEventBase BaseChanged { get; set; }

        public abstract AtomEventBase BaseChangedWithHistory { get; set; }

        [PropertyOrder(1)]
        [Space]
        [SerializeField]
        private String _id = default;
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
            get
            {
                return _value;
            }
            set
            {
                Value = (T)value;
            }
        }

        public override AtomEventBase BaseChanged
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public override AtomEventBase BaseChangedWithHistory
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        /// <summary>
        /// The Variable value as a property.
        /// </summary>
        /// <returns>Get or set the Variable value.</returns>
        public virtual T Value { get { return _value; } set { throw new NotImplementedException(); } }

        [SerializeField]
        [PropertyOrder(3)]
        [OnValueChanged(nameof(OnInspectorValueChanged))]
        protected T _value = default(T);

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
