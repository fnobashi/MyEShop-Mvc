
namespace MyEShop.Constants
{
    public class Regex
    {
        public const string PostalCode = @"\b(?!(\d)\1{3})[13-9]{4}[1346-9][013-9]{5}\b";
        public const string PhoneNumber = @"^(?:0|98|\+98|\+980|0098|098|00980)?(9\d{9})$";
    }
}
