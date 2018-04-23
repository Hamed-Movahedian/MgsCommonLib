using System;
using System.Linq;
using MgsCommonLib.Utilities;
using UnityEngine;

namespace MgsCommonLib
{
    public class MgsSingleton<T> : MonoBehaviour where T : MgsSingleton<T>
    {
        #region Propertes

        private static T _instance;
        private bool _initialized=false;

        public static T Instance
        {
            get
            {
                // instanse not set
                if (_instance == null)
                {
                    // get all objects
                    var objects = FindObjectsOfType<T>().ToList();

                    // not found !!!
                    if (objects.Count == 0)
                    {
                        _instance = MgsGameobjectUtility.CreateComponent<T>();

                        _instance.Initialize();
                        _instance._initialized = true;

                        Debug.LogError(string.Format("{0} must exist !!!", typeof(T).Name));
                    }

                    // more than one object
                    while (objects.Count > 1)
                        objects.RemoveFirstAndReturn().SafeDestroy();

                    _instance = objects[0];
                }

                return _instance;

            }
        }

        #endregion

        protected virtual void Initialize(){}

        private void Awake()
        {
            if(!_initialized)
                Initialize();
        }

    }
}