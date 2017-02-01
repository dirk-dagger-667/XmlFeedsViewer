namespace XmlFeedsViewer.XmlParser.DataTransferObjects
{
    using System.Collections.Generic;

    public class SportDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<EventDTO> Events { get; set; }
    }
}
