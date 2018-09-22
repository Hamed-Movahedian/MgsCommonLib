using System.Collections.Generic;

namespace MgsCommonLib.Utilities
{
    public static class MsgListUtility
    {
        #region RemoveFirstAndReturn

        public static T RemoveFirstAndReturn<T>(this List<T> list)
        {
            return RemoveAtAndReturn(list, 0);
        }

        private static T RemoveAtAndReturn<T>(this List<T> list, int index)
        {
            var item = list[index];
            list.RemoveAt(index);
            return item;
        }

        #endregion

        #region Resize

        public static void Resize<T>(this List<T> list, int size)
        {
            while (list.Count < size)
                list.Add(default(T));

            while (list.Count > size)
                list.RemoveAt(list.Count - 1);
        }
        #endregion

    }
}