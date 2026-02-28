using System.Collections.Generic;
using Matcher.Core.UI.Components;
using UnityEngine;

namespace Matcher.Core.UI
{
    public class WindowManager
    {
        private WindowBlackout _blackout;
        
        private readonly WindowFactory _factory;
        private readonly WindowMapper _mapper;
        private readonly Queue<IWindowController> _windowQueue = new Queue<IWindowController>();
        
        private IWindowController _currentActiveWindow;

        public WindowManager(WindowFactory factory, WindowMapper mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }
        
        public void RegisterWindow<TController>(string path) where TController : IWindowController
        {
            _mapper.Register<TController>(path);
        }

        public void UnregisterWindow<TController>() where TController : IWindowController
        {
            _mapper.Unregister<TController>();
        }

        public void Open(IWindowController controller)
        {
            _windowQueue.Enqueue(controller);
            if (_currentActiveWindow == null)
            {
                ProcessNextWindow();
            }
        }

        private void ProcessNextWindow()
        {
            if (_windowQueue.Count > 0)
            {
                _currentActiveWindow = _windowQueue.Dequeue();
                _currentActiveWindow.OnClosed += HandleWindowClosed;
                
                _currentActiveWindow.InitializeView(_factory);
                _currentActiveWindow.Open();

                if (_blackout == null)
                {
                    _blackout = _factory.CreateBlackout();
                }
                
                _blackout.transform.SetAsFirstSibling();
                _blackout.FadeInAsync();
            }
            else
            {
                _currentActiveWindow = null;
                _blackout?.FadeOutAsync();
            }
        }

        private void HandleWindowClosed(IWindowController closedWindow)
        {
            closedWindow.OnClosed -= HandleWindowClosed;
            ProcessNextWindow();
        }
    }
}