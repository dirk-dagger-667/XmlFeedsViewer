namespace XMLFeedsViewer.Services.Business
{
    using Contracts;
    using XmlDbFiller.Contracts;
    using XmlParser.Contracts;

    public class DataRefresher : IDataRefresher
    {
        private readonly IXmlParser xmlParser;
        private readonly IDbSeeder dbSeeder;
        private readonly INotifier notifier;

        public DataRefresher(IXmlParser xmlParser, IDbSeeder dbSeeder, INotifier notifier)
        {
            this.xmlParser = xmlParser;
            this.dbSeeder = dbSeeder;
            this.notifier = notifier;
        }

        public void Refresh()
        {
            var sports = this.xmlParser.ParseSports();
            var events = this.xmlParser.ParseEvents();
            var matches = this.xmlParser.ParseMathes();
            //var bets = this.xmlParser.ParseBets();
            //var odds = this.xmlParser.ParseOdds();

            var refreshedData = new RefreshedDataResult();

            refreshedData.Sports = this.dbSeeder.SeedFromSportsDTOs(sports);
            refreshedData.Events = this.dbSeeder.SeedFromEventDTOs(events);
            refreshedData.Matches = this.dbSeeder.SeedFromMatchDTOs(matches);
            //refreshedData.Bets = this.dbSeeder.SeedFromBetDTOs(bets);
            //refreshedData.Odds = this.dbSeeder.SeedFromOddDTOs(odds);

            this.notifier.Notify(refreshedData);
        }
    }
}
