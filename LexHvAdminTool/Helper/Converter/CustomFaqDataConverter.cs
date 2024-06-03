using LexHvAdminTool.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexHvAdminTool.Helper.Converter
{
    public class CustomFaqDataConverter : JsonConverter<FaqData>
    {
        public override FaqData ReadJson(JsonReader reader, Type objectType, FaqData existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.Load(reader);

            var faqData = new FaqData();

            if (token.Type == JTokenType.Array)
            {
                // Old format: simple array of FaqEntry
                faqData.Entries = token.ToObject<List<FaqEntry>>();
            }
            else if (token.Type == JTokenType.Object)
            {
                var jsonObject = (JObject)token;

                // New format: object containing Entries array and optional metadata
                faqData.Entries = jsonObject["Entries"]?.ToObject<List<FaqEntry>>() ?? new List<FaqEntry>();

                if (jsonObject["LastModified"] != null)
                {
                    faqData.LastModified = jsonObject["LastModified"].ToObject<DateTime>();
                }

                if (jsonObject["ModifiedBy"] != null)
                {
                    faqData.ModifiedBy = jsonObject["ModifiedBy"].ToString();
                }
            }

            return faqData;
        }

        public override void WriteJson(JsonWriter writer, FaqData value, JsonSerializer serializer)
        {
            var jsonObject = new JObject
            {
                { "LastModified", JToken.FromObject(value.LastModified) },
                { "ModifiedBy", JToken.FromObject(value.ModifiedBy) },
                { "Entries", JToken.FromObject(value.Entries) }
            };
            jsonObject.WriteTo(writer);
        }
    }
}
