using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Matcher.Core.UI
{
    public abstract class BaseWindow<TModel> : MonoBehaviour, IBaseWindow, IDisposable where TModel : IWindowModel
    {
        public abstract Task PlayShowAnimationAsync();
        public abstract Task PlayHideAnimationAsync();
        
        public virtual void Dispose() 
        {
            Destroy(gameObject);
        }
    }
}