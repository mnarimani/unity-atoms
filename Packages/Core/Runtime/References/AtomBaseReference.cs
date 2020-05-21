using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityAtoms
{
    /// <summary>
    /// Different Reference usages.
    /// </summary>
    public enum AtomReferenceUsage
    {
        Value,
        Constant,
        Variable,
        VariableInstancer,
    }

    /// <summary>
    /// None generic base class for `AtomReference&lt;T, C, V, E1, E2, F, VI&gt;`.
    /// </summary>
    [Serializable]
    public abstract class AtomBaseReference
    {
        public AtomReferenceUsage Usage
        {
            get => usage;
            set => usage = value;
        }

        /// <summary>
        /// Describes how we use the Reference and where the value comes from.
        /// </summary>
        [SerializeField, EnumPaging]
        private AtomReferenceUsage usage;
    }
}
