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
        [ListDrawerSettings(IsReadOnly = true, Expanded = true)]
        private List<BaseAtom> InMemoryInstances
        {
            get => new List<BaseAtom>(instances.Values);
            // ReSharper disable once ValueParameterNotUsed
            set => Debug.LogError("Cannot change list");
        }
#endif

        public T GetInstance<T>(T atom) where T : BaseAtom
        {
            if (atom == null)
                return null;

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
                {
                    vInstance.BaseChanged = Instantiate(vOriginal.BaseChanged);
                    vInstance.BaseChanged.IsInMemoryInstance = true;
                }
            }

            if (atom is BaseAtomValueList listOriginal && newInstance is BaseAtomValueList listInstance)
            {
                if (listOriginal.BaseCleared != null)
                {
                    listInstance.BaseCleared = Instantiate(listOriginal.BaseCleared);
                    listInstance.BaseCleared.IsInMemoryInstance = true;
                }

                if (listOriginal.BaseAdded != null)
                {
                    listInstance.BaseAdded = Instantiate(listOriginal.BaseAdded);
                    listInstance.BaseAdded.IsInMemoryInstance = true;
                }

                if (listOriginal.BaseRemoved != null)
                {
                    listInstance.BaseRemoved = Instantiate(listOriginal.BaseRemoved);
                    listInstance.BaseRemoved.IsInMemoryInstance = true;
                }
            }

            newInstance.IsInMemoryInstance = true;
            instances.Add(atom, newInstance);
            return newInstance;
        }
    }
}
