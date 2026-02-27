using System;

namespace Matcher.Core.UI
{
    public interface IWindowController
    {
        event Action<IWindowController> OnClosed;

        void InitializeView(WindowFactory factory);
        void Open();
        void Close();
    }
}