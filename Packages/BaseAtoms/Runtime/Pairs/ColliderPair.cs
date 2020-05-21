using System;
using UnityEngine;
namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// IPair of type `&lt;Collider&gt;`. Inherits from `IPair&lt;Collider&gt;`.
    /// </summary>
    [Serializable]
    public struct ColliderPair : IPair<Collider>
    {
        public Collider Item1 { get => item1; set => item1 = value; }
        public Collider Item2 { get => item2; set => item2 = value; }

        [SerializeField]
        private Collider item1;
        [SerializeField]
        private Collider item2;

        public void Deconstruct(out Collider item1, out Collider item2) { item1 = Item1; item2 = Item2; }
    }
}
