using Common.DTOs.ParkingDTOs;
using Data.Entities;
using Data.Repositories.ParkingRepository;
using Services.FeesService;

namespace Services.ParkingService
{
    public class ParkingService : IParkingService
    {
        private readonly IParkingRepository _parkingRepository;
        private readonly IFeesService _feesService;
        private readonly ISlotsRepository _slotsRepository;

        public ParkingService(IParkingRepository parkingRepository, IFeesService feesService , ISlotsRepository slotsRepository)
        {
            _parkingRepository = parkingRepository;
            _feesService = feesService;
            _slotsRepository = slotsRepository;
        }

        public List<ParkingForViewDto> Get()
        {
            List<Parking> parkings = _parkingRepository.Get();
            return parkings.Select(parking => new ParkingForViewDto
            {
                Plate = parking.Plate,
                SlotId = parking.SlotId,
                EntryTime = parking.EntryTime,
                ExitTime = parking.ExitTime,
                Fee = parking.Fee
            }).ToList();
        }

        public List<ReportDto> GetReports()
        {
            List<Parking> parkings = _parkingRepository.Get();
            int month;
            ReportDto[] parkingsForReport = new ReportDto[12];

            for (int i = 0; i < 12; i++)
            {
                parkingsForReport[i] = new ReportDto
                {
                    Month = i + 1,
                    ParkingCount = 0,
                    TotalValue = 0
                };
            }

            foreach (Parking parking in parkings)
            {
                if (parking.ExitTime is null) continue;
                month = parking.EntryTime.Month;
                parkingsForReport[month - 1].ParkingCount += 1;
                parkingsForReport[month - 1].TotalValue += parking.Fee;
            }

            return parkingsForReport.ToList();
        }

        public void Add(ParkingForAddDto parkingForAdd)
        {
            _parkingRepository.Add(new Parking
            {
                SlotId = parkingForAdd.SlotId,
                Plate = parkingForAdd.Plate,
                EntryTime = DateTime.UtcNow,
                ExitTime = null,
                Fee = null
            });

            Slot? slot = _slotsRepository.Get().FirstOrDefault(s => s.Id == parkingForAdd.SlotId);
            _slotsRepository.Modify(new Slot
            {
                Id = slot.Id,
                Description = slot.Description,
                IsAvailable = false
            });
        }

        public ParkingForViewDto? Close(string plate)
        {
            Parking? parking = _parkingRepository.Get(plate);
            if (parking is null)
            {
                return null;
            }

            DateTime exitTime = DateTime.UtcNow;
            parking.ExitTime = exitTime;
            parking.Fee = _feesService.GetFee(parking.EntryTime, exitTime);

            _parkingRepository.Modify(parking);
            Slot? slot = _slotsRepository.Get().FirstOrDefault(s => s.Id == parking.SlotId);
            _slotsRepository.Modify(new Slot
            {
                Id = slot.Id,
                Description = slot.Description,
                IsAvailable = true
            });

            return new ParkingForViewDto
            {
                Plate = parking.Plate,
                SlotId = parking.SlotId,
                EntryTime = parking.EntryTime,
                ExitTime = parking.ExitTime,
                Fee = parking.Fee
            };
        }
    }
}