using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// A List of type AtomBaseVariable. Used by AtomList.
    /// </summary>
    [Serializable]
    public class AtomBaseVariableList : List<AtomBaseVariable>, ISerializationCallbackReceiver, IAtomList
    {
        /// <summary>
        /// Get or set the Added Action.
        /// </summary>
        public Action<AtomBaseVariable> Added { get => added; set => added = value; }

        /// <summary>
        /// Get or set the Removed Action.
        /// </summary>
        public Action<AtomBaseVariable> Removed { get => removed; set => removed = value; }

        /// <summary>
        /// Get or set the Cleared Action.
        /// </summary>
        public Action Cleared { get => cleared; set => cleared = value; }

        private event Action<AtomBaseVariable> added;
        private event Action<AtomBaseVariable> removed;
        private event Action cleared;

        [SerializeField, InlineEditor(InlineEditorModes.FullEditor, DrawHeader = false)]
        private List<AtomBaseVariable> serializedList = new List<AtomBaseVariable>();

        public void OnAfterDeserialize()
        {
            if (serializedList != null)
            {
                base.Clear();
                for (var i = 0; i < serializedList.Count; ++i)
                {
                    base.Add(serializedList[i]);
                }
            }
        }

        public void OnBeforeSerialize()
        {
            serializedList.Clear();
            for (var i = 0; i < this.Count; ++i)
            {
                serializedList.Add(this[i]);
            }
        }

        /// <summary>
        /// Generic getter.
        /// </summary>
        /// <param name="index">The index you want to retrieve.</param>
        /// <typeparam name="T">The expected type of the value you want to retrieve.</typeparam>
        /// <returns>The value of type T at specified index.</returns>
        public T Get<T>(int index) where T : AtomBaseVariable
        {
            return (T)this[index];
        }

        /// <summary>
        /// Generic getter.
        /// </summary>
        /// <param name="index">The index you want to retrieve.</param>
        /// <typeparam name="T">The expected type of the value you want to retrieve.</typeparam>
        /// <returns>The value of type T at specified index.</returns>
        public T Get<T>(AtomBaseVariable<int> index) where T : AtomBaseVariable
        {
            if (index == null) throw new ArgumentNullException("index");
            return (T)this[index.Value];
        }

        /// <summary>
        /// Add an item to the list.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public new void Add(AtomBaseVariable item)
        {
            base.Add(item);
            serializedList.Add(item);
            Added?.Invoke(item);
        }

        /// <summary>
        /// Remove an item from the list.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns>True if it was removed, otherwise false..</returns>
        public new bool Remove(AtomBaseVariable item)
        {
            var removed = base.Remove(item);
            serializedList.Remove(item);
            if (!removed) return false;
            Removed?.Invoke(item);
            return true;
        }

        /// <summary>
        /// Remove an item at provided index.
        /// </summary>
        /// <param name="index">The index to remove item at.</param>
        public new void RemoveAt(int index)
        {
            var item = this[index];
            base.RemoveAt(index);
            serializedList.RemoveAt(index);
            Removed?.Invoke(item);
        }

        /// <summary>
        /// Insert item at index.
        /// </summary>
        /// <param name="index">Index to insert item at.</param>
        /// <param name="item">Item to insert.</param>
        public new void Insert(int index, AtomBaseVariable item)
        {
            this.Insert(index, item);
            serializedList.Insert(index, item);
            Added?.Invoke(item);
        }

        /// <summary>
        /// Clear list.
        /// </summary>
        public new void Clear()
        {
            base.Clear();
            serializedList.Clear();
            cleared?.Invoke();
        }
    }
}
