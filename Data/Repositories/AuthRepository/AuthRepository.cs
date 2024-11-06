using Data.Entities;

namespace Data.Repositories.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly ParkingContext _context;

        public AuthRepository(ParkingContext context)
        {
            _context = context;
        }

        public User? Get(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
    }
}