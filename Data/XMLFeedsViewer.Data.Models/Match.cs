namespace XMLFeedsViewer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Match : AuditInfo, IDeletableEntity, IXmlInfo
    {
        private ICollection<Bet> bets;

        public Match()
        {
            this.bets = new HashSet<Bet>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string MatchType { get; set; }

        public DateTime StartTime { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public int XmlId { get; set; }

        public int ParentXmlId { get; set; }

        public int? EventId { get; set; }

        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }
        
        public  virtual ICollection<Bet> Bets
        {
            get { return this.bets; }
            set { this.bets = value; }
        }
    }
}
