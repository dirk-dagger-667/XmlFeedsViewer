namespace XMLFeedsViewer.Services.DataTransferObjects
{
    using System.Collections.Generic;

    using Data.Models;
    using Infrastructure.Mapping;

    public class EventDTO : BaseDTO, IMapFrom<Event>, IMapTo<Event>
    {
        public bool IsLive { get; set; }

        public int CategoryID { get; set; }
    }
}
