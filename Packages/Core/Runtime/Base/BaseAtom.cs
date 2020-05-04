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
    }
}
