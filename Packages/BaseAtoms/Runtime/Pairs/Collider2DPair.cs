using System;
using UnityEngine;
namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// IPair of type `&lt;Collider2D&gt;`. Inherits from `IPair&lt;Collider2D&gt;`.
    /// </summary>
    [Serializable]
    public struct Collider2DPair : IPair<Collider2D>
    {
        public Collider2D Item1 { get => item1; set => item1 = value; }
        public Collider2D Item2 { get => item2; set => item2 = value; }

        [SerializeField]
        private Collider2D item1;
        [SerializeField]
        private Collider2D item2;

        public void Deconstruct(out Collider2D item1, out Collider2D item2) { item1 = Item1; item2 = Item2; }
    }
}
