using System;
using UnityEngine;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Variable of type `Collider2D`. Inherits from `AtomVariable&lt;Collider2D, Collider2DEvent, Collider2DPairEvent, Collider2DCollider2DFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Collider2D", fileName = "Collider2DVariable")]
    public sealed class Collider2DVariable : AtomVariable<Collider2D, Collider2DEvent>
    {
        protected override bool ValueEquals(Collider2D other)
        {
            return (value == null && other == null) || value != null && other != null && value == other;
        }
    }
}
