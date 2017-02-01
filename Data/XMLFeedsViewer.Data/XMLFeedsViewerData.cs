namespace XMLFeedsViewer.Data
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Contracts;
    using Models;
    using Repositories;

    public class XMLFeedsViewerData : IXMLFeedsViewerData
    {
        private IXMLFeedsViewerDbContext context;
        private IDictionary<Type, object> repositories;

        public XMLFeedsViewerData()
            :this (new XMLFeedsViewerDbContext())
        {
        }

        public XMLFeedsViewerData(IXMLFeedsViewerDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IGenericRepository<Bet> Bets
        {
            get { return this.GetRepository<Bet>(); }
        }

        public IGenericRepository<Event> Events
        {
            get { return this.GetRepository<Event>(); }
        }

        public IGenericRepository<Match> Matches
        {
            get { return this.GetRepository<Match>(); }
        }

        public IGenericRepository<Odd> Odds
        {
            get { return this.GetRepository<Odd>(); }
        }

        public IGenericRepository<Sport> Sports
        {
            get { return this.GetRepository<Sport>(); }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }

        public IGenericRepository<T> GetRepository<T>() 
            where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var type = typeof(GenericRepository<T>);

                this.repositories.Add(typeOfModel, Activator.CreateInstance(type, this.context));
            }

            return (IGenericRepository<T>)this.repositories[typeOfModel];
        }
    }
}
