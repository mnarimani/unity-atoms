using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityAtoms
{
    /// <summary>
    /// None generic base class for all Atoms.
    /// </summary>
    public abstract class BaseAtom : ScriptableObject
    {
        /// <summary>
        /// A description of the Atom made for documentation purposes.
        /// </summary>
        [SerializeField]
        [Multiline]
        [PropertyOrder(0)]
        private string _developerDescription;

        [SerializeField]
        [PropertyOrder(1)]
        private bool _requiresInstancing;

        public bool RequiresInstancing
        {
            get => _requiresInstancing;
            internal set => _requiresInstancing = value;
        }

        internal bool IsInMemoryInstance { get; set; }

        protected void CheckInstancing()
        {
            if(RequiresInstancing && !IsInMemoryInstance)
                throw new InvalidOperationException("This atom is designed to be instanced. You cannot interact with original object");
        }
    }
}
