namespace XMLFeedsViewer.Services.XmlDbFiller
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using Contracts;
    using Data.Contracts;
    using DataTransferObjects;
    using XMLFeedsViewer.Data.Contracts;
    using XMLFeedsViewer.Data.Models;
    using Infrastructure.Mapping;

    // TODO: Abstract seed methods with one generic method
    public class DbSeeder : IDbSeeder
    {
        private readonly IXMLFeedsViewerData data;
        private readonly IEqualityComparer<SportDTO> sportComparer;
        private readonly IEqualityComparer<EventDTO> eventComparer;
        private readonly IEqualityComparer<MatchDTO> matchComparer;
        private readonly IEqualityComparer<BetDTO> betComparer;
        private readonly IEqualityComparer<OddDTO> oddComparer;
        private readonly ISportsService sportsService;
        private readonly IEventsService eventsService;
        private readonly IMatchesService matchesService;
        private readonly IBetsService betsService;
        private readonly IOddsService oddsService;

        public DbSeeder(IXMLFeedsViewerData data,
            IEqualityComparer<SportDTO> sportComparer,
            IEqualityComparer<EventDTO> eventComparer,
            IEqualityComparer<MatchDTO> matchComparer,
            IEqualityComparer<BetDTO> betComparer,
            IEqualityComparer<OddDTO> oddComparer,
            ISportsService sportsService,
            IEventsService eventsService,
            IMatchesService matchesService,
            IBetsService betsService,
            IOddsService oddsService)
        {
            this.data = data;
            this.sportComparer = sportComparer;
            this.eventComparer = eventComparer;
            this.matchComparer = matchComparer;
            this.betComparer = betComparer;
            this.oddComparer = oddComparer;
            this.sportsService = sportsService;
            this.eventsService = eventsService;
            this.matchesService = matchesService;
            this.betsService = betsService;
            this.oddsService = oddsService;
        }

        public void SeedDbFromDTOs(IEnumerable<SportDTO> sports)
        {
            foreach (var sport in sports)
            {
                var sportDb = Mapper.Map<Sport>(sport);
                this.data.Sports.Add(sportDb);
            }

            this.data.SaveChanges();
        }

        public DataOperationsResult<SportDTO> SeedFromSportsDTOs(IEnumerable<SportDTO> sports)
        {
            var result = new DataOperationsResult<SportDTO>();

            var oldSports = this.data.Sports.SearchFor(os => os.IsDeleted == false).ToList();

            if (oldSports.Count == 0)
            {
                this.sportsService.AddNewCollectionForDbFiller(Mapper.Map<IEnumerable<Sport>>(sports));
                result.Added = sports;
            }
            else
            {
                var oldSportsAsDTO = Mapper.Map<IEnumerable<SportDTO>>(oldSports);
                var sportsForUpdate = oldSportsAsDTO.Intersect(sports, this.sportComparer);
                var sportsForDeletion = oldSportsAsDTO.Except(sportsForUpdate, this.sportComparer);
                var sportsForAdding = sports.Except(sportsForUpdate, this.sportComparer);

                if (sportsForDeletion.Any())
                {
                    this.sportsService.DeleteCollectionForDbFiller(Mapper.Map<IEnumerable<Sport>>(sportsForDeletion));
                    result.Deleted = result.Deleted.Concat(sportsForDeletion);
                }

                if (sportsForUpdate.Any())
                {
                    this.sportsService.EditCollectionForDbFiller(Mapper.Map<IEnumerable<Sport>>(sportsForUpdate));
                    result.Updated = result.Updated.Concat(sportsForUpdate);
                }

                if (sportsForAdding.Any())
                {
                    sportsService.AddNewCollectionForDbFiller(Mapper.Map<IEnumerable<Sport>>(sportsForAdding));
                    result.Added = result.Added.Concat(sportsForAdding);
                }
            }

            this.sportsService.Data.SaveChanges();

            return result;
        }

        public DataOperationsResult<EventDTO> SeedFromEventDTOs(IEnumerable<EventDTO> events)
        {
            var result = new DataOperationsResult<EventDTO>();

            var sports = this.data.Sports
                .SearchFor(s => s.IsDeleted == false)
                .Select(s => new { s.Id, s.XmlId })
                .ToList();

            foreach (var sport in sports)
            {
                var oldEvents = this.data.Events.SearchFor(e => e.ParentXmlId == sport.XmlId && e.IsDeleted == false).ToList();
                var newEvents = events.Where(e => e.ParentXmlId == sport.XmlId);

                if (oldEvents.Count == 0)
                {
                    this.eventsService.AddNewCollectionForDbFiller(Mapper.Map<IEnumerable<Event>>(events));

                    result.Added = events;
                    this.eventsService.Data.SaveChanges();
                }
                else
                {
                    var oldEventsAsDTO = Mapper.Map<IEnumerable<EventDTO>>(oldEvents);
                    var eventsForUpdate = oldEventsAsDTO.Intersect(newEvents, this.eventComparer);
                    var eventsForDeletion = oldEventsAsDTO.Except(eventsForUpdate, this.eventComparer);
                    var eventsForAdding = newEvents.Except(eventsForUpdate, this.eventComparer);

                    if (eventsForDeletion.Any())
                    {
                        this.eventsService.DeleteCollectionForDbFiller(Mapper.Map<IEnumerable<Event>>(eventsForDeletion));
                        result.Deleted = result.Deleted.Concat(eventsForDeletion);
                    }

                    if (eventsForUpdate.Any())
                    {
                        this.eventsService.EditCollectionForDbFiller(Mapper.Map<IEnumerable<Event>>(eventsForUpdate));
                        result.Updated = result.Updated.Concat(eventsForUpdate);
                    }

                    if (eventsForAdding.Any())
                    {
                        var dbEventsToAdd = Mapper.Map<IEnumerable<Event>>(eventsForAdding);
                        foreach (var @event in dbEventsToAdd)
                        {
                            @event.SportId = sport.Id;
                        }

                        this.eventsService.AddNewCollectionForDbFiller(dbEventsToAdd);

                        result.Added = result.Added.Concat(eventsForAdding);
                    }
                }
            }

            this.eventsService.Data.SaveChanges();

            return result;
        }

        public DataOperationsResult<MatchDTO> SeedFromMatchDTOs(IEnumerable<MatchDTO> matches)
        {
            var result = new DataOperationsResult<MatchDTO>();

            var events = this.data.Events
                .SearchFor(s => s.IsDeleted == false)
                .Select(e => new { e.Id, e.XmlId })
                .ToList();

            foreach (var eventDb in events)
            {
                var oldMatches = this.data.Matches.SearchFor(m => m.ParentXmlId == eventDb.XmlId && m.IsDeleted == false).ToList();
                var newMatches = matches.Where(e => e.ParentXmlId == eventDb.XmlId);

                if (oldMatches.Count == 0 && newMatches.Any())
                {
                    this.matchesService.AddNewCollectionForDbFiller(Mapper.Map<IEnumerable<Match>>(newMatches));
                    result.Added = result.Added.Concat(newMatches);
                    this.matchesService.Data.SaveChanges();
                }
                else
                {
                    var oldMatchesAsDTO = Mapper.Map<IEnumerable<MatchDTO>>(oldMatches);
                    var matchesForUpdate = oldMatchesAsDTO.Intersect(newMatches, this.matchComparer);
                    var matchesForDeletion = oldMatchesAsDTO.Except(matchesForUpdate, this.matchComparer);
                    var matchesForAdding = newMatches.Except(matchesForUpdate, this.matchComparer);

                    if (matchesForDeletion.Any())
                    {
                        this.matchesService.DeleteCollectionForDbFiller(Mapper.Map<IEnumerable<Match>>(matchesForDeletion));
                        result.Deleted = result.Deleted.Concat(matchesForDeletion);
                    }

                    if (matchesForUpdate.Any())
                    {
                        this.matchesService.EditCollectionForDbFiller(Mapper.Map<IEnumerable<Match>>(matchesForUpdate));
                        result.Updated = result.Updated.Concat(matchesForUpdate);
                    }

                    if (matchesForAdding.Any())
                    {
                        var dbMatchesToAdd = Mapper.Map<IEnumerable<Match>>(matchesForAdding);

                        this.matchesService.AddNewCollectionForDbFiller(dbMatchesToAdd);

                        result.Added = result.Added.Concat(matchesForAdding);
                    }
                }
            }

            this.matchesService.Data.SaveChanges();

            return result;
        }

        public DataOperationsResult<BetDTO> SeedFromBetDTOs(IEnumerable<BetDTO> bets)
        {
            var result = new DataOperationsResult<BetDTO>();

            var matches = this.data.Matches
                .SearchFor(s => s.IsDeleted == false)
                .Select(e => new { e.Id, e.XmlId })
                .ToList();

            foreach (var match in matches)
            {
                var oldBets = this.data.Bets.SearchFor(b => b.ParentXmlId == match.XmlId && b.IsDeleted == false).ToList();
                var newBets = bets.Where(e => e.ParentXmlId == match.XmlId);

                if (oldBets.Count == 0 && newBets.Any())
                {
                    this.betsService.AddNewCollectionForDbFiller(Mapper.Map<IEnumerable<Bet>>(newBets));
                    result.Added = result.Added.Concat(newBets);

                    this.data.SaveChanges();
                }
                else
                {
                    var oldBetsAsDTO = Mapper.Map<IEnumerable<BetDTO>>(oldBets);
                    var betsForUpdate = oldBetsAsDTO.Intersect(newBets, this.betComparer);
                    var betsForDeletion = oldBetsAsDTO.Except(betsForUpdate, this.betComparer);
                    var betsForAdding = newBets.Except(betsForUpdate, this.betComparer);

                    if (betsForDeletion.Any())
                    {
                        this.betsService.DeleteCollectionForDbFiller(Mapper.Map<IEnumerable<Bet>>(betsForDeletion));
                        result.Deleted = result.Deleted.Concat(betsForDeletion);
                    }

                    if (betsForUpdate.Any())
                    {
                        this.betsService.EditCollectionForDbFiller(Mapper.Map<IEnumerable<Bet>>(betsForUpdate));
                        result.Updated = result.Updated.Concat(betsForUpdate);
                    }

                    if (betsForAdding.Any())
                    {
                        var dbBetsToAdd = Mapper.Map<IEnumerable<Bet>>(betsForAdding);

                        foreach (var bet in dbBetsToAdd)
                        {
                            bet.MatchId = match.Id;
                        }

                        this.betsService.AddNewCollectionForDbFiller(dbBetsToAdd);

                        result.Added = result.Added.Concat(betsForAdding);
                    }
                }
            }

            this.data.SaveChanges();

            return result;
        }

        public DataOperationsResult<OddDTO> SeedFromOddDTOs(IEnumerable<OddDTO> odds)
        {
            var result = new DataOperationsResult<OddDTO>();

            var bets = this.data.Bets
                .SearchFor(s => s.IsDeleted == false)
                .Select(e => new { e.Id, e.XmlId })
                .ToList();

            foreach (var bet in bets)
            {
                var oldOdds = this.data.Odds.SearchFor(o => o.ParentXmlId == bet.XmlId && o.IsDeleted == false).ToList();
                var newOdds = odds.Where(e => e.ParentXmlId == bet.XmlId);

                if (oldOdds.Count == 0 && newOdds.Any())
                {
                    this.oddsService.AddNewCollectionForDbFiller(Mapper.Map<IEnumerable<Odd>>(newOdds));

                    result.Added = result.Added.Concat(newOdds);

                    this.data.SaveChanges();
                }
                else
                {
                    var oldOddsAsDTO = Mapper.Map<IEnumerable<OddDTO>>(oldOdds);
                    var oddsForUpdate = oldOddsAsDTO.Intersect(newOdds, this.oddComparer);
                    var oddsForDeletion = oldOddsAsDTO.Except(oddsForUpdate, this.oddComparer);
                    var oddsForAdding = newOdds.Except(oddsForUpdate, this.oddComparer);

                    if (oddsForDeletion.Any())
                    {
                        this.oddsService.DeleteCollectionForDbFiller(Mapper.Map<IEnumerable<Odd>>(oddsForDeletion));
                        result.Deleted = result.Deleted.Concat(oddsForDeletion);
                    }

                    if (oddsForUpdate.Any())
                    {
                        this.oddsService.EditCollectionForDbFiller(Mapper.Map<IEnumerable<Odd>>(oddsForUpdate));
                        result.Updated = result.Updated.Concat(oddsForUpdate);
                    }

                    if (oddsForAdding.Any())
                    {
                        this.oddsService.AddNewCollectionForDbFiller(Mapper.Map<IEnumerable<Odd>>(oddsForAdding));

                        var dbOddsToAdd = Mapper.Map<IEnumerable<Odd>>(oddsForAdding);

                        foreach (var odd in dbOddsToAdd)
                        {
                            odd.BetId = bet.Id;
                        }

                        result.Added = result.Added.Concat(oddsForAdding);
                    }
                }
            }

            this.data.SaveChanges();

            return result;
        }
    }
}

