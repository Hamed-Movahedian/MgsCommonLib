using System.Collections;
using System.Collections.Generic;
using MgsCommonLib.Theme;
using UnityEngine;
using UnityEngine.UI;

namespace MgsCommonLib.UI
{
    public class MgsDialougWindow : MgsUIWindowBase
    {
        public Text Title, Message;
        public Image Icon;
        public List<Button> Buttons;

        public IEnumerator Display(string title, string message, Sprite icon, List<string> buttonLables)
        {
            Title.text = title;
            Message.text = message;
            Icon.sprite = icon;
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (i < buttonLables.Count)
                {
                    // set button text
                    Buttons[i].GetComponentInChildren<Text>().text =
                        ThemeManager.Instance.LanguagePack.GetLable(buttonLables[i]);

                    Buttons[i].onClick.RemoveAllListeners();
                    var i1 = i;
                    Buttons[i].onClick.AddListener(()=>{Close(buttonLables[i1]);});
                }
            }

            yield return WaitForClose(true, true);

        }
    }
}