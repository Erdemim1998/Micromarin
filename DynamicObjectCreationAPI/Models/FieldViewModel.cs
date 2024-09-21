using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DynamicObjectCreationAPI.Models
{
    public class FieldViewModel
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Field Name field is required.")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Data Type field is required.")]
        [JsonPropertyName("dataType")]
        public string DataType { get; set; } = null!;

        [ForeignKey(nameof(TableId))]
        [JsonPropertyName("tableId")]
        public int TableId { get; set; }
    }
}
