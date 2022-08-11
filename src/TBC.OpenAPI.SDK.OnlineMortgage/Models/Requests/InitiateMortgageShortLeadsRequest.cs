using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TBC.OpenAPI.SDK.OnlineMortgage.Models.Requests
{
    public class InitiateMortgageShortLeadsRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalNo { get; set; }
        public DateTime BirthDate { get; set; }
        public string MobileNumber { get; set; }
        [Required]
        public string Url { get; set; }
        public string RealEstateCode { get; set; }
        public string PropertyPrice { get; set; }
        public string PropertyPriceCurrencyCode { get; set; }
        public int TermInMonths { get; set; }
        public decimal MonthlyIncome { get; set; }
        public bool IsDeveloperOffer { get; set; }
        [MaxLength(1200)]
        public string Comment { get; set; }
    }

}
