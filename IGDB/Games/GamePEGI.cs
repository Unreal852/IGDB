using IGDBLib.Attributes;

namespace IGDBLib.Games
{
    public class GamePEGI
    {
        private int m_rating;

        public GamePEGI()
        {

        }

        public GamePEGI(int pegi, string synopsis)
        {
            Rating = pegi;
            Synopsis = synopsis;
        }

        [IGDBValue("rating")]
        public int Rating
        {
            get { return m_rating; }
            set
            {
                switch (value)
                {
                    case 1:
                        m_rating = 3;
                        break;
                    case 2:
                        m_rating = 7;
                        break;
                    case 3:
                        m_rating = 12;
                        break;
                    case 4:
                        m_rating = 16;
                        break;
                    case 5:
                        m_rating = 18;
                        break;
                    default:
                        m_rating = 0;
                        break;
                }
            }
        }

        [IGDBValue("synopsis")]
        public string Synopsis { get; set; }
    }
}
