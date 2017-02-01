namespace XmlFeedsViewer.XmlParser.DataTransferObjects
{
    using System.ComponentModel.DataAnnotations;

    public class BaseDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
