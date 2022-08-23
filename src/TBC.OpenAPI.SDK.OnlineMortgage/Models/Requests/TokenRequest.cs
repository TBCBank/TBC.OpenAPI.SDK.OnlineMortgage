using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TBC.OpenAPI.SDK.OnlineMortgage.Models.Requests
{
    public static class TokenRequest
    {
        public static string Grant_Type { get; set; } = "client_credentials";
        public static string Scope { get; set; } = "online_mortgages";
    }
}
