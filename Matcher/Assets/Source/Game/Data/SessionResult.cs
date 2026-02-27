namespace Matcher.Game.Data
{
    public class SessionResult
    {
        public string PlayerName { get; }
        public int Score { get; }
        public int Moves { get; }
        public int PairsCount { get; }
        public long TimestampUnix { get; }

        public SessionResult(string playerName, int score, int moves, int pairsCount, long timestampUnix)
        {
            PlayerName = playerName;
            Score = score;
            Moves = moves;
            PairsCount = pairsCount;

            TimestampUnix = timestampUnix;
        }
    }
}