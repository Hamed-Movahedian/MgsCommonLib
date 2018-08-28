using System.Collections;
using System.Collections.Generic;
using MgsCommonLib.Theme;
using UnityEngine.UI;

namespace MgsCommonLib.UI
{
    public class MgsProgressWindow : MgsUIWindowBase
    {
        public Text  Message;

        public IEnumerator Display(string message)
        {
            Message.text = ThemeManager.Instance.LanguagePack.GetLable(message);

            return Show();
        }
    }
}
