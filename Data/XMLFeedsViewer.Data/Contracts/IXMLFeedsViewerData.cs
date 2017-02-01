namespace XMLFeedsViewer.Data.Contracts
{
    using System.Threading.Tasks;

    using Models;

    public interface IXMLFeedsViewerData
    {
        IGenericRepository<Odd> Odds { get; }

        IGenericRepository<Bet> Bets { get; }

        IGenericRepository<Match> Matches { get; }

        IGenericRepository<Event> Events { get; }

        IGenericRepository<Sport> Sports { get; }

        int SaveChanges();

        Task<int> SaveChangesAsync();

        IGenericRepository<T> GetRepository<T>() where T : class;
    }
}
