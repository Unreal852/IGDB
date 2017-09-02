using IGDBLib.Attributes;
using System;
using System.Collections.Generic;

namespace IGDBLib.Games
{
    public class Game
    {
        public Game()
        {

        }

        [IGDBValue("name")]
        public string Name { get; internal set; }
        [IGDBValue("summary")]
        public string Summary { get; internal set; }
        [IGDBValue("storyline")]
        public string StoryLine { get; internal set; }
        [IGDBValue("slug")]
        public string Slug { get; internal set; }
        [IGDBValue("url")]
        public string Url { get; internal set; }

        [IGDBValue("id")]
        public Int64 ID { get; internal set; }
        [IGDBValue("created_at")]
        public Int64 CreatedAt { get; internal set; }
        [IGDBValue("updated_at")]
        public Int64 UpdatedAt { get; internal set; }
        [IGDBValue("collection")]
        public Int64 Collection { get; internal set; }
        [IGDBValue("franchise")]
        public Int64 Franchise { get; internal set; }

        [IGDBValue("hypes")]
        public int Hypes { get; internal set; }
        [IGDBValue("rating_count")]
        public int RatingCount { get; internal set; }
        [IGDBValue("aggregated_rating_count")]
        public int AggregatedRatingCount { get; internal set; }
        [IGDBValue("total_rating_count")]
        public int TotalRatingCount { get; internal set; }

        [IGDBValue("popularity")]
        public double Popularity { get; internal set; }
        [IGDBValue("rating")]
        public double Rating { get; internal set; }
        [IGDBValue("aggregated_rating")]
        public double AggregatedRating { get; internal set; }
        [IGDBValue("weighted_rating")]
        public double WeightedRating { get; internal set; }

        [IGDBValue("status")]
        public GameStatus Status { get; internal set; }

        [IGDBValue("pegi")]
        public GamePEGI PEGI { get; internal set; }

        [IGDBValue("cover")]
        public GameImage Cover { get; internal set; }

        public GameEngine Engine { get; internal set; }

        [IGDBValue("platforms")]
        public List<GamePlatform> Platforms { get; internal set; } = new List<GamePlatform>();
        [IGDBValue("genres")]
        public List<GameGenre> Genres { get; internal set; } = new List<GameGenre>();
        [IGDBValue("game_modes")]
        public List<GameMode> Modes { get; internal set; } = new List<GameMode>();
        [IGDBValue("player_perspectives")]
        public List<GamePerspective> Perspectives { get; internal set; } = new List<GamePerspective>();

        [IGDBValue("themes")]
        public List<GameTheme> Themes { get; internal set; } = new List<GameTheme>();

        [IGDBValue("developers")]
        public List<GameCompany> Developers { get; internal set; } = new List<GameCompany>();
        [IGDBValue("publishers")]
        public List<GameCompany> Publishers { get; internal set; } = new List<GameCompany>();
        [IGDBValue("screenshots")]
        public List<GameImage> ScreenShots { get; internal set; } = new List<GameImage>();
        [IGDBValue("videos")]
        public List<GameVideo> Videos { get; internal set; } = new List<GameVideo>();
    }
}
