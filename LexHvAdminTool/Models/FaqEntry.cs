using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexHvAdminTool.Models
{
    public class FaqEntry
    {
        public string FaqId { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public string Information { get; set; }
        public int OrderBy { get; set; }
        public DateTime DisplayFrom { get; set; }
        public DateTime DisplayUntil { get; set; }
        public string ImageURL { get; set; }
        public bool CreateCalenderEvent { get; set; }
        public DateTime CalenderEventFrom { get; set; }
        public DateTime CalenderEventTo { get; set; }
        public List<int> Flavours { get; set; }
    }

}
