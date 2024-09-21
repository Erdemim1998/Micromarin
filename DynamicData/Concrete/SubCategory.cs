using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DynamicData.Concrete
{
    public class SubCategory
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Sub Category Name field is required.")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("ml1Name")]
        public string? Ml1Name { get; set; }

        [JsonPropertyName("ml2Name")]
        public string? Ml2Name { get; set; }

        [ForeignKey(nameof(CategoryId))]
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }

        [JsonPropertyName("category")]
        public Category Category { get; set; } = null!;
    }
}
