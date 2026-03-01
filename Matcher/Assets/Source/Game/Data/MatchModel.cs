using System;

namespace Matcher.Game.Data
{
    [Serializable]
    public class MatchModel
    {
        public string Symbol;
        public string MetaInfo;

        public MatchModel(string symbol, string metaInfo)
        {
            Symbol = symbol;
            MetaInfo = metaInfo;
        }
    }
}