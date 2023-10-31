using System;
using UnityEngine;

namespace Novus
{
    public class SingletonT<T> : MonoBehaviour where T : Component
    {
        [SerializeField] private bool dontDestroyOnLoad = false;
        
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance != null) 
                    return _instance;
                _instance = FindObjectOfType<T>();
                if (_instance != null) 
                    return _instance;
                
                var obj = new GameObject {name = typeof(T).Name};
                _instance = obj.AddComponent<T>();
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            _instance = this as T;

            if (dontDestroyOnLoad)
                DontDestroyOnLoad(gameObject);
        }
    }
}