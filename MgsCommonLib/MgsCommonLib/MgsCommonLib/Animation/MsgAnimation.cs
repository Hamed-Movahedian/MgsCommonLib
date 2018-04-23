using System;
using System.Collections;
using UnityEngine;

namespace MgsCommonLib.Animation
{
    public static class MsgAnimation
    {
        public static void RunAnimation(this MonoBehaviour component, float duration, Action<float> setValues)
        {
            component.StartCoroutine(RunAnimation(duration, setValues));
        }

        public static IEnumerator RunAnimation(float duration, Action<float> setValues)
        {
            var curve = AnimationCurve.EaseInOut(0, 0, duration, 1);

            float time = 0;

            setValues(0);
            while (time <= duration)
            {
                yield return null;
                time += Time.deltaTime;
                setValues(curve.Evaluate(time));
            }
            if(curve.Evaluate(time)!=1)
                setValues(1);
        }

        public static void RunColorAnimation(this MonoBehaviour component, Color startColor, Color endColor, float duration, Action<Color> setColor)
        {
            component.StartCoroutine(RunColorAnimation(startColor, endColor, duration, setColor));
        }
        public static IEnumerator RunColorAnimation(Color startColor, Color endColor, float duration, Action<Color> setColor)
        {
             yield return RunAnimation(duration, v => setColor(Color.Lerp(startColor, endColor, v)));
        }
    }
}