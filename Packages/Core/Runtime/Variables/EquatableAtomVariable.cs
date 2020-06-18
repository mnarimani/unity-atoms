using System;

namespace UnityAtoms
{
    /// <summary>
    /// Atom Variable base class for types that are implementing `IEquatable&lt;T&gt;`.
    /// </summary>
    /// <typeparam name="T">The Variable type.</typeparam>
    /// <typeparam name="P">Pair of type T.</typeparam>
    /// <typeparam name="E1">Event of type T.</typeparam>
    /// <typeparam name="E2">Pair event of type T.</typeparam>
    /// <typeparam name="F">Function of type T and T.</typeparam>
    [EditorIcon("atom-icon-lush")]
    public abstract class EquatableAtomVariable<T, E1> : AtomVariable<T ,E1>
        where T : IEquatable<T>
        where E1 : AtomEvent<T>
    {
        protected override bool ValueEquals(T other)
        {
            return (value == null && other == null) || (value != null && value.Equals(other));
        }
    }
}
