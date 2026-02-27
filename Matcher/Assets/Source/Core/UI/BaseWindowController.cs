using System;

namespace Matcher.Core.UI
{
    public abstract class BaseWindowController<TModel, TWindow> : IWindowController
        where TModel : IWindowModel
        where TWindow : BaseWindow<TModel>
    {
        protected TModel Model { get; }
        protected TWindow WindowView { get; private set; }
        
        public event Action<IWindowController> OnClosed;

        protected BaseWindowController(TModel model)
        {
            Model = model;
        }

        public void InitializeView(WindowFactory factory)
        {
            WindowView = factory.CreateWindow<TWindow>(this.GetType());
            SetupView();
            SubscribeToEvents(); 
        }

        protected virtual void SetupView() { }
        protected virtual void SubscribeToEvents() { }
        protected virtual void UnsubscribeFromEvents() { }

        public async void Open()
        {
            await WindowView.PlayShowAnimationAsync();
            OnOpened();
        }

        protected virtual void OnOpened() { }

        public async void Close()
        {
            UnsubscribeFromEvents();
            await WindowView.PlayHideAnimationAsync();
            
            WindowView.Dispose();
            WindowView = null;
            
            OnClosed?.Invoke(this);
        }
    }
}