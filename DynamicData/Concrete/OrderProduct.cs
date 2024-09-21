using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DynamicData.Concrete
{
    public class OrderProduct
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [ForeignKey(nameof(ProductId))]
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("product")]
        public Product Product { get; set; } = null!;

        [ForeignKey(nameof(OrderId))]
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("order")]
        public Order Order { get; set; } = null!;
    }
}
