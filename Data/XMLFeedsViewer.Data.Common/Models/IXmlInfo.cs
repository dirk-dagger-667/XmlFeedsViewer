namespace XMLFeedsViewer.Data.Common.Models
{
    public interface IXmlInfo
    {
        int XmlId { get; set; }

        int ParentXmlId { get; set; }
    }
}
