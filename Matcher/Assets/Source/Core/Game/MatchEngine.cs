using System;
using System.Threading.Tasks;
using Matcher.Core.Game.Element;

namespace Matcher.Core.Game
{
    public class MatchEngine
    {
        private readonly float _mismatchDelaySeconds;
        private readonly int _totalPairsToMatch;

        public event Action<int> OnMovesUpdated;
        public event Action OnAllPairsMatched;

        private IGameElement _firstElement;
        private IGameElement _secondElement;
        
        private bool _isInputLocked;
        private int _moves;
        private int _pairsMatched;

        public MatchEngine(int totalPairsToMatch, float mismatchDelaySeconds = 1.0f)
        {
            _totalPairsToMatch = totalPairsToMatch;
            _mismatchDelaySeconds = mismatchDelaySeconds;
            
            _moves = 0;
            _pairsMatched = 0;
            _isInputLocked = false;
        }

        public void Restart()
        {
            _moves = 0;
            _pairsMatched = 0;
            _isInputLocked = false;
            
            _firstElement = null;
            _secondElement = null;
            
            OnMovesUpdated?.Invoke(_moves);
        }

        public async void ProcessElementClick(IGameElement clickedElement)
        {
            if (_isInputLocked || clickedElement.CurrentState != GameElementState.Hidden)
            {
                return;
            }

            clickedElement.SetState(GameElementState.Revealed);

            if (_firstElement == null)
            {
                _firstElement = clickedElement;
            }
            else if (_secondElement == null && clickedElement != _firstElement)
            {
                _secondElement = clickedElement;
                
                _moves++;
                OnMovesUpdated?.Invoke(_moves);
                
                await CheckMatchAsync();
            }
        }

        private async Task CheckMatchAsync()
        {
            _isInputLocked = true;

            if (_firstElement.Id == _secondElement.Id)
            {
                _firstElement.SetState(GameElementState.Matched);
                _secondElement.SetState(GameElementState.Matched);
                
                _pairsMatched++;

                if (_pairsMatched >= _totalPairsToMatch)
                {
                    OnAllPairsMatched?.Invoke();
                }
            }
            else
            {
                await Task.Delay(TimeSpan.FromSeconds(_mismatchDelaySeconds));

                _firstElement.SetState(GameElementState.Hidden);
                _secondElement.SetState(GameElementState.Hidden);
            }

            _firstElement = null;
            _secondElement = null;
            _isInputLocked = false;
        }
    }
}