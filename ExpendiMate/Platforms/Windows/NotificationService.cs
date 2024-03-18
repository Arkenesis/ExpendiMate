using Microsoft.Toolkit.Uwp.Notifications;

namespace ExpendiMate.Services.PartialMethods
{
    static partial class NotificationService
    {
        static NotificationService()
        {

        }

        static partial void DoSendNotification(string title, string message, DateTime scheduleTime)
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
            .Schedule(scheduleTime);
        }
    }
}
