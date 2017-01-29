namespace XMLFeedsViewer.Web.ViewModels
{
    using System;
    using System.Collections.Generic;

    using XMLFeedsViewer.Infrastructure.Mapping;
    using Services.DataTransferObjects;

    public class EventViewModel: IMapFrom<EventDTO>
    {
        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public ICollection<MatchViewModel> Matches { get; set; }
    }
}