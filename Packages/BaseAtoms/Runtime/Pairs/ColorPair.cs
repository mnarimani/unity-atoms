using System;
using UnityEngine;
namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// IPair of type `&lt;Color&gt;`. Inherits from `IPair&lt;Color&gt;`.
    /// </summary>
    [Serializable]
    public struct ColorPair : IPair<Color>
    {
        public Color Item1 { get => item1; set => item1 = value; }
        public Color Item2 { get => item2; set => item2 = value; }

        [SerializeField]
        private Color item1;
        [SerializeField]
        private Color item2;

        public void Deconstruct(out Color item1, out Color item2) { item1 = Item1; item2 = Item2; }
    }
}
