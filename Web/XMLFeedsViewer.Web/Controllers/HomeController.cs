using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using XMLFeedsViewer.Services.XmlParser;

namespace XMLFeedsViewer.Web.Controllers
{
    using Hangfire;

    using XMLFeedsViewer.Services.Business.Contracts;

    public class HomeController : Controller
    {
        private static bool started;

        private readonly IBackgroundJobsService backgroundJobs;
        private readonly IDataRefresher dataRefresher;

        public HomeController(IBackgroundJobsService backgroundJobs, IDataRefresher dataRefresher)
        {
            this.backgroundJobs = backgroundJobs;
            this.dataRefresher = dataRefresher;
        }

        public ActionResult Index()
        {
            if (!started)
            {

                this.backgroundJobs.AddOrUpdateRecurringJob<IDataRefresher>(
                    "IDataRefresher.Refresh",
                    x => x.Refresh(),
                    Cron.Minutely());

                started = true;
            }

            // TODO: Get data from DB
            return View();
        }
    }
}