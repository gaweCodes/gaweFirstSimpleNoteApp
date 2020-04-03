using System.Text.RegularExpressions;
using System.Windows.Input;
using gaweFirstSimpleNoteApp.Dtos;
using gaweFirstSimpleNoteApp.Models;
using gaweFirstSimpleNoteApp.Services;
using gaweFirstSimpleNoteApp.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp.ViewModels
{
    public class SignUpViewModel
    {
        public SignUpdto SignUpDto { get; } = new SignUpdto();
        public string PasswordConfirmation { get; set; }
        public ICommand SignUp { get; }
        public SignUpViewModel() => SignUp = new Command(async () =>
        {
            if (string.IsNullOrWhiteSpace(SignUpDto.Firstname))
            {
                await Application.Current.MainPage.DisplayAlert("Mandatory field",
                    "The firstname is a mandatory field.", "OK");
                return;
            }
            if (string.IsNullOrWhiteSpace(SignUpDto.Lastname))
            {
                await Application.Current.MainPage.DisplayAlert("Mandatory field",
                    "The lastname is a mandatory field.", "OK");
                return;
            }
            var regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!regex.Match(SignUpDto.Username).Success)
            {
                await Application.Current.MainPage.DisplayAlert("Mandatory field",
                    "The username is a mandatory field and must be a valid email.", "OK");
                return;
            }
            regex = new Regex(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,1024}$");
            if (!regex.Match(SignUpDto.Password).Success)
            {
                await Application.Current.MainPage.DisplayAlert("Mandatory field",
                    "The password must contain at least 1 upper case, 1 lower case and 1 number. The length is between 8 and 1024", "OK");
                return;
            }
            if (SignUpDto.Password != PasswordConfirmation)
            {
                await Application.Current.MainPage.DisplayAlert("Mandatory field",
                    "The password and the confirmed password are not equal.", "OK");
                return;
            }
            var data = JsonConvert.SerializeObject(SignUpDto);
            var accountService = new AccountService();
            var result = await accountService.SignUp(data);
            if (result.StartsWith("ERROR"))
            {
                await Application.Current.MainPage.DisplayAlert("Sign up failed",
                    "Sign up failed: " + result, "OK");
                return;
            }
            Application.Current.Properties["user"] = JsonConvert.DeserializeObject<User>(result);
            await Application.Current.MainPage.Navigation.PushAsync(new NotesPage());
        });
    }
}
