using TMPro;
using UnityEngine;

namespace Matcher.Game.Lobby.UI
{
    public class LeaderboardItemView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _rankText;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private TMP_Text _movesText;
        [SerializeField] private TMP_Text _timeText;

        public void Setup(int rank, string playerName, int score, int moves, string time)
        {
            _rankText.text = rank.ToString();
            _nameText.text = playerName;
            _scoreText.text = score.ToString();
            _movesText.text = moves.ToString();
            _timeText.text = time;
        }
    }
}