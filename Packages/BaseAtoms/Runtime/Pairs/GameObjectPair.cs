using System;
using UnityEngine;
namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// IPair of type `&lt;GameObject&gt;`. Inherits from `IPair&lt;GameObject&gt;`.
    /// </summary>
    [Serializable]
    public struct GameObjectPair : IPair<GameObject>
    {
        public GameObject Item1 { get => item1; set => item1 = value; }
        public GameObject Item2 { get => item2; set => item2 = value; }

        [SerializeField]
        private GameObject item1;
        [SerializeField]
        private GameObject item2;

        public void Deconstruct(out GameObject item1, out GameObject item2) { item1 = Item1; item2 = Item2; }
    }
}
