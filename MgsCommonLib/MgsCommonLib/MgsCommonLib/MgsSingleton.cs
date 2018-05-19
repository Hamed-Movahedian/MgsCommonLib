using System;
using System.Linq;
using MgsCommonLib.Utilities;
using UnityEngine;

namespace MgsCommonLib
{
    public class MgsSingleton<T> : MonoBehaviour where T : MgsSingleton<T>
    {
        #region Instance

        private static T _instance;

        public static T Instance
        {
            get
            {
                // instance not set
                if (_instance == null)
                {
                    // get all objects
                    var objects = FindObjectsOfType<T>().ToList();

                    // If not found create a new one
                    if (objects.Count == 0)
                    {
                        Debug.LogError($"{typeof(T).Name} doesn't exist !!!".ToUpper());
                        return null;
                    }

                    // if more than one object destroy them
                    while (objects.Count > 1)
                        objects.RemoveFirstAndReturn().SafeDestroy();

                    _instance = objects[0];
                }

                return _instance;

            }
        }

        #endregion

        #region Awake and Initialize

        private bool _initialized = false;

        protected virtual void Initialize(){}

        private void Awake()
        {
            if (!_initialized)
            {
                Initialize();
                _initialized = true;

            }
        }

        #endregion
    }
}