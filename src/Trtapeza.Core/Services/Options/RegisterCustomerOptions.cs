using Trapeza.Core.Model.Types;

namespace Trapeza.Core.Services.Options
{
    public class RegisterCustomerOptions
    {
        public string Name { get; set; }
        public string SurName { get; set; }
        public string VatNumber { get; set; }
        public CustomerType CustType { get; set; }
        public string Address { get; set; }
    }
}
