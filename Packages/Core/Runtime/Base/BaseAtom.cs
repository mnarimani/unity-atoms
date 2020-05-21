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
        [PropertyOrder(-5)]
        private string developerDescription;

        [SerializeField] [PropertyOrder(-4)] private bool requiresInstancing;

        public bool RequiresInstancing
        {
            get => requiresInstancing;
            internal set => requiresInstancing = value;
        }

        internal bool IsInMemoryInstance { get; set; }

        protected bool CheckInstancing()
        {
            // Do not spam the editor. Only log when playing.
            // In edit mode, there will be error box in inspector
            if (RequiresInstancing && !IsInMemoryInstance)
            {
                // if (Application.isPlaying)
                Debug.LogError($"Atom ({name}) is designed to be instanced. " +
                               $"You cannot interact with original object",
                    this);

                return false;
            }

            return true;
        }
    }
}
