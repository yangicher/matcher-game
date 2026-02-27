using System.Threading.Tasks;
using Matcher.Core.Project;
using Matcher.Core.Scenes;
using Matcher.Game.Services.Database;
using Matcher.Game.Services.Firebase;
using Matcher.Game.Services.Session;

namespace Matcher.Scenes.Loading
{
    public class LoadingScene : BaseScene
    {
        public override Task LoadAsync(object payload = null)
        {
            return Task.CompletedTask;
        }

        protected override async void Start()
        {
            base.Start();
            await InitializeServicesAsync();
        }

        private async Task InitializeServicesAsync()
        {
            await InitializeBackendAsync();
            await ProjectContext.TransitionController.LoadSceneAsync(SceneNames.Main);
        }
        
        private async Task InitializeBackendAsync()
        {
            var dependencyStatus = await Firebase.FirebaseApp.CheckAndFixDependenciesAsync();
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                IDatabaseService firestoreDb = new FirestoreService();
                ISessionService sessionRepo = new MatcherSessionService(firestoreDb);
        
                ProjectContext.RegisterDataService(sessionRepo);
            }
            else
            {
                UnityEngine.Debug.LogError($"InitializeBackendAsync {dependencyStatus}");
            }
        }
    }
}