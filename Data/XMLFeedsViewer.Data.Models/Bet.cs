namespace XMLFeedsViewer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Bet : AuditInfo, IDeletableEntity, IXmlInfo
    {
        private ICollection<Odd> odds;

        public Bet()
        {
            this.Odds = new HashSet<Odd>();
        }

        [Key]
        public int Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        public bool IsLive { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public int XmlId { get; set; }

        public int ParentXmlId { get; set; }

        public bool IsDeleted { get; set; }

        public int? MatchId { get; set; }

        [ForeignKey("MatchId")]
        public virtual Match Match { get; set; }

        public virtual ICollection<Odd> Odds
        {
            get { return this.odds; }
            set { this.odds = value; }
        }
    }
}
