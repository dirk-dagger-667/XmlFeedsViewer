namespace XMLFeedsViewer.Services.Business
{
    using XMLFeedsViewer.Services.DataTransferObjects;
    using XMLFeedsViewer.Services.XmlDbFiller;

    public class RefreshedDataResult
    {
        public DataOperationsResult<SportDTO> Sports { get; set; }

        public DataOperationsResult<EventDTO> Events { get; set; }

        public DataOperationsResult<MatchDTO> Matches { get; set; }

        public DataOperationsResult<BetDTO> Bets { get; set; }

        public DataOperationsResult<OddDTO> Odds { get; set; }
    }
}
