using System;
using TMPro;

namespace Pancake.Localization
{
    using UnityEngine;

    [EditorIcon("icon_default")]
    public class LocaleTMPFontComponent : LocaleComponentGeneric<LocaleTMPFont, TMP_FontAsset>
    {
        private void Reset()
        {
            TrySetComponentAndPropertyIfNotSet<TextMeshProUGUI>("font");
            TrySetComponentAndPropertyIfNotSet<TextMeshPro>("font");
        }
    }
}