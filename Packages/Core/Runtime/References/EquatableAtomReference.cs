using System;

namespace UnityAtoms
{
    /// <summary>
    ///     Atom Reference that where the value is implementing `IEquatable`.
    /// </summary>
    /// <typeparam name="T">The type of the variable.</typeparam>
    /// <typeparam name="P">IPair of type `T`.</typeparam>
    /// <typeparam name="C">Constant of type `T`.</typeparam>
    /// <typeparam name="V">Variable of type `T`.</typeparam>
    /// <typeparam name="E1">Event of type `T`.</typeparam>
    /// <typeparam name="E2">Event of type `IPair&lt;T&gt;`.</typeparam>
    /// <typeparam name="F">Function of type `T => T`.</typeparam>
    public abstract class EquatableAtomReference<T, V, E1> : AtomReference<T, V, E1>,
        IEquatable<EquatableAtomReference<T, V, E1>>
        where V : AtomVariable<T ,E1>
        where E1 : AtomEvent<T>
    {
        public EquatableAtomReference() : base()
        {
        }

        public EquatableAtomReference(T value) : base(value)
        {
        }

        public bool Equals(EquatableAtomReference<T, V, E1> other)
        {
            return base.Equals(other);
        }

        protected override bool ValueEquals(T other)
        {
            return Value == null && other == null || Value != null && Value.Equals(other);
        }
    }
}
