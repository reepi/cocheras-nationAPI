using Common.DTOs.ParkingDTOs;
using Common.DTOs.SlotDTOs;
using Data.Entities;
using Data.Repositories.ParkingRepository;

namespace Services.ParkingService
{
    public class SlotsService : ISlotsService
    {
        private readonly ISlotsRepository _slotsRepository;
        private readonly IParkingService _parkingService;
        public SlotsService(ISlotsRepository slotsRepository, IParkingService parkingService)
        {
            _slotsRepository = slotsRepository;
            _parkingService = parkingService;
        }

        private ParkingForViewDto? GetParking(int slotId)
        {
            return _parkingService.Get().FirstOrDefault(p => p.SlotId == slotId && p.ExitTime == null);
        }

        public List<SlotForViewDto> Get()
        {
            return _slotsRepository
                .Get()
                .Select(s => new SlotForViewDto()
                {
                    Id = s.Id,
                    Description = s.Description,
                    Parking = GetParking(s.Id),
                    IsAvailable = s.IsAvailable
                }).ToList();
        }

        public SlotForViewDto Add(string description)
        {
            Slot slot = new Slot
            {
                Description = description,
                IsAvailable = true
            };
            _slotsRepository.Add(slot);
            return new SlotForViewDto
            {
                Id = slot.Id,
                Description = slot.Description,
                IsAvailable = slot.IsAvailable
            };

        }

        public SlotForViewDto? Modify(int id, SlotForModificationDto slotForModification)
        {
            Slot? slot = _slotsRepository
                .Get()
                .FirstOrDefault(s => s.Id == id);

            if (slot is null)
            {
                return null;
            }

            slot.Description = slotForModification.Description;
            slot.IsAvailable = slotForModification.IsAvailable;
            _slotsRepository.Modify(slot);
            return new SlotForViewDto
            {
                Id = slot.Id,
                Description = slot.Description,
                IsAvailable = slot.IsAvailable
            };
        }

        public bool Delete(int id)
        {
            Slot? slot = _slotsRepository
                .Get()
                .FirstOrDefault(s => s.Id == id);

            if (slot is null)
            {
                return false;
            }

            _slotsRepository.Delete(slot);
            return true;

        }
    }
}
