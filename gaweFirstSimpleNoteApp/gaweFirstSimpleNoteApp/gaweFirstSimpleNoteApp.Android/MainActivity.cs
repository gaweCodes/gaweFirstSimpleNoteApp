using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using gaweFirstSimpleNoteApp.Droid.Services;
using gaweFirstSimpleNoteApp.Services;
using Xamarin.Forms;

namespace gaweFirstSimpleNoteApp.Droid
{
    [Activity(Label = "Gawe Notes", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        IMicrophoneService _micService;
        internal static MainActivity Instance { get; private set; }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            Instance = this;
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
            _micService = DependencyService.Resolve<IMicrophoneService>();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            switch (requestCode)
            {
                case AndroidMicrophoneService.RecordAudioPermissionCode:
                    _micService.OnRequestPermissionResult(grantResults[0] == Permission.Granted);
                    break;
            }
        }
    }
}