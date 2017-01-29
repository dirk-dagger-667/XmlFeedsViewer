namespace XMLFeedsViewer.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using XMLFeedsViewer.Data.Models;
    using XMLFeedsViewer.Data.Contracts;

    public class OddsService : BaseService<Odd>, IOddsService
    {
        public OddsService(IXMLFeedsViewerData data)
            : base(data)
        {
        }

        public override void EditCollectionForDbFiller(IEnumerable<Odd> odds)
        {
            var oddsDtoIds = odds.Select(o => o.XmlId);
            var result = this.data.Odds.SearchFor(o => oddsDtoIds.Contains(o.XmlId) && !o.IsDeleted).ToList();

            foreach (var odd in odds)
            {
                var oddForEditing = result.Where(o => o.XmlId == odd.XmlId).FirstOrDefault();
                oddForEditing.Name =odd.Name;
                oddForEditing.Value = odd.Value;
                oddForEditing.SpecialBetValue = odd.SpecialBetValue;
                this.data.Odds.Update(oddForEditing);
            }
        }
    }
}
