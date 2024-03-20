namespace ExpendiMate.Services.PartialMethods;

static partial class NotificationService
{
    public static void SendNotification(int id, string title, string message, DateTime scheduleTime)
    {
        DoSendNotification(id, title, message, scheduleTime);
    }
    public static void DeleteNotification(int id)
    {
        DoDeleteNotification(id);
    }

    static partial void DoSendNotification(int id, string title, string message, DateTime scheduleTime);
    static partial void DoDeleteNotification(int id);
}

