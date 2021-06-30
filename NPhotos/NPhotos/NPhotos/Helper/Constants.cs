using System;
using System.Collections.Generic;
using System.Text;

namespace NPhotos.Helper
{
    public static class Constants
    {
        public static string AppName = "NPhotos";
        public static string iOSClientId = "33147413996-oev6i9n6qpses2tecmmch2srcag92bug.apps.googleusercontent.com";
        public static string AndroidClientId = "33147413996-1v68mj9nlssj7seoqdqf40v81rbffare.apps.googleusercontent.com";
        public static string AppID = "33147413996-0up4drr7l8cenhn1rbg1mae9i2k6mnd7.apps.googleusercontent.com";

        // These values do not need changing
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string iOSRedirectUrl = "com.googleusercontent.apps.33147413996-oev6i9n6qpses2tecmmch2srcag92bug:/oauth2redirect";
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.33147413996-1v68mj9nlssj7seoqdqf40v81rbffare:/oauth2redirect";
        public static string AppRedirectUrl = "com.googleusercontent.apps.33147413996-0up4drr7l8cenhn1rbg1mae9i2k6mnd7:/oauth2redirect";
    }
}
