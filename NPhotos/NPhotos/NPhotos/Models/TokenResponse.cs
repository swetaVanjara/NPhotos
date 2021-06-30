using System;
using System.Collections.Generic;
using System.Text;

namespace NPhotos.Models
{
    public static class TokenResponse
    {
        public static string access_token { get; set; }
        public static string token_type { get; set; }
        public static string expires_in { get; set; }
        public static string refresh_token { get; set; }
        public static string id_token { get; set; }
    }
}
