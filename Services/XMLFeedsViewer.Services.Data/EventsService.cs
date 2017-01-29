namespace XMLFeedsViewer.Services.Data
{    
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using XMLFeedsViewer.Data.Contracts;
    using XMLFeedsViewer.Data.Models;

    public class EventsService : BaseService<Event>, IEventsService
    {
        public EventsService(IXMLFeedsViewerData data)
            : base(data)
        {
        }

        public override void EditCollectionForDbFiller(IEnumerable<Event> events)
        {
            var eventsDtoIds = events.Select(e => e.XmlId);
            var result = this.data.Events.SearchFor(e => eventsDtoIds.Contains(e.XmlId) && !e.IsDeleted).ToList();

            foreach (var eventDb in events)
            {
                var eventForEditing = result.Where(e => e.XmlId == eventDb.XmlId).FirstOrDefault();
                eventForEditing.Name = eventDb.Name;
                eventForEditing.CategoryId = eventDb.CategoryId;
                eventForEditing.IsLive = eventDb.IsLive;
                eventForEditing.IsDeleted = eventDb.IsDeleted;
                this.data.Events.Update(eventForEditing);
            }
        }
    }
}
