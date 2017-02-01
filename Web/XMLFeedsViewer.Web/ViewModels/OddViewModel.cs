namespace XMLFeedsViewer.Web.ViewModels
{
    using XMLFeedsViewer.Infrastructure.Mapping;
    using Services.DataTransferObjects;

    public class OddViewModel: IMapFrom<OddDTO>
    {
        public string Name { get; set; }

        public string Value { get; set; }

        public string SpecialBetValue { get; set; }
    }
}