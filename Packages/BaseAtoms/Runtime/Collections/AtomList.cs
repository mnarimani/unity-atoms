using System;
using System.Collections.Generic;
using UnityEngine;
using UnityAtoms;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// A List of Atom Variables (AtomBaseVariable).
    /// </summary>
    [CreateAssetMenu(menuName = "Unity Atoms/Collections/List", fileName = "List")]
    [EditorIcon("atom-icon-lime")]
    public class AtomList : AtomValueList<AtomBaseVariable, AtomBaseVariableEvent>
    {
    }
}
