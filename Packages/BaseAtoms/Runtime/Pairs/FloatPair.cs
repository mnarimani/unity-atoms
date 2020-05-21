using System;
using UnityEngine;
namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// IPair of type `&lt;float&gt;`. Inherits from `IPair&lt;float&gt;`.
    /// </summary>
    [Serializable]
    public struct FloatPair : IPair<float>
    {
        public float Item1 { get => item1; set => item1 = value; }
        public float Item2 { get => item2; set => item2 = value; }

        [SerializeField]
        private float item1;
        [SerializeField]
        private float item2;

        public void Deconstruct(out float item1, out float item2) { item1 = Item1; item2 = Item2; }
    }
}
