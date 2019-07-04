using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Java.Lang;
using hfupilot.app.Droid;
using hfupilot.app.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationService))]
namespace hfupilot.app.Droid
{
  public class NotificationService : INotificationService
  {
    public void ShowNotification(string title, string description)
    {
      CreateNotificationChannel();

      var notificationIntent = MainActivity.Activity.PackageManager.GetLaunchIntentForPackage(MainActivity.Activity.PackageName);
      notificationIntent.SetFlags(ActivityFlags.SingleTop);
      notificationIntent.PutExtra("FromNotification", true);
      var pendingIntent = PendingIntent.GetActivity(MainActivity.Activity, 0, notificationIntent, PendingIntentFlags.UpdateCurrent);

      NotificationCompat.Builder builder = new NotificationCompat.Builder(MainActivity.Activity, channelId)
          .SetContentTitle(title)
          .SetContentText(description)
          .SetContentIntent(pendingIntent)
          .SetSmallIcon(Resource.Drawable.ic_audiotrack_dark)
          .SetAutoCancel(true);

      Notification notification = builder.Build();

      NotificationManager notificationManager = MainActivity.Activity.GetSystemService(Context.NotificationService) as NotificationManager;

      const int notificationId = 0;
      notificationManager.Notify(notificationId, notification);
    }

    private void CreateNotificationChannel()
    {
      if (Build.VERSION.SdkInt < BuildVersionCodes.O)
      {
        return;
      }

      var channelName = "Channel";
      var channelDescription = "Super important channel";
      var channel = new NotificationChannel(channelId, channelName, NotificationImportance.Default)
      {
        Description = channelDescription
      };

      var notificationManager = (NotificationManager)MainActivity.Activity.GetSystemService(Context.NotificationService);
      notificationManager.CreateNotificationChannel(channel);
    }

    private string channelId = "Channel";
  }
}
