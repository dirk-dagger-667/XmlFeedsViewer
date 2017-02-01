namespace XMLFeedsViewer.Services.XmlDbFiller
{
    using System.Collections.Generic;

    public class DataOperationsResult<T>
    {
        public DataOperationsResult()
        {
            this.Added = new List<T>();
            this.Updated = new List<T>();
            this.Deleted = new List<T>();
        }
        public IEnumerable<T> Added { get; set; }

        public IEnumerable<T> Updated { get; set; }

        public IEnumerable<T> Deleted { get; set; }
    }
}
