using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityAtoms;
using UnityEngine;

namespace ShipClient.Instancers
{
    public class AtomInstancer : MonoBehaviour
    {
        private readonly Dictionary<BaseAtom, BaseAtom> instances = new Dictionary<BaseAtom, BaseAtom>();

#if UNITY_EDITOR
        [ShowInInspector]
        [InlineEditor]
        private List<BaseAtom> InMemoryInstances => new List<BaseAtom>(instances.Values);
#endif

        public T GetInstance<T>(T atom) where T : BaseAtom
        {
            if (atom.RequiresInstancing == false)
            {
                if (Application.isPlaying)
                    Debug.LogError($"Atom {atom.name} cannot be instanced.");

                return atom;
            }

            if (instances.TryGetValue(atom, out BaseAtom instance))
            {
                return (T) instance;
            }

            T newInstance = Instantiate(atom);

            if (atom is AtomBaseVariable vOriginal && newInstance is AtomBaseVariable vInstance)
            {
                if (vOriginal.BaseChanged != null)
                    vInstance.BaseChanged = Instantiate(vOriginal.BaseChanged);

                if (vOriginal.BaseChangedWithHistory != null)
                    vInstance.BaseChangedWithHistory = Instantiate(vOriginal.BaseChangedWithHistory);
            }

            if (atom is BaseAtomValueList listOriginal && newInstance is BaseAtomValueList listInstance)
            {
                if (listOriginal.BaseCleared != null)
                    listInstance.BaseCleared = Instantiate(listOriginal.BaseCleared);

                if (listOriginal.BaseAdded != null)
                    listInstance.BaseAdded = Instantiate(listOriginal.BaseAdded);

                if (listOriginal.BaseRemoved != null)
                    listInstance.BaseRemoved = Instantiate(listOriginal.BaseRemoved);
            }

            newInstance.IsInMemoryInstance = true;
            instances.Add(atom, newInstance);
            return newInstance;
        }
    }
}
