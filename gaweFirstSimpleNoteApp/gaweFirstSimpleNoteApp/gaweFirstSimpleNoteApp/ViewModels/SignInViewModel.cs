using System.Windows.Input;
using gaweFirstSimpleNoteApp.Dtos;
using gaweFirstSimpleNoteApp.Models;
using gaweFirstSimpleNoteApp.Services;
using gaweFirstSimpleNoteApp.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp.ViewModels
{
    public class SignInViewModel
    {
        public SignInDto SignInDto { get; } = new SignInDto();
        public ICommand SignIn { get; }
        public ICommand SignUp { get; }
        public SignInViewModel()
        {
            SignIn = new Command(async () =>
            {
                if (string.IsNullOrWhiteSpace(SignInDto.Username))
                {
                    await Application.Current.MainPage.DisplayAlert("Mandatory field",
                        "The username is a mandatory field.", "OK");
                    return;
                }
                if (string.IsNullOrWhiteSpace(SignInDto.Password))
                {
                    await Application.Current.MainPage.DisplayAlert("Mandatory field",
                        "The password is a mandatory field.", "OK");
                    return;
                }
                var data = JsonConvert.SerializeObject(SignInDto);
                var accountService = new AccountService();
                var result = await accountService.SignIn(data);
                if (result.StartsWith("ERROR"))
                {
                    await Application.Current.MainPage.DisplayAlert("Sign in failed",
                        "Sign in failed: " + result, "OK");
                    return;
                }
                Application.Current.Properties["user"] = JsonConvert.DeserializeObject<User>(result);
                await Application.Current.MainPage.Navigation.PushAsync(new NotesPage());
            });
            SignUp = new Command(async () => await Application.Current.MainPage.Navigation.PushAsync(new SignUpPage()));
        }
    }
}
