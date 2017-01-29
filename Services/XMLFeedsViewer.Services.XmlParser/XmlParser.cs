namespace XMLFeedsViewer.Services.XmlParser
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Xml.Linq;

    using Contracts;
    using DataTransferObjects;

    public class XmlParser : IXmlParser
    {
        private XDocument xdoc;

        public XmlParser(string urlPath)
        {
            this.xdoc = XDocument.Load(urlPath);
        }

        //public IEnumerable<SportDTO> ParseAllData()
        //{
        //    try
        //    {
        //        return from sport in this.xdoc.Descendants("Sport")
        //               where sport.Descendants("Bet").Any()
        //               select new SportDTO
        //               {
        //                   Id = int.Parse(sport.Attribute("ID").Value),
        //                   Name = sport.Attribute("Name").Value,
        //                   //Projecting only the Events that have Odds to EventDTO
        //                   Events = sport.Descendants("Event").Where(e => e.Descendants("Odd").Any()).Select(e => new EventDTO
        //                   {
        //                       Id = int.Parse(sport.Element("Event").Attribute("ID").Value),
        //                       Name = e.Attribute("Name").Value,
        //                       IsLive = bool.Parse(sport.Element("Event").Attribute("IsLive").Value),
        //                       CategoryID = int.Parse(sport.Element("Event").Attribute("CategoryID").Value),
        //                       //Projecting only the Matches that have Odds and attribute StartDate is greater than
        //                       //the XML feeds CreateDate and less than the XML feeds (CreateDate + 1 day) 
        //                       Matches = e.Descendants("Match").Where(m =>
        //                        m.Descendants("Odd").Any()
        //                       && DateTime.Parse(m.Attribute("StartDate").Value, DateTimeFormatInfo.InvariantInfo) < DateTime.Parse(sport.Parent.Attribute("CreateDate").Value, DateTimeFormatInfo.InvariantInfo).AddDays(1)
        //                       && DateTime.Parse(m.Attribute("StartDate").Value, DateTimeFormatInfo.InvariantInfo) > DateTime.Parse(sport.Parent.Attribute("CreateDate").Value, DateTimeFormatInfo.InvariantInfo))
        //                               .Select(m => new MatchDTO
        //                               {
        //                                   Id = int.Parse(m.Attribute("ID").Value),
        //                                   MatchType = m.Attribute("MatchType").Value,
        //                                   Name = m.Attribute("Name").Value,
        //                                   //Projecting only the Bets that have Odds
        //                                   Bets = m.Descendants("Bet").Where(b => b.Descendants("Odds").Any()).Select(b => new BetDTO
        //                                   {
        //                                       Id = int.Parse(b.Attribute("ID").Value),
        //                                       IsLive = bool.Parse(b.Attribute("IsLive").Value),
        //                                       Name = b.Attribute("Name").Value,
        //                                       Odds = b.Descendants("Odd").Select(o => new OddDTO
        //                                       {
        //                                           Id = int.Parse(o.Attribute("ID").Value),
        //                                           Name = o.Attribute("Name").Value,
        //                                           Value = o.Attribute("Value").Value,
        //                                           SpecialBetValue = o.Attribute("SpecialBetValue") != null ? o.Attribute("SpecialBetValue").Value : null
        //                                       })
        //                                   })
        //                               })
        //                   })
        //               };
        //    }
        //    catch (Exception)
        //    {
        //        //TODO: Add logger
        //        throw;
        //    }
        //}

        public IEnumerable<SportDTO> ParseSports()
        {
            return from sport in this.xdoc.Descendants("Sport")
                   where sport.Descendants("Odd").Any()
                   select new SportDTO
                   {
                       Name = sport.Attribute("Name").Value,
                       XmlId = int.Parse(sport.Attribute("ID").Value)
                   };
        }

        public IEnumerable<EventDTO> ParseEvents()
        {
            return from xmlEvent in this.xdoc.Descendants("Event")
                   where xmlEvent.Descendants("Odd").Any()
                   select new EventDTO
                   {
                       Name = xmlEvent.Attribute("Name").Value,
                       CategoryID = int.Parse(xmlEvent.Attribute("CategoryID").Value),
                       XmlId = int.Parse(xmlEvent.Attribute("ID").Value),
                       ParentXmlId = int.Parse(xmlEvent.Parent.Attribute("ID").Value),
                       IsLive = bool.Parse(xmlEvent.Attribute("IsLive").Value)
                   };
        }

        public IEnumerable<MatchDTO> ParseMathes()
        {
            return from match in this.xdoc.Descendants("Match")
                   where match.Descendants("Odd").Any()
                   && DateTime.Parse(match.Attribute("StartDate").Value, DateTimeFormatInfo.InvariantInfo) < DateTime.Parse(match.Parent.Parent.Parent.Attribute("CreateDate").Value, DateTimeFormatInfo.InvariantInfo).AddDays(1)
                    && DateTime.Parse(match.Attribute("StartDate").Value, DateTimeFormatInfo.InvariantInfo) > DateTime.Parse(match.Parent.Parent.Parent.Attribute("CreateDate").Value, DateTimeFormatInfo.InvariantInfo)
                   select new MatchDTO
                   {
                       Name = match.Attribute("Name").Value,
                       XmlId = int.Parse(match.Attribute("ID").Value),
                       ParentXmlId = int.Parse(match.Parent.Attribute("ID").Value),
                       MatchType = match.Attribute("MatchType").Value,
                       StartTime = DateTime.Parse(match.Attribute("StartDate").Value, DateTimeFormatInfo.InvariantInfo)
                   };
        }

        public IEnumerable<BetDTO> ParseBets()
        {
            return from bet in this.xdoc.Descendants("Bet")
                   where bet.Descendants("Odd").Any()
                   select new BetDTO
                   {
                       Name = bet.Attribute("Name").Value,
                       XmlId = int.Parse(bet.Attribute("ID").Value),
                       ParentXmlId = int.Parse(bet.Parent.Attribute("ID").Value),
                       IsLive = bool.Parse(bet.Attribute("IsLive").Value)
                   };
        }

        public IEnumerable<OddDTO> ParseOdds()
        {
            return from odd in this.xdoc.Descendants("Odd")
                   select new OddDTO
                   {
                       Name = odd.Attribute("Name").Value,
                       XmlId = int.Parse(odd.Attribute("ID").Value),
                       ParentXmlId = int.Parse(odd.Parent.Attribute("ID").Value),
                       Value = odd.Attribute("Value").Value,
                       SpecialBetValue = odd.Attribute("SpecialBetValue") != null ? odd.Attribute("SpecialBetValue").Value : null
                   };
        }
    }
}
