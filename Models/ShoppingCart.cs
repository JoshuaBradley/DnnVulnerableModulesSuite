namespace JB.Security.SecurityVulnerabilities.Models
{
    using System.Collections.Generic;

    public class ShoppingCart
    {
        public string Name { get; set; }

        public int UserId { get; set; }

        public IEnumerable<int> ItemIds { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string CreditCardNumber { get; set; }

        public string CreditCardCCV { get; set; }

        public string CreditCardExpiration { get; set; }

        public decimal Total { get; set; }
    }
}