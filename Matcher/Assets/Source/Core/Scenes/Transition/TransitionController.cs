using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace Matcher.Core.Scenes.Transition
{
    public class TransitionController
    {
        private readonly TransitionView _viewPrefab;
        private BaseScene _currentSceneLogic;
        private TaskCompletionSource<BaseScene> _sceneReady;

        public TransitionController(TransitionView viewPrefab)
        {
            _viewPrefab = viewPrefab;
        }

        public void RegisterScene(BaseScene scene)
        {
            _currentSceneLogic = scene;
            _sceneReady?.TrySetResult(scene);
        }

        public async Task LoadSceneAsync(string sceneName)
        {
            TransitionView activeView = Object.Instantiate(_viewPrefab);
            Object.DontDestroyOnLoad(activeView.gameObject);

            await activeView.FadeInAsync();

            _currentSceneLogic?.Dispose();
            _sceneReady = new TaskCompletionSource<BaseScene>();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (asyncLoad is { isDone: false })
            {
                await Task.Yield();
            }

            BaseScene newScene = await _sceneReady.Task;
        
            await newScene.LoadAsync();

            await activeView.FadeOutAsync();
            
            Object.Destroy(activeView.gameObject);
        }
    }
}