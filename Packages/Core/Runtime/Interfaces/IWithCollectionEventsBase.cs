namespace UnityAtoms
{
    internal interface IWithCollectionEventsBase
    {
        AtomEventBase BaseAdded { get; set; }
        AtomEventBase BaseRemoved { get; set; }
        AtomEventBase BaseCleared { get; set; }
    }
}
