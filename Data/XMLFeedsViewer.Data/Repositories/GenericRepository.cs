namespace XMLFeedsViewer.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    using Contracts;

    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private IXMLFeedsViewerDbContext dbContext;
        private IDbSet<T> dbSet;

        public GenericRepository(IXMLFeedsViewerDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }

        public T GetbyId(object id)
        {
            return this.dbSet.Find(id);
        }

        public void Add(T entity)
        {
            var entry = this.AttachIfDetached(entity);
            entry.State = EntityState.Added;
        }

        public void Delete(T entity)
        {
            var entry = this.AttachIfDetached(entity);
            entry.State = EntityState.Deleted;
        }

        public void Detach(T entity)
        {
            var entry = this.AttachIfDetached(entity);
            entry.State = EntityState.Detached;
        }

        public void Update(T entity)
        {
            var entry = this.AttachIfDetached(entity);
            entry.State = EntityState.Modified;
        }

        public IQueryable<T> GetAll()
        {
            return this.dbSet.AsQueryable();
        }

        public IQueryable<T> Include(string path)
        {
            return this.dbSet.Include(path);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> conditions)
        {
            return this.GetAll().Where(conditions);
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this.dbContext.SaveChangesAsync();
        }

        private DbEntityEntry AttachIfDetached(T entity)
        {
            var entry = this.dbContext.Entry(entity);

            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            return entry;
        }
    }
}
