using Common.Models;

namespace Common.DTOs.FeeDTOs
{
    public class FeeForModificationDto
    {
        public FeeTypeEnum Type { get; set; }
        public int Value { get; set; }
    }
}
