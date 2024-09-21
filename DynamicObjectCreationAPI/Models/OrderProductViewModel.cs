using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DynamicObjectCreationAPI.Models
{
    public class OrderProductViewModel
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [ForeignKey(nameof(ProductId))]
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [ForeignKey(nameof(OrderId))]
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }
    }
}
