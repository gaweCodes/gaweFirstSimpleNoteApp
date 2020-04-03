using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace gaweFirstSimpleNoteApp.Services
{
        public class NoteService
        {
            private readonly HttpClient _httpClient = new HttpClient();
            private readonly string _baseUrl = "https://gawefirstsimplenoteapp.azurewebsites.net/api/note";
            public NoteService(string token = null)
            {
                if (token != null)
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            public async Task<string> GetAll(Guid userId)
            {
                Thread.Sleep(200);
                var response = await _httpClient.GetAsync(_baseUrl+"?userId="+userId);
                if (!response.IsSuccessStatusCode) return "ERROR: " + response.ReasonPhrase;
                return await response.Content.ReadAsStringAsync();
            }
            public async Task<string> GetById(Guid noteId)
            {
                var response = await _httpClient.GetAsync(Path.Combine(_baseUrl, noteId.ToString()));
                if (!response.IsSuccessStatusCode) return "ERROR: " + response.ReasonPhrase;
                return await response.Content.ReadAsStringAsync();
            }
            public async Task<string> Add(string data)
            {
                var response = await _httpClient.PostAsync(_baseUrl,
                    new StringContent(data, Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode) return "ERROR: " + response.ReasonPhrase;
                return string.Empty;
            }
            public async Task<string> Update(string data)
            {
                var response = await _httpClient.PutAsync(_baseUrl,
                    new StringContent(data, Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode) return "ERROR: " + response.ReasonPhrase;
                return string.Empty;
            }
            public async Task<string> Delete(Guid noteId)
            {
                var response = await _httpClient.DeleteAsync(_baseUrl+"?noteId="+noteId);
                if (!response.IsSuccessStatusCode) return "ERROR: " + response.ReasonPhrase;
                return string.Empty;
            }
        }
}
