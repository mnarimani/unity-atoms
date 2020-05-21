using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    ///     Event Reference of type `ColliderPair`. Inherits from `AtomEventReference&lt;ColliderPair, ColliderVariable,
    ///     ColliderPairEvent, ColliderVariableInstancer, ColliderPairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class ColliderPairEventReference : AtomEventReference<
        ColliderPair,
        ColliderVariable,
        ColliderPairEvent
    >, IGetEvent
    {
    }
}
