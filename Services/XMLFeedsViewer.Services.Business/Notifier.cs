namespace XMLFeedsViewer.Services.Business
{
    using Microsoft.AspNet.SignalR;

    using XMLFeedsViewer.Services.Business.Contracts;

    public class Notifier : INotifier
    {
        private readonly IHubContext hubContext;

        public Notifier(IHubContext hubContext)
        {
            this.hubContext = hubContext;
        }

        public void Notify<T>(T data)
        {
            this.hubContext.Clients.All.refreshData(data);
        }
    }
}
