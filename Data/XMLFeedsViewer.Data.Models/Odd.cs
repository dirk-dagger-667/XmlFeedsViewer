namespace XMLFeedsViewer.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Odd : AuditInfo, IDeletableEntity, IXmlInfo
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }
        
        public string SpecialBetValue { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public int XmlId { get; set; }

        public int ParentXmlId { get; set; }

        public int? BetId { get; set; }

        [ForeignKey("BetId")]
        public virtual Bet Bet { get; set; }
    }
}
