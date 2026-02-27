using System;

namespace Matcher.Core.Installer
{
    public interface IInstaller : IDisposable
    {
        void Install();
    }
}