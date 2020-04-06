using System.Collections.Generic;
using System.Threading.Tasks;
using gaweFirstSimpleNoteApp.Models;
using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Intent;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp.Services
{
    public class LuisService
    {
        private readonly IMicrophoneService _micService;
        public LuisService() => _micService = DependencyService.Resolve<IMicrophoneService>();
        private async Task<bool> Setup()
        {
            if (await _micService.GetPermissionAsync()) return true;
            await Application.Current.MainPage.DisplayAlert("Gawe Notes", "Please grant access to the microphone!", "OK");
            return false;
        }
        public async Task<IntentRecognition> GetRecognizedIntent()
        {
            if (await Setup())
            {
                var config = SpeechConfig.FromSubscription(Constants.LuisPredictionPrimaryKey, Constants.Region);
                using (var recognizer = new IntentRecognizer(config))
                {
                    var model = LanguageUnderstandingModel.FromSubscription(Constants.LuisAuthorizingPrimaryKey,
                        Constants.AppId, Constants.Region);
                    recognizer.AddAllIntents(model);

                    var result = await recognizer.RecognizeOnceAsync();
                    if (result.Reason == ResultReason.RecognizedIntent)
                    {
                        var json = result.Properties.GetProperty(PropertyId
                            .LanguageUnderstandingServiceResponse_JsonResult);
                        return Newtonsoft.Json.JsonConvert.DeserializeObject<IntentRecognition>(json);
                    }

                    await TextToSpeech.SpeakAsync(result.Reason.ToString());
                }
            }
            return new IntentRecognition
                {
                    Query = string.Empty,
                    Entities = new List<Entity>(),
                    TopScoringIntent = new TopScoringIntent {Intent = "None", Score = 1}
                };
        }
    }
}
