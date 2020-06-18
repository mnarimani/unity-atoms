using System;
using UnityEngine;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Variable of type `Collider`. Inherits from `AtomVariable&lt;Collider, ColliderEvent, ColliderPairEvent, ColliderColliderFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Collider", fileName = "ColliderVariable")]
    public sealed class ColliderVariable : AtomVariable<Collider, ColliderEvent>
    {
        protected override bool ValueEquals(Collider other)
        {
            return (value == null && other == null) || value != null && other != null && value == other;
        }
    }
}
