namespace XMLFeedsViewer.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    
    public interface IBaseService<T>
        where T : class
    {
        IQueryable<T> All();

        void AddNewCollectionForDbFiller(IEnumerable<T> sports);

        void EditCollectionForDbFiller(IEnumerable<T> sports);

        void DeleteCollectionForDbFiller(IEnumerable<T> sports);
    }
}
