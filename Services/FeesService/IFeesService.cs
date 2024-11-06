using Common.DTOs.FeeDTOs;

namespace Services.FeesService
{
    public interface IFeesService
    {
        List<FeeForViewDto> Get();
        FeeForViewDto Modify(FeeForModificationDto feeForModification);
        decimal GetFee(DateTime entryTime, DateTime exitTime);
    }
}
