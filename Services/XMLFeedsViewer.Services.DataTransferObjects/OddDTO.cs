namespace XMLFeedsViewer.Services.DataTransferObjects
{
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Infrastructure.Mapping;

    public class OddDTO : BaseDTO, IMapFrom<Odd>, IMapTo<Odd>
    {
        [Required]
        public string Value { get; set; }

        [Required]
        public string SpecialBetValue { get; set; }
    }
}
