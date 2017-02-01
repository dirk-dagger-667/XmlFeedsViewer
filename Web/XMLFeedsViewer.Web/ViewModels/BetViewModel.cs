namespace XMLFeedsViewer.Web.ViewModels
{
    using System.Collections.Generic;

    using XMLFeedsViewer.Infrastructure.Mapping;
    using Services.DataTransferObjects;

    public class BetViewModel: IMapFrom<BetDTO>
    {
        public string Name { get; set; }

        public bool IsLive { get; set; }

        public ICollection<OddViewModel> Odds { get; set; } 
    }
}