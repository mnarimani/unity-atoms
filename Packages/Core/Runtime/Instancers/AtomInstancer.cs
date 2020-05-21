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
            if (atom.RequiresInstancing)
            {
                Debug.LogError($"Atom {atom.name} is not designed to be instanced");
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

            if (atom is IWithCollectionEventsBase listOriginal && newInstance is IWithCollectionEventsBase listInstance)
            {
                if (listOriginal.BaseCleared != null)
                    listInstance.BaseCleared = Instantiate(listOriginal.BaseCleared);

                if (listOriginal.BaseAdded != null)
                    listInstance.BaseAdded = Instantiate(listOriginal.BaseAdded);

                if (listOriginal.BaseRemoved != null)
                    listInstance.BaseRemoved = Instantiate(listOriginal.BaseRemoved);
            }

            instances.Add(atom, newInstance);
            return newInstance;
        }
    }
}
