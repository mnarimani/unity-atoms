using System;
using UnityEngine;
namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// IPair of type `&lt;Vector2&gt;`. Inherits from `IPair&lt;Vector2&gt;`.
    /// </summary>
    [Serializable]
    public struct Vector2Pair : IPair<Vector2>
    {
        public Vector2 Item1 { get => item1; set => item1 = value; }
        public Vector2 Item2 { get => item2; set => item2 = value; }

        [SerializeField]
        private Vector2 item1;
        [SerializeField]
        private Vector2 item2;

        public void Deconstruct(out Vector2 item1, out Vector2 item2) { item1 = Item1; item2 = Item2; }
    }
}
