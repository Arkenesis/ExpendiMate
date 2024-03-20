using Android.App;
using Android.Content;
using Android.Runtime;
using AndroidX.Core.App;

namespace ExpendiMate.Services.PartialMethods;

static partial class NotificationService
{
    private static Context Context;
    public const string CHANNEL_ID = "1";

    static NotificationService()
    {
        //This constructor will run before the first call is made to DoSendNotification
        //It gets the NotificationManager for the android device and creates a channel
        //to send notifications on
        Context = Platform.CurrentActivity.ApplicationContext;
        var channelName = "Festival Holledau";
        var channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationImportance.Default);
        NotificationManager notificationManager = Context.GetSystemService(Context.NotificationService)
            as NotificationManager;
        notificationManager.CreateNotificationChannel(channel);

    }

    static partial void DoSendNotification(int id, string title, string message, DateTime scheduleTime)
    {
        //Get the devices native alarm manager
        AlarmManager alarmManager = Context.GetSystemService(Context.AlarmService).JavaCast<AlarmManager>();
        var alarmIntent = new Intent(Context, typeof(AlarmReceiver));
        //Add extra tags so we can read it later
        alarmIntent.PutExtra("id", id);
        alarmIntent.PutExtra("title", title);
        alarmIntent.PutExtra("message", message);

        DateTime dateTime = scheduleTime;

        DateTimeOffset dateOffsetValue = DateTimeOffset.Parse(dateTime.ToString());
        long millisecondsToBegin = dateOffsetValue.ToUnixTimeMilliseconds();
        PendingIntent pending = PendingIntent.GetBroadcast(Context, id, alarmIntent, PendingIntentFlags.UpdateCurrent);
        //Schedule the alarm to trigger our AlarmReceiver at the designated time
        alarmManager.Set(AlarmType.RtcWakeup, millisecondsToBegin, pending);
    }

    //https://stackoverflow.com/questions/3595232/android-remove-notification-from-notification-bar?rq=3
    static partial void DoDeleteNotification(int id)
    {
        // Optionally, you might also want to cancel any alarms associated with this notification
        AlarmManager alarmManager = Context.GetSystemService(Context.AlarmService).JavaCast<AlarmManager>();
        PendingIntent pendingIntent = PendingIntent.GetBroadcast(Context, id, new Intent(Context, typeof(AlarmReceiver)), PendingIntentFlags.UpdateCurrent);
        alarmManager.Cancel(pendingIntent);

        // Get the notification service
        String ns = Context.NotificationService;
        NotificationManager nMgr = (NotificationManager)Context.GetSystemService(ns);

        // Cancel the notification using the provided notifyId
        nMgr.Cancel(id);
    }

}

[BroadcastReceiver]
public class AlarmReceiver : BroadcastReceiver
{
    //Receive the alarm broadcast
    public override void OnReceive(Context context, Intent intent)
    {
        //Get the extra data we added to the alarm earlier
        var title = intent.GetStringExtra("title");
        var message = intent.GetStringExtra("message");
        var idString = intent.GetStringExtra("id");
        var id = Convert.ToInt32(idString);
        //Specify that the notification should launch our app, and attach some custom data to read later
        Intent resultIntent = new Intent(context, typeof(MainActivity));
        resultIntent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask);
        resultIntent.PutExtra("test_key", "thisisatestkey");

        const int pendingIntentId = 0;
        PendingIntent pendingIntent = PendingIntent.GetActivity(context, pendingIntentId, resultIntent, PendingIntentFlags.OneShot);
        var color = new Color(20, 20, 20).ToInt();
        //Build the notification
        NotificationCompat.Builder builder = new NotificationCompat.Builder(context, NotificationService.CHANNEL_ID)
            .SetContentTitle(title)
            .SetContentText(message)
            .SetDefaults((int)(NotificationDefaults.Sound | NotificationDefaults.Vibrate))
            .SetSmallIcon(Resource.Drawable.money_bag) //This line references the image in our Resources folder we added previously
            .SetColor(color)
            .SetContentIntent(pendingIntent)
            .SetPriority((int)NotificationPriority.High);
        NotificationManager notificationManager = context.GetSystemService(Context.NotificationService) as NotificationManager;
        Notification notification = builder.Build();
        //Call the notification
        notificationManager.Notify(id, notification);
    }
}
