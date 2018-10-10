using System.Collections;
using System.Collections.Generic;
using MgsCommonLib.Theme;
using UnityEngine;
using UnityEngine.UI;

namespace MgsCommonLib.UI
{
    public class MgsDialougWindow : MgsUIWindowBase
    {
        public Text Title;
        public Text Message;
        public Text Timer;
        public Image Icon;

        public List<Button> Buttons;
        private List<string> _buttonLables;

        public IEnumerator Display(string title, string message, string icon, List<string> buttonLables,float timer=0)
        {
            Title.text = ThemeManager.Instance.LanguagePack.GetLable(title);
            Message.text = ThemeManager.Instance.LanguagePack.GetLable(message);
            Icon.sprite = ThemeManager.Instance.IconPack.GetIcon(icon);
            _buttonLables = buttonLables;
            for (int i = 0; i < Buttons.Count; i++)
            {
                if (i < buttonLables.Count)
                {
                    // set button text
                    Buttons[i].GetComponentInChildren<Text>().text =
                        ThemeManager.Instance.LanguagePack.GetLable(buttonLables[i]);

                    Buttons[i].onClick.RemoveAllListeners();
                    var i1 = i;
                    Buttons[i].onClick.AddListener(() => { Close(buttonLables[i1]); });
                }

                Buttons[i].gameObject.SetActive(i < buttonLables.Count);
            }

            yield return Show();

            Timer.text = "";
            if (timer>0)
            {
                StartCoroutine(ShowTimer(timer));
            }

            yield return WaitForClose(false,true);

        }

        private IEnumerator ShowTimer(float timer)
        {
            float timerPassed = 0;

            while (timerPassed<=timer)
            {
                Timer.text = Mathf.RoundToInt(timer - timerPassed).ToString();
                yield return null;
                timerPassed += Time.deltaTime;
            }

            Close("Time Out");
        }

        #region Back (ESCAPE)

        public void Update()
        {
            if (_buttonLables != null)
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (_buttonLables.Contains("Back"))
                        Close("Back");

                    if (_buttonLables.Contains("back"))
                        Close("back");
                }
        }

        #endregion


    }
}