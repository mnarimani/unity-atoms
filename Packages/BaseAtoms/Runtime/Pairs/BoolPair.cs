using System;
using UnityEngine;
namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// IPair of type `&lt;bool&gt;`. Inherits from `IPair&lt;bool&gt;`.
    /// </summary>
    [Serializable]
    public struct BoolPair : IPair<bool>
    {
        public bool Item1 { get => item1; set => item1 = value; }
        public bool Item2 { get => item2; set => item2 = value; }

        [SerializeField]
        private bool item1;
        [SerializeField]
        private bool item2;

        public void Deconstruct(out bool item1, out bool item2) { item1 = Item1; item2 = Item2; }
    }
}
