using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XMLFeedsViewer.Data.Common.Models;
using XMLFeedsViewer.Data.Contracts;
using XMLFeedsViewer.Services.Data.Contracts;

namespace XMLFeedsViewer.Services.Data
{
    public abstract class BaseService<T> : IBaseService<T>
        where T : class, IXmlInfo, IDeletableEntity
    {
        public IXMLFeedsViewerData data;

        public IXMLFeedsViewerData Data
        {
            get
            {
                return this.data;
            }

            set
            {
                this.data = value;
            }
        }

        protected BaseService(IXMLFeedsViewerData data)
        {
            this.data = data;
        }

        public virtual IQueryable<T> All()
        {
            return this.data.GetRepository<T>().GetAll();
        }

        public virtual void AddNewCollectionForDbFiller(IEnumerable<T> entities)
        {
            var repository = this.data.GetRepository<T>();

            foreach (var entity in entities)
            {
                repository.Add(entity);
            }
        }

        public virtual void DeleteCollectionForDbFiller(IEnumerable<T> entities)
        {
            var repository = this.data.GetRepository<T>();
            var enititiesDtoIds = entities.Select(e => e.XmlId);
            var result = repository.SearchFor(e => enititiesDtoIds.Contains(e.XmlId) && !e.IsDeleted).ToList();

            foreach (var entity in result)
            {
                entity.IsDeleted = true;
                repository.Update(entity);
            }
        }

        public abstract void EditCollectionForDbFiller(IEnumerable<T> sports);
    }
}
