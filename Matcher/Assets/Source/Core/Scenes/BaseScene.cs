using System;
using System.Threading.Tasks;
using Matcher.Core.Project;
using UnityEngine;

namespace Matcher.Core.Scenes
{
    public abstract class BaseScene : MonoBehaviour, IDisposable
    {
        protected virtual void Start()
        {
            ProjectContext.TransitionController.RegisterScene(this);
        }

        public abstract Task LoadAsync(object payload = null);

        public virtual void Dispose()
        {
        }

        protected virtual void OnDestroy()
        {
            Dispose();
        }
    }
}