namespace XMLFeedsViewer.Services.XmlDbFiller.Contracts
{
    using System.Collections.Generic;

    using DataTransferObjects;

    public interface IDbSeeder
    {
        DataOperationsResult<SportDTO> SeedFromSportsDTOs(IEnumerable<SportDTO> sports);

        DataOperationsResult<EventDTO> SeedFromEventDTOs(IEnumerable<EventDTO> events);

        DataOperationsResult<MatchDTO> SeedFromMatchDTOs(IEnumerable<MatchDTO> matches);

        DataOperationsResult<BetDTO> SeedFromBetDTOs(IEnumerable<BetDTO> bets);

        DataOperationsResult<OddDTO> SeedFromOddDTOs(IEnumerable<OddDTO> odds);
    }
}
