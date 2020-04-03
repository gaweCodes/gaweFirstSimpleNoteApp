using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace gaweFirstSimpleNoteApp.Services
{
    public class AccountService
    {
        private readonly HttpClient _httpClient = new HttpClient();
        private readonly string _baseUrl = "https://gawefirstsimplenoteapp.azurewebsites.net/api/account/";
        public async Task<string> SignUp(string data)
        {
            var response = await _httpClient.PostAsync(_baseUrl + "signup",
                new StringContent(data, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode) return "ERROR: " + response.ReasonPhrase;
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<string> SignIn(string data)
        {
            var response = await _httpClient.PostAsync(_baseUrl + "signin",
                new StringContent(data, Encoding.UTF8, "application/json"));
            if (!response.IsSuccessStatusCode) return "ERROR: " + response.ReasonPhrase;
            return await response.Content.ReadAsStringAsync();
        }
    }
}
