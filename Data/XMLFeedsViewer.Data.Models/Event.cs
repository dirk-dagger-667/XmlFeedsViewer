namespace XMLFeedsViewer.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Common.Models;

    public class Event : AuditInfo, IDeletableEntity, IXmlInfo
    {
        private ICollection<Match> matches;

        public Event()
        {
            this.matches = new HashSet<Match>();
        }

        [Key]
        public int Id { get; set; }

        public int CategoryId { get; set; }

        public string Name { get; set; }

        public bool IsLive { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public int XmlId { get; set; }

        public int ParentXmlId { get; set; }

        public bool IsDeleted { get; set; }

        public int? SportId { get; set; }

        [ForeignKey("SportId")]
        public virtual Sport Sport { get; set; }

        public virtual ICollection<Match> Matches
        {
            get { return this.matches; }
            set { this.matches = value; }
        }
    }
}
