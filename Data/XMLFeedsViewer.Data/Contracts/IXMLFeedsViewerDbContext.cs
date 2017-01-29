namespace XMLFeedsViewer.Data.Contracts
{
    using Models;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Threading.Tasks;

    public interface IXMLFeedsViewerDbContext
    {
        IDbSet<Odd> Odds { get; set; }
        
        IDbSet<Bet> Bets { get; set; }
        
        IDbSet<Match> Matches { get; set; }
        
        IDbSet<Event> Events { get; set; }
        
        IDbSet<Sport> Sports { get; set; } 

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}
