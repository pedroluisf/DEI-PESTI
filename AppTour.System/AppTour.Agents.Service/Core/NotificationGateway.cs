
namespace AppTour.Agents.Service.Core
{
    public sealed class NotificationGateway
    {
        public NotificationGateway() { }

        public void SendNotification(string value)
        {
            NoticeService.Instance.Notify(value);
        }
    }
}
