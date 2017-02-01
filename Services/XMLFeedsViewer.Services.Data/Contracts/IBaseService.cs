namespace XMLFeedsViewer.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Linq;
    using XMLFeedsViewer.Data.Contracts;

    public interface IBaseService<T>
        where T : class
    {
        IXMLFeedsViewerData Data { get; set; }

        IQueryable<T> All();

        void AddNewCollectionForDbFiller(IEnumerable<T> sports);

        void EditCollectionForDbFiller(IEnumerable<T> sports);

        void DeleteCollectionForDbFiller(IEnumerable<T> sports);
    }
}
