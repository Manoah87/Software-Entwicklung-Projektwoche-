using Android.App;
using Android.Util;
using Firebase.Iid;

namespace hfupilot.app.Droid
{
  [Service]
  [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
  public class MyFirebaseIIDService : FirebaseInstanceIdService
  {
    const string TAG = "FirebaseInit";

    public override void OnTokenRefresh()
    {
      var refreshedToken = FirebaseInstanceId.Instance.Token;
      Log.Debug(TAG, "Refreshed token: " + refreshedToken);
      SendRegistrationToServer(refreshedToken);
    }

    public void SendRegistrationToServer(string token)
    {
      // Add custom implementation, as needed.
    }
  }
}
