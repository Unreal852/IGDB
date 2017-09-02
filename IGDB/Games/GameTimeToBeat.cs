using IGDBLib.Attributes;
using System;

namespace IGDBLib.Games
{
    public class GameTimeToBeat
    {
        public GameTimeToBeat()
        {

        }

        [IGDBValue("hastly")]
        public Int64 Hastly { get; }
        [IGDBValue("normally")]
        public Int64 Normally { get; }
        [IGDBValue("campletely")]
        public Int64 Completely { get; }
    }
}
