using UnityEngine;
using UnityEngine.Assertions;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Adds a GameObject to a GameObject Value List on OnEnable and removes it on OnDestroy.
    /// </summary>
    [AddComponentMenu("Unity Atoms/MonoBehaviour Helpers/Sync GameObject To List")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncGameObjectToList : MonoBehaviour
    {
        [SerializeField]
        private GameObjectValueList list = default;

        void OnEnable()
        {
            Assert.IsNotNull(list);
            list.Add(gameObject);
        }

        void OnDestroy()
        {
            list.Remove(gameObject);
        }
    }
}
