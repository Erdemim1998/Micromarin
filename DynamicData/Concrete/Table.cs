using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DynamicData.Concrete
{
    public class Table
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Table Name field is required.")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;
    }
}
