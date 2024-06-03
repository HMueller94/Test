using LexHvAdminTool.Helper.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexHvAdminTool.Models
{
    [JsonConverter(typeof(CustomFaqDataConverter))]
    public class FaqData
    {
        public DateTime? LastModified { get; set; }
        public string ModifiedBy { get; set; }
        public List<FaqEntry> Entries { get; set; }
    }
}
