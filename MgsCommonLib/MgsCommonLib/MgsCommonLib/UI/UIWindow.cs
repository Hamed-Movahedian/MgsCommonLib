using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MgsCommonLib.Animation;
using MgsCommonLib.Utilities;
using UnityEngine;

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

        #region Actions

        private readonly Dictionary<string, Func<UIWindow, IEnumerator>> _actionDic =
            new Dictionary<string, Func<UIWindow, IEnumerator>>();

        public void RunAction(string actionName)
        {
            if (_actionDic.ContainsKey(actionName))
                StartCoroutine(_actionDic[actionName](this));
        }

        public void SetAction(string actionName, Func<UIWindow, IEnumerator> action)
        {
            if (_actionDic.ContainsKey(actionName))
                _actionDic[actionName] = action;
            else
                _actionDic.Add(actionName, action);
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
                        .Select(c=>(Component)c)
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
            return (T) components[0];

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


        #endregion

        #region ShowAndWaitForClose
        public IEnumerator ShowAndWaitForClose()
        {
            yield return Show();

            _isDone = false;

            while (!_isDone)
                yield return null;

            yield return Hide();

        }
        #endregion

    }
}

