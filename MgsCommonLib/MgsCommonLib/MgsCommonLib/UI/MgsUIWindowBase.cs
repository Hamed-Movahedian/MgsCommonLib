using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MgsCommonLib.Animation;
using MgsCommonLib.Utilities;
using UnityEngine;

namespace MgsCommonLib.UI
{
    public abstract class MgsUIWindowBase : MonoBehaviour
    {
        [HideInInspector]
        public string Result;

        public Change OnChange;

        private readonly Dictionary<Type, List<Component>> _componentListDic =
            new Dictionary<Type, List<Component>>();

        private Animator _animator;
        protected bool _isDone = false;

        public delegate void Change();

        public static MgsUIWindow GetWindow(string name)
        {
            // name to lower case
            name = name.ToLower();

            // find window
            var window = FindObjectsOfType<MgsUIWindow>()
                .FirstOrDefault(w => w.name.ToLower() == name);

            if (window != null)
                return window;

            Debug.LogError($"Window {name} not found!!");

            return null;

        }

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
                throw new Exception($"More than one component of type {componentType.Name} with name {componentName} in window {name} exist!!!");
            }

            if (components.Count < 1)
            {
                throw new Exception($"No component of type {componentType.Name} with name {componentName} in window {name} exist!!!");
            }

            #endregion

            // return the only one matching component
            return (T)components[0];

        }

        public IEnumerator Hide()
        {
            if (_animator == null)
                _animator = GetComponent<Animator>();

            if (_animator)
                yield return _animator.SetTriggerAndWaitForTwoStateChanges("Hide");

        }

        public IEnumerator Show()
        {
            transform.SetActiveChilds(true);

            OnShow();
            if (_animator == null)
                _animator = GetComponent<Animator>();


            if (_animator)
                yield return _animator.SetTriggerAndWaitForTwoStateChanges("Show");

        }

        protected virtual void OnShow()
        {

        }

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

        public void StartCorotineMethod(string methodName)
        {
            StartCoroutine(methodName);
        }

        private void OnValidate()
        {
            OnChange?.Invoke();
        }

        public virtual void Close(string result)
        {
            _isDone = true;

            Result = result;
        }

        public IEnumerator WaitForClose(bool show, bool hide)
        {
            if (show)
                yield return Show();

            yield return WaitForClose();

            if (hide)
                yield return Hide();
        }
    }
}