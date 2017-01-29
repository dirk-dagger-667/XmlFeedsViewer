using Owin;
using Microsoft.Owin;
using XMLFeedsViewer.Infrastructure.Mapping;
using System.Reflection;
using XMLFeedsViewer.Services.DataTransferObjects;

[assembly: OwinStartup(typeof(XMLFeedsViewer.Web.Startup))]
namespace XMLFeedsViewer.Web
{
    using Hangfire;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
            
            AutoMapperConfig.RegisterMappings(
                typeof(Startup).GetTypeInfo().Assembly,
                typeof(SportDTO).GetTypeInfo().Assembly);

            var dashboardOptions = new DashboardOptions();
            app.UseHangfireDashboard("/hangfire", dashboardOptions);
        }
    }
}