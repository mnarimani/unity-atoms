using System;
using UnityEngine;
namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// IPair of type `&lt;string&gt;`. Inherits from `IPair&lt;string&gt;`.
    /// </summary>
    [Serializable]
    public struct StringPair : IPair<string>
    {
        public string Item1 { get => item1; set => item1 = value; }
        public string Item2 { get => item2; set => item2 = value; }

        [SerializeField]
        private string item1;
        [SerializeField]
        private string item2;

        public void Deconstruct(out string item1, out string item2) { item1 = Item1; item2 = Item2; }
    }
}
