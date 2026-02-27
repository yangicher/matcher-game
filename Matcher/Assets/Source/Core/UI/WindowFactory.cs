using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Matcher.Core.UI
{
    public class WindowFactory
    {
        private readonly WindowMapper _mapper;
        private readonly Transform _uiRoot;

        public WindowFactory(WindowMapper mapper, Transform uiRoot)
        {
            _mapper = mapper;
            _uiRoot = uiRoot;
        }

        public TWindow CreateWindow<TWindow>(Type controllerType) where TWindow : IBaseWindow
        {
            string path = _mapper.GetPath(controllerType);
            GameObject prefab = Resources.Load<GameObject>(path);
            
            if (prefab == null)
                throw new Exception($"No prefab at path: {path}");

            GameObject instance = Object.Instantiate(prefab, _uiRoot);
            return instance.GetComponent<TWindow>();
        }
    }
}