using Common.DTOs.SlotDTOs;

namespace Services.ParkingService
{
    public interface ISlotsService
    {
        public List<SlotForViewDto> Get();
        public SlotForViewDto Add(string description);
        public SlotForViewDto? Modify(int id, SlotForModificationDto slotForModification);
        public bool Delete(int id);
    }
}