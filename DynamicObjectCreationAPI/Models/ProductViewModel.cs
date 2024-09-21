using DynamicData.Concrete;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DynamicObjectCreationAPI.Models
{
    public class ProductViewModel
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name field is required.")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("ml1Name")]
        public string? Ml1Name { get; set; }

        [JsonPropertyName("ml2Name")]
        public string? Ml2Name { get; set; }

        [JsonPropertyName("price")]
        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }

        [ForeignKey(nameof(SubCategoryId))]
        [JsonPropertyName("subCategoryId")]
        public int SubCategoryId { get; set; }

        [ForeignKey(nameof(BrandId))]
        [JsonPropertyName("brandId")]
        public int BrandId { get; set; }
    }
}
