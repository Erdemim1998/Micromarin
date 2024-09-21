using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DynamicData.Concrete
{
    public class Brand
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Brand Name field is required.")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("ml1Name")]
        public string? Ml1Name { get; set; }

        [JsonPropertyName("ml2Name")]
        public string? Ml2Name { get; set; }
    }
}
