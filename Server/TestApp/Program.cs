namespace TestApp
{
    using XMLFeedsViewer.Services.XmlParser;
    using XMLFeedsViewer.Services.XmlDbFiller;
    using XMLFeedsViewer.Services.DataTransferObjects;
    using XMLFeedsViewer.Services.Data;
    using XMLFeedsViewer.Data;
    using AutoMapper;
    using System.Reflection;
    using XMLFeedsViewer.Web;
    using XMLFeedsViewer.Infrastructure.Mapping;

    public class Program
    {
        static void Main(string[] args)
        {
            AutoMapperConfig.RegisterMappings(
                typeof(Startup).GetTypeInfo().Assembly,
                typeof(SportDTO).GetTypeInfo().Assembly);

            var data = new XMLFeedsViewerData();
            var sportComparer = new XmlEntityEqualityComparer<SportDTO>();
            var eventComparer = new XmlEntityEqualityComparer<EventDTO>();
            var matchComparer = new XmlEntityEqualityComparer<MatchDTO>();
            var betComparer = new XmlEntityEqualityComparer<BetDTO>();
            var oddComparer = new XmlEntityEqualityComparer<OddDTO>();
            var sportsService = new SportsService(data);
            var eventsService = new EventsService(data);
            var matchesService = new MatchesService(data);
            var betsService = new BetsService(data);
            var oddsService = new OddsService(data);

            var parser = new XmlParser("../../../../Feed1.xml");

            var sports = parser.ParseSports();
            var events = parser.ParseEvents();
            var matches = parser.ParseMathes();
            var bets = parser.ParseBets();
            var odds = parser.ParseOdds();

            var dbSeeder = new DbSeeder(data,
                sportComparer,
                eventComparer,
                matchComparer,
                betComparer,
                oddComparer,
                sportsService,
                eventsService,
                matchesService,
                betsService,
                oddsService);

            dbSeeder.SeedFromSportsDTOs(sports);
        }
    }
}
