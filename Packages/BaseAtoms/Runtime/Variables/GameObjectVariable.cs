using System;
using UnityEngine;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Variable of type `GameObject`. Inherits from `AtomVariable&lt;GameObject, GameObjectEvent, GameObjectPairEvent, GameObjectGameObjectFunction&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/GameObject", fileName = "GameObjectVariable")]
    public sealed class GameObjectVariable : AtomVariable<GameObject, GameObjectEvent>
    {
        protected override bool ValueEquals(GameObject other)
        {
            return (value == null && other == null) || value != null && other != null && value.GetInstanceID() == other.GetInstanceID();
        }
    }
}
