namespace XMLFeedsViewer.Services.Business.Contracts
{
    using System;
    using System.Linq.Expressions;

    public interface IBackgroundJobsService
    {
        void AddOrUpdateRecurringJob<T>(string recurringJobId, Expression<Action<T>> methodCall, string cron);
    }
}
