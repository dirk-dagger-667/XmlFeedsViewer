namespace XMLFeedsViewer.Services.Data
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Contracts;
    using XMLFeedsViewer.Data.Models;
    using XMLFeedsViewer.Data.Contracts;
    
    public class MatchesService : BaseService<Match>, IMatchesService
    {
        public MatchesService(IXMLFeedsViewerData data)
            : base(data)
        {
        }

        public override void EditCollectionForDbFiller(IEnumerable<Match> matches)
        {
            var matchesDtoIds = matches.Select(m => m.XmlId);
            var result = this.data.Matches.SearchFor(m => matchesDtoIds.Contains(m.XmlId) && !m.IsDeleted).ToList();

            foreach (var match in matches)
            {
                var matchForEditing = result.Where(m => m.XmlId == match.XmlId).FirstOrDefault();
                matchForEditing.Name = match.Name;
                matchForEditing.StartTime = match.StartTime;
                matchForEditing.IsDeleted = match.IsDeleted;
                matchForEditing.MatchType = match.MatchType;

                this.data.Matches.Update(matchForEditing);
            }
        }
    }
}
