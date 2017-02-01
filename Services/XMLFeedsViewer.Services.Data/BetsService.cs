namespace XMLFeedsViewer.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using XMLFeedsViewer.Data.Contracts;
    using XMLFeedsViewer.Data.Models;

    public class BetsService : BaseService<Bet>, IBetsService
    {
        public BetsService(IXMLFeedsViewerData data) 
            : base(data)
        {
        }

        public override void EditCollectionForDbFiller(IEnumerable<Bet> bets)
        {
            var betsDtoIds = bets.Select(b => b.XmlId);
            var result = this.data.Bets.SearchFor(b => betsDtoIds.Contains(b.XmlId) && !b.IsDeleted).ToList();

            foreach (var bet in bets)
            {
                var betForEditing = result.Where(b => b.XmlId == bet.XmlId).FirstOrDefault();
                betForEditing.Name = bet.Name;
                betForEditing.IsLive = bet.IsLive;
                this.data.Bets.Update(betForEditing);
            }
        }
    }
}
