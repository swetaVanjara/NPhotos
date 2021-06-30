
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPhotos.Helper;
using NPhotos.Models;
using NPhotos.Services;
using NPhotos.ViewModels;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NPhotos.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        Account account;
        AccountStore store;
        private readonly GoogleViewModel _googleViewModel = new GoogleViewModel();
        //private string clientID;
        //private string redirectURL;
        ObservableCollection<albums> Albums = new ObservableCollection<albums>();

        public HomePage()
        {
            InitializeComponent();
           
            string clientID = null;
            string redirectURL = null;

            switch(Device.RuntimePlatform)
            {
                case Device.iOS:
                    clientID = Constants.iOSClientId;
                    redirectURL = Constants.iOSRedirectUrl;
                    break;
                case Device.Android:
                    clientID = Constants.AndroidClientId;
                    redirectURL = Constants.AndroidRedirectUrl;
                    break;
            }

            store = AccountStore.Create();
            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();
            BindingContext = _googleViewModel;

            var authenticator = new OAuth2Authenticator(
                clientID,
                null,
                Constants.Scope,
                new Uri(Constants.AuthorizeUrl),
                new Uri(redirectURL),
                new Uri(Constants.AccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            ////Presenting the Sign - In User Interface
            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        async void SELECT_PICTURE(object o, EventArgs eventArg)
        {
            await DisplayAlert("Take a pic: ",  "button clicked", "OK");

            var apiTask = await new GoogleServices().GetAlbums();
            var response = JsonConvert.DeserializeObject<RootObject<albums>>(apiTask.ToString());
            for (int i = 0; i < response.albums.Count; i++)
            {
                var model = response.albums[i];
                Albums.Add(model);
            }
            //AlbumView.ItemsSource = Albums;
        }

        //Examining the OAuth Response

        public async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            User user = null;

            var authenticator = sender as OAuth2Authenticator;
            if(authenticator!=null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }
            if(e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                TokenResponse.access_token = e.Account.Properties["access_token"];
                TokenResponse.token_type = e.Account.Properties["token_type"];

                var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();

                if(response!=null)
                {
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<User>(userJson);
                    //User.Email = user["email"].ToString();
                    //_googleViewModel.Email = user.Email;
                }

                if(account!=null)
                {
                    store.Delete(account, Constants.AppName);
                }
                await store.SaveAsync(e.Account, Constants.AppName);
                await DisplayAlert("Email: ", user.Email + " " + user.Name, "OK");
                Name.Text = user.Name;
            }
        }

        private void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if(authenticator!=null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }
            DisplayAlert("Authentication error", e.Message, "OK");
        }


    }
}
