using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ArabicSupport;
using MgsCommonLib.Animation;
using MgsCommonLib.Utilities;
using UnityEngine;
using UnityEngine.UI;

namespace MgsCommonLib.UI
{
    public class UIWindow : MonoBehaviour
    {

        #region Get Window
        public static UIWindow GetWindow(string name)
        {
            // name to lower case
            name = name.ToLower();

            // find window
            var window = FindObjectsOfType<UIWindow>()
                .FirstOrDefault(w => w.name.ToLower() == name);

            if (window != null)
                return window;

            Debug.LogError($"Window {name} not found!!");

            return null;

        }
        #endregion

        #region Dialogue

        public static UIWindow Dialogue;

        public static IEnumerator ShowDialogueWait(string windowName, string message, params string[] buttons)
        {
            SetDialogue(windowName, message, buttons);

            yield return Dialogue.ShowWaitForAction();
        }
        public static IEnumerator ShowDialogueWaitHide(string windowName, string message, params string[] buttons)
        {
            SetDialogue(windowName, message, buttons);

            yield return Dialogue.ShowWaitForActionHide();
        }
        public static IEnumerator ShowDialogue(string windowName, string message, params string[] buttons)
        {
            SetDialogue(windowName, message, buttons);

            yield return Dialogue.Show();
        }

        public static void SetDialogue(string windowName, string message,params string[] buttons)
        {
            // Get window
            Dialogue = GetWindow(windowName);

            // Setup window
            Dialogue.GetComponentByName<Text>("Message").text = message;

            for (var i = 0; i < buttons.Length; i++)
                SetupDialogueButton("Button" + i, buttons[i]);
        }

        private static void SetupDialogueButton(string name, string lable)
        {
            Button button = Dialogue.GetComponentByName<Button>(name);

            button.gameObject.SetActive(lable == null);

            if (lable != null)
            {
                Text text = button.GetComponentInChildren<Text>(true);
                if (text)
                    text.text = ArabicFixer.Fix(lable);
            }
        }

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

        private readonly Dictionary<Type, List<Component>> _componentListDic =
            new Dictionary<Type, List<Component>>();

        public T GetComponentByName<T>(string componentName) where T : Component
        {
            #region Get all component of type T and name componentName

            // Convert componentName to lower case
            componentName = componentName.ToLower();

            // cache component type
            var componentType = typeof(T);

            // Add list of components to dic if not exist
            if (!_componentListDic.ContainsKey(componentType))
                _componentListDic.Add(
                    componentType,
                    GetComponentsInChildren<T>(true)
                        .Select(c => (Component)c)
                        .ToList());

            // Get all component of type T and name as componentName lowerCase
            List<Component> components =
                _componentListDic[componentType]
                    .Where(c => c.name.ToLower() == componentName)
                    .ToList();

            #endregion

            #region Check only one component exist

            if (components.Count > 1)
            {
                Debug.LogError($"More than one component of type {componentType.Name} with name {componentName} in window {name} exist!!!");
                return null;
            }

            if (components.Count < 1)
            {
                Debug.LogError($"No component of type {componentType.Name} with name {componentName} in window {name} exist!!!");
                return null;
            }

            #endregion

            // return the only one matching component
            return (T)components[0];

        }

        #endregion

        #region Show and Hide

        private Animator _animator;

        public IEnumerator Hide()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();

            if (_animator)
                yield return _animator.SetTriggerAndWaitForTwoStateChanges("Hide");

            transform.SetActiveChilds(false);
        }

        public IEnumerator Show()
        {
            transform.SetActiveChilds(true);

            if (_animator == null)
                _animator = GetComponent<Animator>();

            if (_animator)
                yield return _animator.SetTriggerAndWaitForTwoStateChanges("Show");

        }

        #endregion

        #region Close

        private bool _isDone = false;

        public void Close()
        {
            _isDone = true;
        }

        public IEnumerator WaitForClose()
        {
            _isDone = false;

            while (!_isDone)
                yield return null;
        }


        #endregion

        #region Auxilury methods

        public void StartCorotineMethod(string methodName)
        {
            StartCoroutine(methodName);
        }

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

        #region Special Components

        private Text _messageText;

        public void SetTextMessage(string messageText)
        {
            if (_messageText == null)
                _messageText = GetComponentByName<Text>("Message");
            _messageText.text = messageText;
        }

        #endregion
    }
}

