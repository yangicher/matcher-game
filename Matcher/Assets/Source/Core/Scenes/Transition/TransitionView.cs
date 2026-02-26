using UnityEngine;
using System.Threading.Tasks;

namespace Matcher.Core.Scenes.Transition
{
    [RequireComponent(typeof(CanvasGroup))]
    public class TransitionView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 0.5f;

        private void Awake()
        {
            if (_canvasGroup == null)
            {
                _canvasGroup = GetComponent<CanvasGroup>();
            }
        
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
        }

        public async Task FadeInAsync()
        {
            _canvasGroup.blocksRaycasts = true;
            float time = 0;
            while (time < _fadeDuration)
            {
                time += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(0, 1, time / _fadeDuration);
                await Task.Yield();
            }
            _canvasGroup.alpha = 1;
        }

        public async Task FadeOutAsync()
        {
            float time = 0;
            while (time < _fadeDuration)
            {
                time += Time.deltaTime;
                _canvasGroup.alpha = Mathf.Lerp(1, 0, time / _fadeDuration);
                await Task.Yield();
            }
            _canvasGroup.alpha = 0;
            _canvasGroup.blocksRaycasts = false;
        }
    }
}