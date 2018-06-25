using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

namespace MgsCommonLib.Utilities
{
    public class MgsCoroutine
    {
        private double _lastTime;
        private Func<bool> _userFunc;
        private double _deltaTime;

        public static string Title;
        public static string Info;
        public static float Percentage;
        private static bool _forceUpdate;

        public static void ForceUpdate()
        {
            _forceUpdate = true;
        }
        public static void Start(IEnumerator enumerator, Func<bool> function, double funcCallDeltaTime)
        {
            MgsCoroutine coroutine = new MgsCoroutine
            {
                _userFunc = function,
                _deltaTime = funcCallDeltaTime,
                _lastTime = EditorApplication.timeSinceStartup
            };
            coroutine.StartEnumerator(enumerator);
        }
        private void StartEnumerator(IEnumerator enumerator)
        {

            while (enumerator.MoveNext())
            {
                if (EditorApplication.timeSinceStartup - _lastTime > _deltaTime || _forceUpdate)
                {
                    _lastTime = EditorApplication.timeSinceStartup;
                    _forceUpdate = false;
                    if (_userFunc())
                        break;

                }
                var current = enumerator.Current as IEnumerator;
                if (current != null)
                    StartEnumerator(current);
            }
        }
    }
}
