using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using System.Threading.Tasks;
using Android.Support.V4.Content;
using Xamarin.Forms;
using gaweFirstSimpleNoteApp.Droid.Services;
using gaweFirstSimpleNoteApp.Services;

[assembly: Dependency(typeof(AndroidMicrophoneService))]
namespace gaweFirstSimpleNoteApp.Droid.Services
{
    public class AndroidMicrophoneService : IMicrophoneService
    {
        public const int RecordAudioPermissionCode = 1;
        private TaskCompletionSource<bool> _tcsPermissions;
        string[] permissions = new string[] { Manifest.Permission.RecordAudio };
        public Task<bool> GetPermissionAsync()
        {
            _tcsPermissions = new TaskCompletionSource<bool>();
            if ((int)Build.VERSION.SdkInt < 23)
                _tcsPermissions.TrySetResult(true);
            else
            {
                var currentActivity = MainActivity.Instance;
                if (ContextCompat.CheckSelfPermission(currentActivity, Manifest.Permission.RecordAudio) != Permission.Granted)
                    RequestMicPermissions();
                else
                    _tcsPermissions.TrySetResult(true);
            }
            return _tcsPermissions.Task;
        }
        public void OnRequestPermissionResult(bool isGranted) => _tcsPermissions.TrySetResult(isGranted);
        public void RequestMicPermissions()
        {
            if (ActivityCompat.ShouldShowRequestPermissionRationale(MainActivity.Instance, Manifest.Permission.RecordAudio))
            {
                Snackbar.Make(MainActivity.Instance.FindViewById(Android.Resource.Id.Content),
                        "Microphone permissions are required for speech transcription!",
                        Snackbar.LengthIndefinite)
                    .SetAction("Ok", v =>
                    {
                        ((Activity)MainActivity.Instance).RequestPermissions(permissions, RecordAudioPermissionCode);
                    })
                    .Show();
            }
            else
                ActivityCompat.RequestPermissions((Activity)MainActivity.Instance, permissions, RecordAudioPermissionCode);
        }
    }
}