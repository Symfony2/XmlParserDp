using Newtonsoft.Json;

namespace Infrastructure.Model
{
    public class JsonMatchModel
    {
        [JsonProperty("original_id")]
        public string OriginalId { get; set; }
        
        [JsonProperty("admitad_category_id")]
        public string AdmitadCategoryId { get; set; } 
        
    }
}