using System.Threading.Tasks;
using Matcher.Core.Project;
using Matcher.Core.Scenes;

namespace Matcher.Scenes.Loading
{
    public class LoadingScene : BaseScene
    {
        public override async Task LoadAsync()
        {
            await Task.Delay(2000);
            await ProjectContext.TransitionController.LoadSceneAsync(SceneNames.Main);
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
}