using System.Threading.Tasks;
using Matcher.Core.Project;
using Matcher.Core.Scenes;

namespace Matcher.Scenes.Splash
{
    public class SplashScene : BaseScene
    {
        public override Task LoadAsync(object payload = null)
        {
            return Task.CompletedTask;
        }

        protected override async void Start()
        {
            base.Start();
            await Task.Delay(1000);
            
            await ProjectContext.TransitionController.LoadSceneAsync(SceneNames.Loading);
        }
    }
}