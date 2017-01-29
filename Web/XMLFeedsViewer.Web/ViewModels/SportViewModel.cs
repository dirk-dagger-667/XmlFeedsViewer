namespace XMLFeedsViewer.Web.ViewModels
{
    using System.Collections.Generic;

    using XMLFeedsViewer.Infrastructure.Mapping;
    using Services.DataTransferObjects;

    public class SportViewModel: IMapFrom<SportDTO>
    {
        public string Name { get; set; }

        public ICollection<EventViewModel> Events { get; set; }
    }
}