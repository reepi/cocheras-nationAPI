using Common.DTOs.SlotDTOs;
using Data.Entities;

namespace Data.Repositories.ParkingRepository
{
    public class SlotsRepository : ISlotsRepository
    {
        private readonly ParkingContext _context;
        public SlotsRepository(ParkingContext context)
        {
            _context = context;
        }

        public List<Slot> Get()
        {
            return _context.Slots.ToList();
        }

        public void Add(Slot slot)
        {
            _context.Slots.Add(slot);
            _context.SaveChanges();
        }

        public void Modify(Slot slot)
        {
            Slot slotAux = _context.Slots.FirstOrDefault(s => s.Id == slot.Id);
            slotAux.IsAvailable = slot.IsAvailable;
            _context.Update(slotAux);
            _context.SaveChanges();
        }

        public void Delete(Slot slot)
        {
            _context.Slots.Remove(slot);
            _context.SaveChanges();
        }
    }
}
