namespace XMLFeedsViewer.Web.ViewModels
{
    using System.Collections.Generic;

    using XMLFeedsViewer.Infrastructure.Mapping;
    using Services.DataTransferObjects;

    public class MatchViewModel: IMapFrom<MatchDTO>
    {
        public string Name { get; set; }

        public string MatchType { get; set; }

        public ICollection<BetViewModel> Bets { get; set; }
    }
}