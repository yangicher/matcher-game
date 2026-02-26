using Matcher.Core.Scenes.Transition;
using UnityEngine;

namespace Matcher.Core.Project
{
    public class ProjectContext : MonoBehaviour
    {
        public static TransitionController TransitionController { get; private set; }

        [SerializeField] private TransitionView _transitionViewPrefab;

        private void Awake()
        {
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }

            TransitionController = new TransitionController(_transitionViewPrefab);
        }
    }
}