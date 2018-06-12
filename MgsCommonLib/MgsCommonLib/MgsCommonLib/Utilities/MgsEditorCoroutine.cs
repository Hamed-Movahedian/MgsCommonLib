using System;
using System.Collections;
using UnityEngine;
using UnityEditor;

namespace MgsCommonLib.Utilities
{
    public class MgsEditorCoroutine
    {
        private double _lastTime;
        private Func<bool> _userFunc;
        private double _deltaTime;

        public static void Start(IEnumerator enumerator, Func<bool> function, double funcCallDeltaTime)
        {
            MgsEditorCoroutine editorCoroutine = new MgsEditorCoroutine
            {
                _userFunc = function,
                _deltaTime = funcCallDeltaTime,
                _lastTime = EditorApplication.timeSinceStartup
            };
            editorCoroutine.StartEnumerator(enumerator);
        }
        private void StartEnumerator(IEnumerator enumerator)
        {

            while (enumerator.MoveNext())
            {
                if (EditorApplication.timeSinceStartup - _lastTime > _deltaTime)
                {
                    _lastTime = EditorApplication.timeSinceStartup;
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
