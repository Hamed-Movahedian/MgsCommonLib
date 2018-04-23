using UnityEngine;

namespace MgsCommonLib.Utilities
{
    public static class MgsColorUtility
    {
        public static Color FadeIn(float v)
        {
            return new Color(1, 1, 1, Mathf.Lerp(0, 1, v));
        }
        public static Color FadeOut(float v)
        {
            return new Color(1, 1, 1, Mathf.Lerp(1, 0, v));
        }
    }
}