namespace XMLFeedsViewer.Services.DataTransferObjects
{
    using Data.Common.Models;
    using System.ComponentModel.DataAnnotations;

    public abstract class BaseDTO : IXmlInfo
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int ParentXmlId { get; set; }

        [Required]
        public int XmlId { get; set; }

        public bool Deleted { get; set; }
    }
}
