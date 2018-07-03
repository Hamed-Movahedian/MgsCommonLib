using System;
using System.Collections;
using UnityEngine;
//using UnityEditor;

namespace MgsCommonLib.Utilities
{
    public class MgsCoroutine
    {
        public static Func<double> GetTime;
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
                _lastTime = GetTime()
            };
            coroutine.StartEnumerator(enumerator);
        }
        private void StartEnumerator(IEnumerator enumerator)
        {

            while (enumerator.MoveNext())
            {
                if (GetTime() - _lastTime > _deltaTime || _forceUpdate)
                {
                    _lastTime = GetTime();
                    _forceUpdate = false;
                    if (_userFunc())
                        break;

                }
                var current = enumerator.Current as IEnumerator;
                if (current != null)
                    StartEnumerator(current);
            }
        }

        public static IEnumerator StartCoroutineRuntime(IEnumerator enumerator, Func<bool> function, double funcCallDeltaTime)
        {
            float lastTime = Time.realtimeSinceStartup;

            while (enumerator.MoveNext())
            {
                if (Time.realtimeSinceStartup - lastTime > funcCallDeltaTime)
                {
                    if (function())
                        yield return null;
                    else
                        yield break;

                    lastTime = Time.realtimeSinceStartup;
                }

                if(enumerator.Current!=null)
                    if (enumerator.Current is IEnumerator)
                        yield return StartCoroutineRuntime((IEnumerator) enumerator.Current, function, funcCallDeltaTime);
            }
        }
    }
}
