using System;
using ShipClient.Instancers;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityAtoms
{
    /// <summary>
    ///     A Reference lets you define a variable in your script where you then from the inspector can choose if it's going to
    ///     be taking the value from a Constant, Variable, Value or a Variable Instancer.
    /// </summary>
    /// <typeparam name="T">The type of the variable.</typeparam>
    /// <typeparam name="P">IPair of type `T`.</typeparam>
    /// <typeparam name="C">Constant of type `T`.</typeparam>
    /// <typeparam name="V">Variable of type `T`.</typeparam>
    /// <typeparam name="E1">Event of type `T`.</typeparam>
    /// <typeparam name="E2">Event of type `IPair&lt;T&gt;`.</typeparam>
    /// <typeparam name="F">Function of type `T => T`.</typeparam>
    [Serializable]
    public abstract class AtomReference<T, V, E1> : AtomBaseReference,
        IEquatable<AtomReference<T, V, E1>>
        where V : AtomVariable<T, E1>
        where E1 : AtomEvent<T>
    {
        /// <summary>
        ///     Get or set the value for the Reference.
        /// </summary>
        /// <value>The value of type `T`.</value>
        public T Value
        {
            get
            {
                switch (Usage)
                {
                    case AtomReferenceUsage.Variable:
                    {
                        return variable != null ? variable.Value : default(T);
                    }
                    case AtomReferenceUsage.VariableInstancer:
                    {
                        if (instancer == null)
                        {
                            return default;
                        }

                        V instancedVariable = instancer.GetInstance(variable);
                        return instancedVariable.Value;
                    }
                    case AtomReferenceUsage.Value:
                        return value;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(Usage));
                }
            }
            set
            {
                switch (Usage)
                {
                    case AtomReferenceUsage.Variable:
                    {
                        variable.Value = value;
                        break;
                    }
                    case AtomReferenceUsage.Value:
                    {
                        this.value = value;
                        break;
                    }
                    case AtomReferenceUsage.VariableInstancer:
                    {
                        if (instancer == null)
                        {
                            Debug.LogError("Instancer is not assigned. Cannot assign value.");
                            return;
                        }

                        V instancedVariable = instancer.GetInstance(variable);
                        instancedVariable.Value = value;
                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(Usage));
                }
            }
        }

        /// <summary>
        ///     Value used if `Usage` is set to `Value`.
        /// </summary>
        [SerializeField]
        [FormerlySerializedAs("_value")]
        private T value = default(T);

        /// <summary>
        ///     Variable used if `Usage` is set to `Variable`.
        /// </summary>
        [SerializeField]
        [FormerlySerializedAs("_variable")]
        private V variable = default(V);

        /// <summary>
        ///     Variable Instancer used if `Usage` is set to `VariableInstancer`.
        /// </summary>
        [SerializeField]
        private AtomInstancer instancer = default(AtomInstancer);

        public V Variable
        {
            get => variable;
            set => variable = value;
        }

        public AtomInstancer Instancer
        {
            get => instancer;
            set => instancer = value;
        }

        protected AtomReference()
        {
            Usage = AtomReferenceUsage.Value;
        }

        protected AtomReference(T value) : this()
        {
            Usage = AtomReferenceUsage.Value;
            this.value = value;
        }

        protected abstract bool ValueEquals(T other);

        public bool Equals(AtomReference<T, V, E1> other)
        {
            if (other == null)
                return false;

            return ValueEquals(other.Value);
        }

        public override int GetHashCode()
        {
            return Value == null ? 0 : Value.GetHashCode();
        }
    }
}
