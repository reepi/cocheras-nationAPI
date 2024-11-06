using Data.Entities;

namespace Data.Repositories.FeesRepository
{
    public class FeesRepository : IFeesRepository
    {
        private readonly ParkingContext _context;
        public FeesRepository(ParkingContext context)
        {
            _context = context;
        }

        public List<Fee> Get()
        {
            return _context.Fees.ToList();
        }

        public void Modify(Fee fee)
        {
            _context.Update(fee);
            _context.SaveChanges();
        }
    }
}
