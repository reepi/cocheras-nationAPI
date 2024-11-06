using Common.DTOs.ParkingDTOs;

namespace Common.DTOs.SlotDTOs
{
    public class SlotForViewDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ParkingForViewDto? Parking { get; set; }
        public bool IsAvailable { get; set; }
    }
}
