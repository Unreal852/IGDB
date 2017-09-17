using IGDBLib.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGDBLib.Games
{
    public class GameCompany
    {
        public GameCompany()
        {

        }

        [IGDBValue("id")]
        public Int64 ID { get; internal set; }
        [IGDBValue("created_at")]
        public Int64 CreatedAt { get; internal set; }
        [IGDBValue("updated_at")]
        public Int64 UpdatedAt { get; internal set; }
        [IGDBValue("start_date")]
        public Int64 StartDate { get; internal set; }
        [IGDBValue("changed_company_id")]
        public Int64 ChangedCompanyID { get; internal set; }
        [IGDBValue("change_date")]
        public Int64 ChangeDate { get; internal set; }

        [IGDBValue("published")]
        public List<Int64> Published { get; internal set; } = new List<Int64>();
        [IGDBValue("developed")]
        public List<Int64> Developed { get; internal set; } = new List<Int64>();

        [IGDBValue("country")]
        public int Country { get; internal set; }
        [IGDBValue("start_date_category")]
        public int StartDateCategory { get; internal set; }
        [IGDBValue("change_date_category")]
        public int ChangeDateCategory { get; internal set; }

        [IGDBValue("name")]
        public string Name { get; internal set; }
        [IGDBValue("slug")]
        public string Slug { get; internal set; }
        [IGDBValue("url")]
        public string Url { get; internal set; }
        [IGDBValue("description")]
        public string Description { get; internal set; }
        [IGDBValue("website")]
        public string Website { get; internal set; }
        [IGDBValue("twitter")]
        public string Twitter { get; internal set; }

        [IGDBValue("logo")]
        public GameImage Logo { get; internal set; }
    }
}
