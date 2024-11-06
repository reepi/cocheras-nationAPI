namespace Common.DTOs.ParkingDTOs
{
    public class ParkingForViewDto
    {
        public string Plate { get; set; }
        public int SlotId { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public decimal? Fee { get; set; }
    }
}
