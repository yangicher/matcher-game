using System;
using Matcher.Core.Factory;
using Matcher.Core.UI.Components;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Matcher.Core.UI
{
    public class WindowFactory : BaseFactory
    {
        private const string BlackoutPath = "Prefabs/UI/Blackout";
        private readonly WindowMapper _mapper;
        private readonly Transform _uiRoot;

        public WindowFactory(WindowMapper mapper, Transform uiRoot)
        {
            _mapper = mapper;
            _uiRoot = uiRoot;
        }

        public WindowBlackout CreateBlackout()
        {
            return CreateObject<WindowBlackout>(BlackoutPath, _uiRoot);
        }

        public TWindow CreateWindow<TWindow>(Type controllerType) where TWindow : Object, IBaseWindow
        {
            string path = _mapper.GetPath(controllerType);
            return CreateObject<TWindow>(path, _uiRoot);
        }
    }
}