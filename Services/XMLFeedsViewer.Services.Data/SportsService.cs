namespace XMLFeedsViewer.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using XMLFeedsViewer.Data.Models;
    using XMLFeedsViewer.Data.Contracts;
    using System.Data.Entity;
    using System.Collections.Generic;
    using System;

    public class SportsService : BaseService<Sport>, ISportsService
    {
        public SportsService(IXMLFeedsViewerData data) 
            : base(data)
        { }

        public override void EditCollectionForDbFiller(IEnumerable<Sport> sports)
        {
            var sportsDtoIds = sports.Select(s => s.XmlId);
            var result = this.data.Sports.SearchFor(s => sportsDtoIds.Contains(s.XmlId) && !s.IsDeleted).ToList();

            foreach (var sport in sports)
            {
                var sportForEditing = result.Where(s => s.XmlId == sport.XmlId).FirstOrDefault();
                sportForEditing.Name = sport.Name;
                sportForEditing.XmlId = sport.XmlId;
                sportForEditing.IsDeleted = sport.IsDeleted;
                this.data.Sports.Update(sportForEditing);
            }
        }
    }
}
