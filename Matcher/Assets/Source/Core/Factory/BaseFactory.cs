using System;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Matcher.Core.Factory
{
    public class BaseFactory
    {
        protected T TryCreateObject<T>(string path, Transform parent = null)
            where T : Object
        {
            if (string.IsNullOrEmpty(path))
            {
                return null;
            }
            
            T prototype = Resources.Load<T>(path);
            if (prototype == null)
            {
                return null;
            }

            return CreateObject(prototype, parent);
        }
        
        protected T CreateObject<T>(string path, Transform parent = null) where T : Object
        {
            T prototype = Resources.Load<T>(path);
            if (prototype == null)
            {
                throw new NullReferenceException($"Couldn't find {path} object");
            }

            return CreateObject(prototype, parent);
        }
        
        private T CreateObject<T>(T prototype, Transform parent = null) where T : Object
        {
            var gameObject = (parent == null)
                ? Object.Instantiate(prototype)
                : Object.Instantiate(prototype, parent);
            
            var objectInstance = gameObject.GetComponent<T>();
            return objectInstance;
        }
    }
}