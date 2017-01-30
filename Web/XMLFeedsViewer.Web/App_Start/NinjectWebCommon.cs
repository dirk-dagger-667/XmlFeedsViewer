[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(XMLFeedsViewer.Web.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(XMLFeedsViewer.Web.App_Start.NinjectWebCommon), "Stop")]

namespace XMLFeedsViewer.Web.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Data.Contracts;
    using Data;
    using Data.Repositories;
    using Services.XmlDbFiller;
    using System.Collections.Generic;
    using Services.Business.Contracts;
    using Services.Business;
    using Services.Data.Contracts;
    using Services.Data;
    using Hubs;
    using Microsoft.AspNet.SignalR;
    using Hangfire;

    using XMLFeedsViewer.Services.XmlDbFiller.Contracts;
    using XMLFeedsViewer.Services.XmlParser;
    using XMLFeedsViewer.Services.XmlParser.Contracts;
    using XMLFeedsViewer.Web.Infrastructure.Extensions;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);

                // HangFire configuration
                GlobalConfiguration.Configuration.UseNinjectActivator(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IXMLFeedsViewerDbContext>().To<XMLFeedsViewerDbContext>().InRequestScope();
            kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>)).InRequestScope();
            kernel.Bind<IXMLFeedsViewerData>().To<XMLFeedsViewerData>().InRequestScope();
            kernel.Bind(typeof(IEqualityComparer<>)).To(typeof(XmlEntityEqualityComparer<>)).InRequestScope();
            kernel.Bind<ISportsService>().To<SportsService>().InRequestScope();
            kernel.Bind<IEventsService>().To<EventsService>().InRequestScope();
            kernel.Bind<IMatchesService>().To<MatchesService>().InRequestScope();
            kernel.Bind<IBetsService>().To<BetsService>().InRequestScope();
            kernel.Bind<IOddsService>().To<OddsService>().InRequestScope();

            kernel.Bind<IDataRefresher>().To<DataRefresher>().InRequestOrBackgroundJobScope();
            kernel.Bind<IBackgroundJobsService>().To<BackgroundJobsService>().InRequestOrBackgroundJobScope();
            kernel.Bind<INotifier>().To<Notifier>().InRequestOrBackgroundJobScope();
            kernel.Bind<IDbSeeder>().To<DbSeeder>().InRequestOrBackgroundJobScope();
            kernel.Bind<IXmlParser>().To<XmlParser>()
                .InRequestOrBackgroundJobScope()
                .WithConstructorArgument("urlPath", @"http://vitalbet.net/sportxml"); // TODO: Change to original URL
            kernel.Bind<FeedsHub>().ToSelf();

            kernel
                .Bind<IHubContext>()
                .ToMethod(context => GlobalHost.ConnectionManager.GetHubContext<FeedsHub>());
        }
    }
}
