using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;


using Android.Content;
using Xamarin.Forms;
using hfupilot.app.ViewModels;
using hfupilot.app.Services;
using System.Threading;
using System.Threading.Tasks;
using Android.Gms.Common;
using Firebase.Messaging;
using Firebase.Iid;
using Android.Util;


namespace hfupilot.app.Droid
{
    [Activity(Label = "hfupilot.app", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        #region BiometricSetup
        public MainActivity()
        {
            Activity = this;
        }

        public static Activity Activity { get; private set; }

        #endregion BiometricSetup


        protected override void OnCreate(Bundle savedInstanceState)
        {
            if (!Forms.IsInitialized)
            {
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(savedInstanceState);

                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
                LoadApplication(new App());
            }
            else
            {
                base.OnCreate(savedInstanceState);
            }

            // Check if our notification was clicked while the app was closed.
            var extra = Intent.GetBooleanExtra("FromNotification", false);
            if (extra)
            {
                CheckForListRequest();
            }
            else if (Intent.Extras?.Get("RemoteKey") != null)
            {
                CheckForListRequest();
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent)
        {
            // Do something with the data you pass from the notification.
            var extra = intent.GetBooleanExtra("FromNotification", false);
            if (extra)
            {
                CheckForListRequest();
            }

            base.OnNewIntent(intent);
        }

        private void CheckForListRequest()
        {
            RunOnUiThread(async () =>
            {
                // Navigate to another view if the notification is clicked.
                var viewModel = App.Services.GetInstance<AnmeldenViewModel>();
                var viewMapper = App.Services.GetInstance<IViewMapper>();

                // Add a wait to make sure our MainView is loaded up.
                await Task.Delay(200);
                await App.Services.GetInstance<INavigation>().PushAsync(viewMapper.Map(viewModel));
            });
        }
    }
}