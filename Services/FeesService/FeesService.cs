using Common.DTOs.FeeDTOs;
using Data.Entities;
using Data.Repositories.FeesRepository;

namespace Services.FeesService
{
    public class FeesService : IFeesService
    {
        private readonly IFeesRepository _feesRepository;
        public FeesService(IFeesRepository feesRepository)
        {
            _feesRepository = feesRepository;
        }

        public List<FeeForViewDto> Get()
        {
            return _feesRepository
                .Get()
                .Select(fee => new FeeForViewDto
            {
                Type = fee.Type,
                Value = fee.Value
            }).ToList();
        }

        public FeeForViewDto Modify(FeeForModificationDto feeForModification)
        {
            Fee fee = new Fee
            {
                Type = feeForModification.Type,
                Value = feeForModification.Value
            };
            _feesRepository.Modify(fee);

            return new FeeForViewDto
            {
                Type = fee.Type,
                Value = fee.Value
            };
        }

        public decimal GetFee(DateTime entryTime, DateTime exitTime)
        {
            uint totalMinutes = (uint)Math.Round((exitTime - entryTime).TotalMinutes);
            uint days = totalMinutes / 60 / 24;
            uint hours = totalMinutes / 60 % 24;
            uint minutes = totalMinutes % 60;

            if (minutes > 30)
            {
                minutes = 0;
                hours += 1;
                if (hours == 24)
                {
                    hours = 0;
                    days += 1;
                }
            }
            else
            {
                minutes = 1;
            }

            List<Fee> fees = _feesRepository.Get();

            return days * fees[2].Value + hours * fees[1].Value + minutes * fees[0].Value;
;
        }

    }
}
