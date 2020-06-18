using System;
using UnityAtoms.BaseAtoms;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Reference of type `bool`. Inherits from `EquatableAtomReference&lt;bool, >, BoolConstant, BoolVariable, BoolEvent, BoolPairEvent, BoolBoolFunction, BoolVariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class BoolReference : EquatableAtomReference<
        bool,
        BoolVariable,
        BoolEvent>, IEquatable<BoolReference>
    {
        public BoolReference() : base()
        {
        }

        public BoolReference(bool value) : base(value)
        {
        }

        public bool Equals(BoolReference other)
        {
            return base.Equals(other);
        }
    }
}
