namespace XmlFeedsViewer.XmlParser
{
    //TODO: change application type from console to class library or extract all logic to the business service
    public class Program
    {
        static void Main(string[] args)
        {
            var parser = new XmlParser("http://vitalbet.net/sportxml");

            var allData = parser.ParseAllData();
        }
    }
}
