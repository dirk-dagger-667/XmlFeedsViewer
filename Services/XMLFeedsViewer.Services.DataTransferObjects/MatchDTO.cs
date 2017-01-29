namespace XMLFeedsViewer.Services.DataTransferObjects
{
    using System;
    using Infrastructure.Mapping;
    using Data.Models;

    public class MatchDTO : BaseDTO, IMapFrom<Match>, IMapTo<Match>
    {
        public string MatchType { get; set; }

        public DateTime? StartTime { get; set; }
    }
}
