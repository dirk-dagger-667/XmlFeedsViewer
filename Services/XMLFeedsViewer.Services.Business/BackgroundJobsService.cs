namespace XMLFeedsViewer.Services.Business
{
    using System;
    using System.Linq.Expressions;

    using Hangfire;

    using Contracts;

    public class BackgroundJobsService : IBackgroundJobsService
    {
        public void AddOrUpdateRecurringJob<T>(string recurringJobId, Expression<Action<T>> methodCall, string cron)
        {
            RecurringJob.AddOrUpdate(recurringJobId, methodCall, cron);
        }
    }
}
