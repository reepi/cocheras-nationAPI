using Data.Entities;

namespace Data.Repositories.ParkingRepository
{
    public class ParkingRepository : IParkingRepository
    {
        private readonly ParkingContext _context;

        public ParkingRepository(ParkingContext context)
        {
            _context = context;
        }

        public List<Parking> Get()
        {
            return _context.Parkings.ToList();
        }

        public Parking? Get(string Plate)
        {
            return _context.Parkings.FirstOrDefault(p => p.Plate == Plate && p.ExitTime == null);
        }

        public void Add(Parking parking)
        {
            _context.Parkings.Add(parking);
            _context.SaveChanges();
        }
        public void Modify(Parking parking)
        {
            _context.Update(parking);
            _context.SaveChanges();
        }
    }
}
