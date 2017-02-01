namespace XMLFeedsViewer.Web.Infrastructure.HangFireConfig
{
    using System.Web.Hosting;

    public class XmlFeedsViewerPreloader : IProcessHostPreloadClient
    {
        public void Preload(string[] parameters)
        {
            HangfireBootstrapper.Instance.Start();
        }
    }
}
