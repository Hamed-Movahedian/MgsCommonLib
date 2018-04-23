using UnityEngine;

namespace MgsCommonLib.Utilities
{
    public static class MgsUiUtility
    {
        #region Validate as ui object

        /// <summary>
        /// check and create parent canvas and nessesry rect transforms
        /// </summary>
        /// <param name="gameObject"></param>
        public static void ValidateAsUiGameObject(this GameObject gameObject)
        {
            // Check Parent canvas and nessesery rect transforms
            GetParentCanvas(gameObject);
        }

        #endregion

        #region Canvas

        /// <summary>
        /// Create new RectTransforms if canvas not exist and need rect transforms
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static Canvas GetParentCanvas(GameObject gameObject)
        {
            var canvas = gameObject.GetComponentInParent<Canvas>();

            return canvas ?? CreateNewParentCanvas(gameObject);
        }

        /// <summary>
        /// Create new RectTransforms if needed
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static Canvas CreateNewParentCanvas(GameObject gameObject)
        {
            var canvas = CreateNewCanvas("Canvas");

            gameObject.SetUiParent(canvas);

            return canvas;
        }
        
        public static Canvas CreateNewCanvas(string name)
        {
            return MgsGameobjectUtility.CreateComponent<Canvas>();
        }

        #endregion

        #region SetUiParent

        /// <summary>
        /// Create new RectTransforms if needed
        /// </summary>
        private static void SetUiParent(this GameObject gameObject, Component parent)
        {
            gameObject.SetUiParent(parent);
        }        
        
        /// <summary>
        /// Create new RectTransforms if needed
        /// </summary>
        private static void SetUiParent(this GameObject gameObject, GameObject parent)
        {
            gameObject.GetRectTransform().SetParent(parent.GetRectTransform());
        }

        #endregion

        #region GetRectTransform

        /// <summary>
        /// Create a new RectTransform if not exist!
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        public static RectTransform GetRectTransform(this Component component)
        {
            return GetRectTransform(component.gameObject);
        }
        /// <summary>
        /// Create a new RectTransform if not exist!
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static RectTransform GetRectTransform(this GameObject gameObject)
        {
            RectTransform rectTransform = gameObject.GetComponent<RectTransform>();

            if (rectTransform != null) return rectTransform;
            else
                return gameObject.AddComponent<RectTransform>();
        }

        #endregion

        #region Create

        public static GameObject CreateNewUiChild(this Component parent, string childName)
        {
            return CreateNewUiChild(parent.gameObject, childName);
        }

        public static GameObject CreateNewUiChild(this GameObject parent, string childName)
        {
            var child = CreateNewUiGameObject(childName);

            child.SetUiParent(parent);

            child.GetRectTransform().localPosition=Vector3.zero;

            return child;
        }

        public static GameObject CreateNewUiGameObject(string name)
        {
            var gameObject = new GameObject(name);

            gameObject.GetRectTransform();

            return gameObject;
        }

        #endregion

    }
}
