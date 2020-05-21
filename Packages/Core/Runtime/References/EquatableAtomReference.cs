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
    public abstract class EquatableAtomReference<T, P, C, V, E1, E2, F> : AtomReference<T, P, C, V, E1, E2, F>,
        IEquatable<EquatableAtomReference<T, P, C, V, E1, E2, F>>
        where P : struct, IPair<T>
        where C : AtomBaseVariable<T>
        where V : AtomVariable<T, P, E1, E2, F>
        where E1 : AtomEvent<T>
        where E2 : AtomEvent<P>
        where F : AtomFunction<T, T>
    {
        public EquatableAtomReference() : base()
        {
        }

        public EquatableAtomReference(T value) : base(value)
        {
        }

        public bool Equals(EquatableAtomReference<T, P, C, V, E1, E2, F> other)
        {
            return base.Equals(other);
        }

        protected override bool ValueEquals(T other)
        {
            return Value == null && other == null || Value != null && Value.Equals(other);
        }
    }
}
