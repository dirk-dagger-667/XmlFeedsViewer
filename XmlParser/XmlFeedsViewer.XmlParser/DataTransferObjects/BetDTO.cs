namespace XmlFeedsViewer.XmlParser.DataTransferObjects
{
    using System.Collections.Generic;

    public class BetDTO : BaseDTO
    {
        public bool IsLive { get; set; }

        public IEnumerable<OddDTO> Odds { get; set; }
    }
}
