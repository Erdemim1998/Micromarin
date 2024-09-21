using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DynamicObjectCreationAPI.Models
{
    public class SubCategoryViewModel
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
    }
}
