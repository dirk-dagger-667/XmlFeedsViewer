namespace XMLFeedsViewer.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Common.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Sport : AuditInfo, IDeletableEntity, IXmlInfo
    {
        private ICollection<Event> events;

        public Sport()
        {
            this.events = new HashSet<Event>();
        }

        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Index(IsUnique = true)]
        public int XmlId { get; set; }

        public int ParentXmlId { get; set; }

        public virtual ICollection<Event> Events
        {
            get { return this.events; }
            set { this.events = value; }
        }
    }
}
