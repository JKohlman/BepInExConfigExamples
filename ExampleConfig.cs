using System.ComponentModel;

using BepInEx.Configuration;
using UnityEngine;

namespace ConfigExamples
{
    public static class ExampleConfig
    {
        public static void Initialize(ConfigFile cfg)
        {
            #region Examples 01 - Basics
            Examples01.BasicBoolean = cfg.Bind(
                section: Examples01.SECTION_NAME,
                key: "A boolean",
                defaultValue: true
            );
            Examples01.BasicInteger = cfg.Bind(
                section: Examples01.SECTION_NAME,
                key: "A number",
                defaultValue:0
            );
            Examples01.BasicString = cfg.Bind(
                section: Examples01.SECTION_NAME,
                key: "A string",
                defaultValue: "Hello!"
            );
            Examples01.BasicStringWithoutDefault = cfg.Bind<string>(
                section: Examples01.SECTION_NAME,
                key: "No default value",
                defaultValue: null
            );
            #endregion

            #region Examples 02 - Descriptions
            Examples02.StringWithDescription = cfg.Bind(
                section: Examples02.SECTION_NAME,
                key: "A string with a description",
                defaultValue: "Hover this entry's name!",
                description: "This is a description"
            );
            Examples02.StringWithConfigDescription = cfg.Bind(
                section: Examples02.SECTION_NAME,
                key: "A string with a ConfigDescription",
                defaultValue: "Hover this entry's name!",
                new ConfigDescription(description: "This is a ConfigDescription")
            );
            Examples02.StringOutOfOrder = cfg.Bind(
                section: Examples02.SECTION_NAME,
                key: "A string that is out of order!",
                defaultValue: "Configs are ordered by key (name) by default!",
                new ConfigDescription(description: "Configs are ordered by key by default!")
            );
            #endregion

            #region Examples 03 - Order
            Examples03.StringOrder1 = cfg.Bind(
                section: Examples03.SECTION_NAME,
                key: "Ze last string but ordered first!",
                defaultValue: "Would be last by default",
                new ConfigDescription(
                    description: "Order can override sorting!",
                    tags: new ConfigurationManagerAttributes { Order = 3 }
                )
            );
            Examples03.StringOrder2 = cfg.Bind(
                section: Examples03.SECTION_NAME,
                key: "The middle string but ordered last!",
                defaultValue: "Would be middle by default",
                new ConfigDescription(
                    description: "Order can override sorting!",
                    tags: new ConfigurationManagerAttributes { Order = 1 }
                )
            );
            Examples03.StringOrder3 = cfg.Bind(
                section: Examples03.SECTION_NAME,
                key: "A string that would normally be first!",
                defaultValue: "Would be first by default",
                new ConfigDescription(
                    description: "Order can override sorting!",
                    tags: new ConfigurationManagerAttributes { Order = 2 }
                )
            );
            #endregion

            #region Examples 04 - Allowed Values
            // 0f-1f defaults to a percentage
            Examples04.Percentage = cfg.Bind(
                section: Examples04.SECTION_NAME,
                key: "A float percentage",
                defaultValue: 0f,
                new ConfigDescription(
                    description: "0f-1f defaults to a percentage",
                    acceptableValues: new AcceptableValueRange<float>(0f, 1f),
                    tags: new ConfigurationManagerAttributes { Order = 5 }
                )
            );
            // 0-100 defaults to a percentage
            Examples04.Percentage2 = cfg.Bind(
                section: Examples04.SECTION_NAME,
                key: "An integer percentage",
                defaultValue: 5,
                new ConfigDescription(
                    description: "0-100 defaults to a percentage",
                    acceptableValues: new AcceptableValueRange<int>(0, 100),
                    tags: new ConfigurationManagerAttributes { Order = 4 }
                )
            );
            Examples04.NonPercentageRange = cfg.Bind(
                section: Examples04.SECTION_NAME,
                key: "An integer range",
                defaultValue: 6,
                new ConfigDescription(
                    description: "A range that is not a percentage",
                    acceptableValues: new AcceptableValueRange<int>(0, 15),
                    tags: new ConfigurationManagerAttributes { Order = 3 }
                )
            );
            Examples04.DropDownSelection = cfg.Bind(
                section: Examples04.SECTION_NAME,
                key: "An integer selection",
                defaultValue: 1,
                new ConfigDescription(
                    description: "A dropdown to choose from",
                    acceptableValues: new AcceptableValueList<int>(new int[] { 1, 2, 3, 5, 8, 13 }),
                    tags: new ConfigurationManagerAttributes { Order = 2 }
                )
            );
            Examples04.DropDownEnum = cfg.Bind(
                section: Examples04.SECTION_NAME,
                key: "An enum selection",
                defaultValue: Examples04.EnumValues.Entry2,
                new ConfigDescription(
                    description: "Enums can have custom labels",
                    tags: new ConfigurationManagerAttributes { Order = 1 }
                )
            );
            #endregion

            #region Examples 05 - Hotkey
            Examples05.ShortCut = cfg.Bind(
                section: Examples05.SECTION_NAME,
                key: "A keyboard shortcut",
                defaultValue: new KeyboardShortcut(KeyCode.LeftControl)
            );
            #endregion

            #region Examples 06 - Custom Drawers

            Examples06.Choice = cfg.Bind(
                section: Examples06.SECTION_NAME,
                key: "A keyboard shortcut",
                defaultValue: Examples06.MultipleChoiceOptions.NONE,
                new ConfigDescription(
                    description: "OOOOH FANCYYY",
                    tags: new ConfigurationManagerAttributes { Order = 1, CustomDrawer = Examples06.MultipleChoiceDrawer }
                )
            );
            #endregion
        }

        // Examples 01 - Basics
        public static class Examples01
        {
            public const string SECTION_NAME = "Examples 01 - Basics";
            public static ConfigEntry<bool> BasicBoolean { get; internal set; }
            public static ConfigEntry<int> BasicInteger { get; internal set; }
            public static ConfigEntry<string> BasicString { get; internal set; }
            public static ConfigEntry<string> BasicStringWithoutDefault { get; internal set; }
        }

        // Examples 02 - Descriptions
        public static class Examples02
        {
            public const string SECTION_NAME = "Examples 02 - Descriptions";
            public static ConfigEntry<string> StringWithDescription { get; internal set; }
            public static ConfigEntry<string> StringWithConfigDescription { get; internal set; }
            public static ConfigEntry<string> StringOutOfOrder { get; internal set; }
        }

        // Examples 03 - Order
        public static class Examples03
        {
            public const string SECTION_NAME = "Examples 03 - Order";
            public static ConfigEntry<string> StringOrder1 { get; internal set; }
            public static ConfigEntry<string> StringOrder2 { get; internal set; }
            public static ConfigEntry<string> StringOrder3 { get; internal set; }
        }

        // Examples 04 - Allowed Values
        public static class Examples04
        {
            public const string SECTION_NAME = "Examples 04 - Allowed Values";
            public static ConfigEntry<float> Percentage { get; internal set; }
            public static ConfigEntry<int> Percentage2 { get; internal set; }
            public static ConfigEntry<int> NonPercentageRange { get; internal set; }

            public static ConfigEntry<int> DropDownSelection { get; internal set; }

            public enum EnumValues
            {
                Entry1,
                Entry2,
                Entry3,
                [Description("This is a C# feature")]
                Entry4,
                Entry5
            }
            public static ConfigEntry<EnumValues> DropDownEnum { get; internal set; }
        }

        // Examples 05 - Hotkey
        public static class Examples05
        {
            public const string SECTION_NAME = "Examples 05 - Hotkey";
            public static ConfigEntry<KeyboardShortcut> ShortCut { get; internal set; }
        }

        // Examples 06 - Custom Drawers
        public static class Examples06
        {
            public const string SECTION_NAME = "Examples 06 - Custom Drawers";

            public static ConfigEntry<MultipleChoiceOptions> Choice { get; internal set; }
            public enum MultipleChoiceOptions { A, B, C, D, NONE }

            public static void MultipleChoiceDrawer(ConfigEntryBase entry)
            {
                if (GUILayout.Toggle((MultipleChoiceOptions)entry.BoxedValue == MultipleChoiceOptions.A, "A"))
                    entry.BoxedValue = MultipleChoiceOptions.A;
                if (GUILayout.Toggle((MultipleChoiceOptions)entry.BoxedValue == MultipleChoiceOptions.B, "B"))
                    entry.BoxedValue = MultipleChoiceOptions.B;
                if (GUILayout.Toggle((MultipleChoiceOptions)entry.BoxedValue == MultipleChoiceOptions.C, "C"))
                    entry.BoxedValue = MultipleChoiceOptions.C;
                if (GUILayout.Toggle((MultipleChoiceOptions)entry.BoxedValue == MultipleChoiceOptions.D, "D"))
                    entry.BoxedValue = MultipleChoiceOptions.D;
            }
        }
    }
}
