using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Matcher.Core.UI.Components
{
    [RequireComponent(typeof(Image))]
    public class WindowBlackout : MonoBehaviour
    {
        [SerializeField] private float _fadeDuration = 0.2f;
        [SerializeField] private float _targetAlpha = 0.7f;

        [SerializeField] private Image _backgroundImage;
        private bool _isAnimating;

        private void Awake()
        {
            SetAlpha(0f);
            gameObject.SetActive(false);
        }

        public async Task FadeInAsync()
        {
            gameObject.SetActive(true);
            _isAnimating = true;
            
            float elapsedTime = 0f;
            float startAlpha = _backgroundImage.color.a;

            while (elapsedTime < _fadeDuration && _isAnimating)
            {
                elapsedTime += Time.deltaTime;
                SetAlpha(Mathf.Lerp(startAlpha, _targetAlpha, elapsedTime / _fadeDuration));
                await Task.Yield();
            }

            if (_isAnimating) SetAlpha(_targetAlpha);
        }

        public async Task FadeOutAsync()
        {
            _isAnimating = true;
            
            float elapsedTime = 0f;
            float startAlpha = _backgroundImage.color.a;

            while (elapsedTime < _fadeDuration && _isAnimating)
            {
                elapsedTime += Time.deltaTime;
                SetAlpha(Mathf.Lerp(startAlpha, 0f, elapsedTime / _fadeDuration));
                await Task.Yield();
            }

            if (_isAnimating)
            {
                SetAlpha(0f);
                gameObject.SetActive(false);
            }
        }

        private void SetAlpha(float alpha)
        {
            Color c = _backgroundImage.color;
            c.a = alpha;
            _backgroundImage.color = c;
        }
    }
}