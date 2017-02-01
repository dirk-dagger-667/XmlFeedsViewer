namespace XMLFeedsViewer.Services.DataTransferObjects
{
    using Data.Models;
    using Infrastructure.Mapping;

    public class BetDTO : BaseDTO, IMapFrom<Bet>, IMapTo<Bet>
    {
        public bool IsLive { get; set; }
    }
}
