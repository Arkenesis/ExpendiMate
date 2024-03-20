using Microsoft.Toolkit.Uwp.Notifications;
using System.Text;
using Windows.UI.Notifications;

namespace ExpendiMate.Services.PartialMethods
{
    static partial class NotificationService
    {
        static NotificationService()
        {

        }

        static partial void DoSendNotification(int id, string title, string message, DateTime scheduleTime)
        {
            ToastButton button = new ToastButton()
                .SetContent("See More")
                .AddArgument("action", "seeMore")
                .SetAfterActivationBehavior(ToastAfterActivationBehavior.Default);

            new ToastContentBuilder()
            .AddArgument("action", "viewConversation")
            .AddArgument("conversationId", 9813)
            .AddText(title)
            .AddText(message)
            .AddButton(button)
            .Schedule(scheduleTime, toast =>
            {
                toast.Tag = id.ToString();
                toast.Group = "Expenses Notification";
            });
        }

        //https://learn.microsoft.com/en-us/windows/apps/design/shell/tiles-and-notifications/scheduled-toast
        static partial void DoDeleteNotification(int id)
        {
            // Create the toast notifier
            ToastNotifierCompat notifier = ToastNotificationManagerCompat.CreateToastNotifier();

            // Get the list of scheduled toasts that haven't appeared yet
            IReadOnlyList<ScheduledToastNotification> scheduledToasts = notifier.GetScheduledToastNotifications();

            // Find our scheduled toast we want to cancel
            var toRemove = scheduledToasts.FirstOrDefault(i => i.Tag == id.ToString() && i.Group == "Expenses Notification");
            if (toRemove != null)
            {
                // And remove it from the schedule
                notifier.RemoveFromSchedule(toRemove);
            }
        }
    }
}
