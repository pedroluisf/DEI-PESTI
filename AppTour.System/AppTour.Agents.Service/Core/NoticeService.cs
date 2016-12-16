using AppTour.Business.Services.Observable;

namespace AppTour.Agents.Service.Core
{
    public class NoticeService : ObservableService<NoticeService>
    {
        private object _sync = new object();
        private string _message;
        public string Message
        {
            get { lock (_sync) { return _message; } }
            private set { lock (_sync) { _message = value; } }
        }

        private static readonly NoticeService _instance = new NoticeService();
        public static NoticeService Instance
        {
            get { return _instance; }
        }

        public NoticeService() { }

        public void Notify(string Message)
        {
            this.Message = Message;
            Notify(this);
        }
    }
}
