using IGDBLib.Attributes;
using System;
using System.Collections.Generic;

namespace IGDBLib.Games
{
    public class GameEngine
    {
        public GameEngine()
        {

        }

        [IGDBValue("id")]
        public Int64 ID { get; internal set; }
        [IGDBValue("created_at")]
        public Int64 CreatedAt { get; internal set; }
        [IGDBValue("updated_at")]
        public Int64 UpdatedAt { get; internal set; }

        [IGDBValue("games")]
        public List<Int64> Games { get; internal set; } = new List<Int64>();
        [IGDBValue("companies")]
        public List<Int64> Companies { get; internal set; } = new List<Int64>();
        [IGDBValue("Platforms")]
        public List<Int64> Platforms { get; internal set; } = new List<Int64>();

        [IGDBValue("name")]
        public string Name { get; internal set; }
        [IGDBValue("slug")]
        public string Slug { get; internal set; }
        [IGDBValue("url")]
        public string Url { get; internal set; }

        [IGDBValue("logo")]
        public GameImage Logo { get; internal set; }

    }
}
