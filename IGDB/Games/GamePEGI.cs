using IGDBLib.Attributes;

namespace IGDBLib.Games
{
    public class GamePEGI
    {
        public GamePEGI(int pegi, string synopsis)
        {
            Rating = pegi;
            Synopsis = synopsis;
        }

        public GamePEGI()
        {

        }

        [IGDBValue("rating")]
        public int Rating { get; set; }

        [IGDBValue("synopsis")]
        public string Synopsis { get; set; }
    }
}
