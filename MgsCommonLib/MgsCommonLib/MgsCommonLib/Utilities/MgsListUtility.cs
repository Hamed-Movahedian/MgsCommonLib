using System.Collections.Generic;
using System.Linq;

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

        #region EqaulTo

        public static bool IsEqualTo<T>(this List<T> l1, List<T> l2)
        {
            if (l1.Count != l2.Count)
                return false;

            return l2 
                .Select((item, index) => new { item, index })
                .All(li => l1[li.index].Equals(li.item));
        }
        #endregion

    }
}