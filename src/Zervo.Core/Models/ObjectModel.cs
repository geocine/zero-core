using Newtonsoft.Json;

namespace Zervo.Core.Models
{
    public class ObjectModel
    {
        [JsonProperty(Order = 1)]
        public int Id { get; set; }
    }
}
