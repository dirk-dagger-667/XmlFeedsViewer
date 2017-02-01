namespace XmlFeedsViewer.XmlParser.DataTransferObjects
{
    using System.Collections.Generic;

    public class MatchDTO: BaseDTO
    {
        public string MatchType { get; set; }

        public IEnumerable<BetDTO> Bets{ get; set; }
    }
}
