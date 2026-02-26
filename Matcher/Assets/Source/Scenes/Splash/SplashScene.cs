using System.Threading.Tasks;
using Matcher.Core.Project;
using Matcher.Core.Scenes;

namespace Matcher.Scenes.Splash
{
    public class SplashScene : BaseScene
    {
        private async void Start()
        {
            await Task.Delay(3000);
            await LoadAsync();
        }
        public override async Task LoadAsync(object payload = null)
        {
            await ProjectContext.TransitionController.LoadSceneAsync(SceneNames.Loading);
        }
    }
}