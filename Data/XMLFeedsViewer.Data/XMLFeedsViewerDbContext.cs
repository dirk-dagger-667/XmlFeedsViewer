namespace XMLFeedsViewer.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using Common.Models;
    using Models;

    public class XMLFeedsViewerDbContext : DbContext, IXMLFeedsViewerDbContext
    {
        public XMLFeedsViewerDbContext()
            : base("XMLFeedsViewer")
        {

        }

        public IDbSet<Bet> Bets { get; set; }

        public IDbSet<Event> Events { get; set; }

        public IDbSet<Match> Matches { get; set; }

        public IDbSet<Odd> Odds { get; set; }

        public IDbSet<Sport> Sports { get; set; }

        public static XMLFeedsViewerDbContext Create()
        {
            return new XMLFeedsViewerDbContext();
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            this.ApplyAuditInfoRules();
            return await base.SaveChangesAsync();
        }

        private void ApplyAuditInfoRules()
        {
            var changedAudits = this.ChangeTracker.Entries()
                    .Where(e => e.Entity is IAuditInfo && ((e.State == EntityState.Added) || (e.State == EntityState.Modified)));

            foreach (var entry in changedAudits)
            {
                var entity = (IAuditInfo)entry.Entity;

                if (entry.State == EntityState.Added)
                {
                    if (!entity.PreserveCreatedOn)
                    {
                        entity.CreatedOn = DateTime.Now;
                    }
                }
                else
                {
                    entity.ModifiedOn = DateTime.Now;
                }
            }
        }
    }
}
