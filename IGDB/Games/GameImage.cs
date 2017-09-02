using IGDBLib.Attributes;

namespace IGDBLib.Games
{
    public class GameImage
    {
        public GameImage(string url, int width, int height)
        {
            URL = url;
            Width = width;
            Height = height;
        }

        public GameImage()
        {

        }

        [IGDBValue("url")]
        public string URL { get; set; }

        [IGDBValue("width")]
        public int Width { get; set; }
        [IGDBValue("height")]
        public int Height { get; set; }
    }
}
