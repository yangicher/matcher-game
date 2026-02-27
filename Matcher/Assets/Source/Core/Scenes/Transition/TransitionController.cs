using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

namespace Matcher.Core.Scenes.Transition
{
    public class TransitionController
    {
        private readonly TransitionView _viewPrefab;
        private readonly Transform _transitionContainer;
        private BaseScene _currentSceneLogic;
        private TaskCompletionSource<BaseScene> _sceneReady;

        public TransitionController(TransitionView viewPrefab, Transform transitionContainer)
        {
            _viewPrefab = viewPrefab;
            _transitionContainer = transitionContainer;
        }

        public void RegisterScene(BaseScene scene)
        {
            _currentSceneLogic = scene;
            _sceneReady?.TrySetResult(scene);
        }

        public async Task LoadSceneAsync(string sceneName, object payload = null)
        {
            TransitionView activeView = Object.Instantiate(_viewPrefab, _transitionContainer);

            await activeView.FadeInAsync();

            _currentSceneLogic?.Dispose();
            _sceneReady = new TaskCompletionSource<BaseScene>();

            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
            while (asyncLoad is { isDone: false })
            {
                await Task.Yield();
            }

            BaseScene newScene = await _sceneReady.Task;
        
            await newScene.LoadAsync(payload);

            await activeView.FadeOutAsync();
            
            Object.Destroy(activeView.gameObject);
        }
    }
}