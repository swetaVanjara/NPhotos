using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPhotos.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NPhotos.Services
{
    public class GoogleServices
    {
        /// <summary>
        /// Create a new app and get new creadentials: 
        /// https://console.developers.google.com/apis/
        /// </summary>
        //public static readonly string ClientId = "33147413996-1v68mj9nlssj7seoqdqf40v81rbffare.apps.googleusercontent.com";
        //public static readonly string ClientSecret = "3Q2Lk-ErZlceEJmAt1oTfb2M";
        //public static readonly string RedirectUri = "https://www.youtube.com/watch?v=OX-h7MtkeOI:/oauth2redirect";

        //public async Task<string> GetAccessTokenAsync(string code)
        //{
        //    var requestUrl =
        //        "https://www.googleapis.com/oauth2/v4/token"
        //        + "?code=" + code
        //        + "&client_id=" + ClientId
        //        + "&client_secret=" + ClientSecret
        //        + "&redirect_uri=" + RedirectUri
        //        + "&grant_type=authorization_code";

        //    var httpClient = new HttpClient();

        //    var response = await httpClient.PostAsync(requestUrl, null);

        //    var json = await response.Content.ReadAsStringAsync();

        //    var accessToken = JsonConvert.DeserializeObject<JObject>(json).Value<string>("access_token");

        //    return accessToken;
        //}

        public async Task<JObject> GetAlbums()
        {
            var requestUrl = "https://photoslibrary.googleapis.com/v1/albums"
                        + "?access_token=" + TokenResponse.access_token;

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenResponse.access_token);
            var albumJson = await httpClient.GetStringAsync(requestUrl);
            return JsonConvert.DeserializeObject<JObject>(albumJson);
        }

        public async Task<GoogleProfile> GetGoogleUserProfileAsync(string accessToken)
        {
            
            var requestUrl = "https://www.googleapis.com/plus/v1/people/me"
                             + "?access_token=" + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var googleProfile = JsonConvert.DeserializeObject<GoogleProfile>(userJson);

            return googleProfile;
        }
    }
}
