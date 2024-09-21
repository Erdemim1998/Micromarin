using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DynamicData.Concrete
{
    public class Order
    {
        [Key]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("orderDate")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Order Phone Number field is required.")]
        [JsonPropertyName("phoneNumber")]
        [Phone(ErrorMessage = "Please spesicify a valid phone number.")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Order Email field is required.")]
        [JsonPropertyName("email")]
        [EmailAddress(ErrorMessage = "Please spesicify a valid email.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Order Address field is required.")]
        [JsonPropertyName("address")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Order User Name field is required.")]
        [JsonPropertyName("userName")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "Order Cart Number field is required.")]
        [JsonPropertyName("cartNumber")]
        [CreditCard(ErrorMessage = "Please spesicify a valid cart number.")]
        public string CartNumber { get; set; } = null!;

        [Required(ErrorMessage = "Order Expiration Month field is required.")]
        [JsonPropertyName("expirationMonth")]
        public string ExpirationMonth { get; set; } = null!;

        [Required(ErrorMessage = "Order Expiration Year field is required.")]
        [JsonPropertyName("expirationYear")]
        public string ExpirationYear { get; set; } = null!;

        [Required(ErrorMessage = "Order Cvc field is required.")]
        [JsonPropertyName("cvc")]
        public string Cvc { get; set; } = null!;
    }
}
