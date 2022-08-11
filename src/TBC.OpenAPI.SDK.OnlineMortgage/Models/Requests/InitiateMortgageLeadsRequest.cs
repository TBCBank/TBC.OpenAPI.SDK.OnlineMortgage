using System;
using System.Collections.Generic;
using System.Text;

namespace TBC.OpenAPI.SDK.OnlineMortgage.Models.Requests
{
    public class InitiateMortgageLeadsRequest
    {
        public string Url { get; set; }
        public string RealEstateCode { get; set; }
        public string CompanyCode { get; set; }
        public string OtherCompanyName { get; set; }
        public string PropertyPrice { get; set; }
        public string PropertyPriceCurrencyCode { get; set; }
        public float DownPaymentAmount { get; set; }
        public string DownPaymentAmountCurrencyCode { get; set; }
        public int TermInMonths { get; set; }
    }

}
