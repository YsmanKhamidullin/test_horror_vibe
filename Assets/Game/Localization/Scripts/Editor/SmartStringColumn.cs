#if UNITY_EDITOR

using UnityEditor.Localization;
using UnityEditor.Localization.Plugins.Google.Columns;
using UnityEngine.Localization.Metadata;
using UnityEngine.Localization.Tables;

namespace Game.Localization.Scripts.Editor
{
    public class SmartStringColumn : LocaleMetadataColumn<SmartFormatTag>
    {
        public override PushFields PushFields => PushFields.Value;

        public override void PullMetadata(StringTableEntry entry, SmartFormatTag metadata, string cellValue,
            string cellNote)
        {
            entry.IsSmart = !string.IsNullOrEmpty(cellValue);
        }

        public override void PushHeader(StringTableCollection collection, out string header, out string headerNote)
        {
            header = $"{LocaleIdentifier.ToString()}";
            headerNote = null;
        }

        public override void PushMetadata(SmartFormatTag metadata, out string value, out string note)
        {
            value = "x";
            note = null;
        }
    }
}
#endif