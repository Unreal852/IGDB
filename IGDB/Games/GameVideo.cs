using IGDBLib.Attributes;

namespace IGDBLib.Games
{
    public class GameVideo
    {
        public GameVideo(string name, string ytID)
        {
            Name = name;
            YouTubeID = ytID;
        }

        public GameVideo()
        {

        }

        [IGDBValue("name")]
        public string Name { get; set; }
        [IGDBValue("video_id")]
        public string YouTubeID { get; set; }
    }
}
