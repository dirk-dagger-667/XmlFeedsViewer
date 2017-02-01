namespace XMLFeedsViewer.Services.DataTransferObjects
{
    using Infrastructure.Mapping;
    using Data.Models;
    using System.ComponentModel.DataAnnotations;
    using Data.Common.Models;
    using System;

    public class SportDTO : IXmlInfo, IMapTo<Sport>, IMapFrom<Sport>
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int XmlId { get; set; }

        public bool IsDeleted { get; set; }

        public int ParentXmlId { get; set; }
    }
}
