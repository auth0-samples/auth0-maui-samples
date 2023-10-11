﻿using Auth0.OidcClient;
using Auth0.OidcClient.MAUI;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Browser;

namespace Auth0MauiApp
{
    public partial class MainPage : ContentPage
    {
        Auth0Client client = new Auth0Client(new Auth0ClientOptions
        {
            Domain = "",
            ClientId = "",
            Scope = "openid profile email"
        });

        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var extraParameters = new Dictionary<string, string>();
            var audience = ""; // FILL WITH AUDIENCE AS NEEDED

            if (!string.IsNullOrEmpty(audience))
                extraParameters.Add("audience", audience);

            var result = await client.LoginAsync(extraParameters);

            DisplayResult(result);
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            BrowserResultType browserResult = await client.LogoutAsync();

            if (browserResult != BrowserResultType.Success)
            {
                ErrorLabel.Text = browserResult.ToString();
                return;
            }

            LogoutBtn.IsVisible = false;
            LoginBtn.IsVisible = true;

            HelloLabel.Text = $"Hello, World!";
        }

        private void DisplayResult(LoginResult loginResult)
        {
            if (loginResult.IsError)
            {
                ErrorLabel.Text = loginResult.Error;
                return;
            }

            LogoutBtn.IsVisible = true;
            LoginBtn.IsVisible = false;


            HelloLabel.Text = $"Hello, {loginResult.User.Identity.Name}";
        }
    }
}