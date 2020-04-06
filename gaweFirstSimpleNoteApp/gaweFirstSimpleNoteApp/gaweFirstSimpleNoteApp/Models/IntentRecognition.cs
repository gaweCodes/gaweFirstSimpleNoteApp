using System.Collections.Generic;
using Newtonsoft.Json;

namespace gaweFirstSimpleNoteApp.Models
{
    public class TopScoringIntent
    {
        public string Intent { get; set; }
        public double Score { get; set; }
    }

    public class Entity
    {
        [JsonProperty(nameof(Entity))]
        public string Value { get; set; }
        public string Type { get; set; }
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public double Score { get; set; }
    }

    public class IntentRecognition
    {
        public string Query { get; set; }
        public TopScoringIntent TopScoringIntent { get; set; }
        public List<Entity> Entities { get; set; }
    }
}
