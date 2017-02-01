namespace XMLFeedsViewer.Services.XmlParser.Contracts
{
    using System.Collections.Generic;

    using DataTransferObjects;

    public interface IXmlParser
    {
        IEnumerable<SportDTO> ParseSports();

        IEnumerable<EventDTO> ParseEvents();

        IEnumerable<MatchDTO> ParseMathes();

        IEnumerable<BetDTO> ParseBets();

        IEnumerable<OddDTO> ParseOdds();
    }
}
