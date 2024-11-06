using Common.Models;

namespace Common.DTOs.FeeDTOs
{
    public class FeeForViewDto
    {
        public FeeTypeEnum Type { get; set; }
        public int Value { get; set; }
    }
}
