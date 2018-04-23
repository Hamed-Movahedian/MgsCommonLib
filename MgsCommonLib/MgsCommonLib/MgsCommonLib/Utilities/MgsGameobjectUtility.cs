using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MgsCommonLib.Utilities
{
    public static class MgsGameobjectUtility
    {
        #region SafeDestroy

        /// <summary>
        /// Check is editor or play
        /// </summary>
        /// <param name="component"></param>
        public static void SafeDestroy(this Component component)
        {
            SafeDestroy(component.gameObject);
        }

        /// <summary>
        /// Check is editor or play
        /// </summary>
        /// <param name="gameObject"></param>
        public static void SafeDestroy(this GameObject gameObject)
        {
            if (Application.isPlaying)
                Object.Destroy(gameObject);
            else
                Object.DestroyImmediate(gameObject);
        }

        #endregion

        #region CreateComponent

        public static T CreateComponent<T>() where T : Component
        {
            return CreateComponent<T>(typeof(T).Name);
        }

        public static T CreateComponent<T>(string name) where T : Component
        {
            var gameObject = new GameObject(name);
            return gameObject.AddComponent<T>();
        }

        #endregion

        #region DeleteAllChilds

        public static void DeleteAllChilds(this Component component)
        {
            DeleteAllChilds(component.gameObject);
        }

        public static void DeleteAllChilds(this GameObject gameObject)
        {
            while (gameObject.transform.childCount > 0)
                gameObject.transform.GetChild(0).SafeDestroy();
        }

        #endregion

        public static void SortChildsByName(this Transform transform)
        {
            transform
                .GetChilds()
                .OrderByDescending(t=>t.name)
                .ToList()
                .ForEach(t=>t.SetSiblingIndex(0));
        }

        public static List<Transform> GetChilds(this Transform transform)
        {
            return Enumerable
                .Range(0, transform.childCount)
                .Select(transform.GetChild)
                .ToList();
        }
    }
}