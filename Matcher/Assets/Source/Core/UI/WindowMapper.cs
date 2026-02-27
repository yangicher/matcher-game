using System;
using System.Collections.Generic;

namespace Matcher.Core.UI
{
    public class WindowMapper
    {
        private readonly Dictionary<Type, string> _prefabPaths = new Dictionary<Type, string>();

        public void Register<TController>(string resourcesPath) where TController : IWindowController
        {
            _prefabPaths[typeof(TController)] = resourcesPath;
        }
        
        public void Unregister<TController>() where TController : IWindowController
        {
            _prefabPaths.Remove(typeof(TController));
        }

        public string GetPath(Type controllerType)
        {
            if (_prefabPaths.TryGetValue(controllerType, out string path))
                return path;
                
            throw new Exception($"Nothing for: {controllerType.Name}");
        }
    }
}