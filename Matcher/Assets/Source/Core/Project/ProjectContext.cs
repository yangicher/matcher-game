using Matcher.Core.Scenes.Transition;
using Matcher.Core.Services;
using Matcher.Core.UI;
using UnityEngine;

namespace Matcher.Core.Project
{
    public class ProjectContext : MonoBehaviour
    {
        public static WindowManager WindowManager { get; private set; }
        public static ISessionService SessionService { get; private set; }
        public static TransitionController TransitionController { get; private set; }

        [SerializeField] private RectTransform _windowsLayer;
        [SerializeField] private RectTransform _transitionLayer;
        
        [SerializeField] private TransitionView _transitionViewPrefab;

        private void Awake()
        {
            if (Application.isPlaying)
            {
                DontDestroyOnLoad(gameObject);
            }
            
            InitializeWindows();
            InitializeTransitions();
        }

        public static void RegisterDataService(ISessionService sessionService)
        {
            SessionService = sessionService;
        }
        
        private void InitializeWindows()
        {
            WindowMapper mapper = new WindowMapper();
            WindowFactory factory = new WindowFactory(mapper, _windowsLayer); 
            WindowManager = new WindowManager(factory, mapper);
        }
    
        private void InitializeTransitions()
        {
            TransitionController = new TransitionController(_transitionViewPrefab, _transitionLayer);
        }
    }
}