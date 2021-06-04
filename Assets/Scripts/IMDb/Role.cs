using Newtonsoft.Json; 
namespace IMDb{ 

    public class Role
    {
        [JsonProperty("character")]
        public string Character { get; set; }

        [JsonProperty("characterId")]
        public string CharacterId { get; set; }
    }

}