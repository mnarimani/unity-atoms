using System;
using UnityEngine;
namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// IPair of type `&lt;int&gt;`. Inherits from `IPair&lt;int&gt;`.
    /// </summary>
    [Serializable]
    public struct IntPair : IPair<int>
    {
        public int Item1 { get => item1; set => item1 = value; }
        public int Item2 { get => item2; set => item2 = value; }

        [SerializeField]
        private int item1;
        [SerializeField]
        private int item2;

        public void Deconstruct(out int item1, out int item2) { item1 = Item1; item2 = Item2; }
    }
}
