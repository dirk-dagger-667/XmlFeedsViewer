namespace XmlFeedsViewer.XmlParser.DataTransferObjects
{
    using System.Collections.Generic;

    public class EventDTO : BaseDTO
    {
        public bool IsLive { get; set; }

        // TODO: remove!? 
        public int CategoryID { get; set; }

        public IEnumerable<MatchDTO> Matches { get; set; }
    }
}
