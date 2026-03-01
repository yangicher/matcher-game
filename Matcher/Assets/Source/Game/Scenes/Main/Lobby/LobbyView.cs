using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace Matcher.Scenes.Main.Lobby
{
    public class LobbyView : MonoBehaviour, IDisposable
    {
        [SerializeField] private TMP_InputField _nameInputField;
        [SerializeField] private Button _easyButton;
        [SerializeField] private Button _hardButton;
        [SerializeField] private Button _leaderboardButton;

        public Action<string> OnPlayEasyClicked;
        public Action<string> OnPlayHardClicked;
        public Action OnLeaderboardClicked;

        private void Awake()
        {
            _easyButton.onClick.AddListener(() => OnPlayEasyClicked?.Invoke(_nameInputField.text));
            _hardButton.onClick.AddListener(() => OnPlayHardClicked?.Invoke(_nameInputField.text));
            _leaderboardButton.onClick.AddListener(() => OnLeaderboardClicked?.Invoke());
        }

        public void SetPlayerName(string name)
        {
            _nameInputField.text = name;
        }

        public void SetInteractable(bool state)
        {
            _easyButton.interactable = state;
            _hardButton.interactable = state;
            _leaderboardButton.interactable = state;
            _nameInputField.interactable = state;
        }

        public void Dispose()
        {
            _easyButton.onClick.RemoveAllListeners();
            _hardButton.onClick.RemoveAllListeners();
            _leaderboardButton.onClick.RemoveAllListeners();
        }
    }
}