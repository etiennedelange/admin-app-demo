using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace AdminApp.API.Helpers
{
    public static class TokenHelper
    {
        public static string Encode(string token) => WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        public static string Decode(string encodedToken) => Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encodedToken));
    }
}
