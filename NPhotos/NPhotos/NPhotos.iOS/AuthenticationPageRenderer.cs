
using System;
using System.Diagnostics;
using NPhotos.Helper;
using UIKit;
using Xamarin.Auth;
using Xamarin.Forms.Platform.iOS;

namespace NPhotos.iOS
{
    public class AuthenticationPageRenderer : PageRenderer
    {
        public AuthenticationPageRenderer()
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            var auth = new OAuth2Authenticator(
                Constants.iOSClientId,
                Constants.Scope,
                new Uri(Constants.AuthorizeUrl),
                new Uri(Constants.iOSRedirectUrl),
                null,
                true
            );

            auth.Completed+=Auth_Completed;
            UIViewController authView = auth.GetUI() as UIViewController;
            //PresentViewController(auth.GetUI(), true, null);
            PresentViewController((SafariServices.SFSafariViewController)authView,true,null);
        }

        public void Auth_Completed(object sender, AuthenticatorCompletedEventArgs e)
        {
            this.DismissViewController(true, null);
            Debug.WriteLine("AUTH Completed!");
            if (e.IsAuthenticated)
            {
                
            }
        }

    }
}
