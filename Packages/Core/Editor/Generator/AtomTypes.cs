using System.IO;
using System.Collections.Generic;

namespace UnityAtoms.Editor
{
    /// <summary>
    /// Internal static class holding predefined static `AtomType`s.
    /// </summary>
    internal static class AtomTypes
    {
        public static readonly AtomType EVENT = new AtomType(displayName: "Event",
            templateName: "UA_Template__Event.txt",
            drawerTemplateName: "UA_Template_AtomDrawer__Event.txt",
            editorTemplateName: "UA_Template_AtomEditor__Event.txt");

        public static readonly AtomType VALUE_LIST = new AtomType(displayName: "Value List",
            templateName: "UA_Template__ValueList.txt",
            drawerTemplateName: "UA_Template_AtomDrawer__ValueList.txt");

        // BASE_EVENT_REFERENCE_LISTENER is only used in thoses cases where a Variable does not make sense, eg. Void.
        public static readonly AtomType BASE_EVENT_REFERENCE_LISTENER = new AtomType(
            displayName: "Base Event Reference Listener",
            templateName: "UA_Template__BaseEventReferenceListener.txt");

        public static readonly AtomType EVENT_REFERENCE_LISTENER = new AtomType(displayName: "Event Reference Listener",
            templateName: "UA_Template__EventReferenceListener.txt");

        // EVENT_LISTENER is only used in thoses cases where a Event Reference listener does not make sense, eg. AtomBaseVariable.
        public static readonly AtomType EVENT_LISTENER = new AtomType(displayName: "Event Listener",
            name: "EventListener",
            templateName: "UA_Template__EventListener.txt");

        public static readonly AtomType REFERENCE = new AtomType(displayName: "Reference",
            templateName: "UA_Template__Reference.txt");

        // BASE_EVENT_REFERENCE is only used in thoses cases where a Variable does not make sense, eg. Void.
        public static readonly AtomType BASE_EVENT_REFERENCE = new AtomType(displayName: "Base Event Reference",
            templateName: "UA_Template__BaseEventReference.txt");

        public static readonly AtomType EVENT_REFERENCE = new AtomType(displayName: "Event Reference",
            templateName: "UA_Template__EventReference.txt");

        public static readonly AtomType UNITY_EVENT = new AtomType(displayName: "Unity Event",
            templateName: "UA_Template__UnityEvent.txt");

        public static readonly AtomType VARIABLE = new AtomType(displayName: "Variable",
            templateName: "UA_Template__Variable.txt",
            drawerTemplateName: "UA_Template_AtomDrawer__Variable.txt",
            editorTemplateName: "UA_Template_AtomEditor__Variable.txt");

        /// <summary>
        /// Containes all common atom types that are usually generated for a type.
        /// </summary>
        public static readonly List<AtomType> ALL_ATOM_TYPES = new List<AtomType>()
        {
            AtomTypes.EVENT,
            AtomTypes.VALUE_LIST,
            AtomTypes.EVENT_REFERENCE_LISTENER,
            AtomTypes.REFERENCE,
            AtomTypes.EVENT_REFERENCE,
            AtomTypes.UNITY_EVENT,
            AtomTypes.VARIABLE,
        };

        /// <summary>
        /// Graph explaining all the dependencies between Atoms.
        /// </summary>
        public static readonly Dictionary<AtomType, List<AtomType>> DEPENDENCY_GRAPH =
            new Dictionary<AtomType, List<AtomType>>()
            {
                {
                    AtomTypes.VALUE_LIST,
                    new List<AtomType>()
                    {
                        AtomTypes.EVENT
                    }
                },
                {
                    AtomTypes.BASE_EVENT_REFERENCE_LISTENER,
                    new List<AtomType>()
                    {
                        AtomTypes.EVENT, AtomTypes.BASE_EVENT_REFERENCE, AtomTypes.UNITY_EVENT
                    }
                },
                {
                    AtomTypes.EVENT_REFERENCE_LISTENER,
                    new List<AtomType>()
                    {
                        AtomTypes.EVENT, AtomTypes.EVENT_REFERENCE, AtomTypes.UNITY_EVENT
                    }
                },
                {
                    AtomTypes.REFERENCE, new List<AtomType>()
                    {
                        AtomTypes.VARIABLE, AtomTypes.EVENT
                    }
                },
                {
                    AtomTypes.BASE_EVENT_REFERENCE,
                    new List<AtomType>()
                    {
                        AtomTypes.EVENT
                    }
                },
                {
                    AtomTypes.EVENT_REFERENCE,
                    new List<AtomType>()
                    {
                        AtomTypes.VARIABLE, AtomTypes.EVENT
                    }
                },
                {
                    AtomTypes.VARIABLE, new List<AtomType>()
                    {
                        AtomTypes.EVENT
                    }
                },
            };
    }
}
