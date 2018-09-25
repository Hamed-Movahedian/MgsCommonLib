using System.Collections;
using System.Collections.Generic;
using MgsCommonLib.Theme;
using UnityEngine.UI;

namespace MgsCommonLib.UI
{
    public class MgsProgressWindow : MgsUIWindowBase
    {
        public Text  Message;

        public IEnumerator Display(string message, bool show=true)
        {
            Message.text = ThemeManager.Instance.LanguagePack.GetLable(message);

            if (show)
                return Show();
            else
                return null;
        }
    }
}
