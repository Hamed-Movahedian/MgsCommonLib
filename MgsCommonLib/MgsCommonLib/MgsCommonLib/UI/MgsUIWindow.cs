using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MgsCommonLib.UI
{
    public class MgsUIWindow : MgsUIWindowBase
    {
        #region Public

        public List<string> ActionList = new List<string>();

        #endregion
 
        #region Actions

        private string _lastActionName;

        public IEnumerator WaitForAction()
        {
            _lastActionName = "";
            while (_lastActionName == "")
                yield return null;
        }

        public void RunAction(string actionName)
        {
            _lastActionName = actionName;
        }

        public bool CheckLastAction(string actionName)
        {
            return _lastActionName.ToLower() == actionName.ToLower();
        }


        #endregion

        #region GetComponentByName

        #endregion

        #region Show and Hide

        #endregion

        #region Close

        public override void Close(string result)
        {
            base.Close(result);

            OnClose();

            foreach (var action in ActionList)
                if (action == result)
                    return;
            throw new Exception($"Action {result} isn't set in window {name} ");
        }

        public virtual void OnClose()
        {
            
        }

        #endregion

        #region Auxilury methods

        public IEnumerator ShowWaitForAction()
        {
            yield return Show();
            yield return WaitForAction();

        }
        public IEnumerator ShowWaitForClose()
        {
            yield return Show();
            yield return WaitForClose();
        }
        public IEnumerator ShowWaitForCloseHide()
        {
            yield return Show();
            yield return WaitForClose();
            yield return Hide();
        }

        public IEnumerator ShowWaitForActionHide()
        {
            yield return Show();
            yield return WaitForAction();
            yield return Hide();
        }

        #endregion

        #region Set special Components

        private Text _messageText;
        private Image _iconImage;
        private Image _fillImage;

        public void SetTextMessage(string messageText)
        {
            if (_messageText == null)
                _messageText = GetComponentByName<Text>("Message");
            _messageText.text = messageText;
        }

        public void SetIcon(Sprite icon)
        {
            if (_iconImage == null)
                _iconImage = GetComponentByName<Image>("Icon");
            _iconImage.sprite = icon;
        }
        public void SetFillAmount(float amount)
        {
            if (_fillImage == null)
                _fillImage = GetComponentByName<Image>("ProgressFill");

            _fillImage.fillAmount = amount;
        }

        #endregion

        #region Refresh 

        public virtual void Refresh()
        {

        }

        #endregion
    }
}

