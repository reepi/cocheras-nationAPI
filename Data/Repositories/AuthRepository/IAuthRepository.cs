using Data.Entities;

namespace Data.Repositories.AuthRepository
{
    public interface IAuthRepository
    {
        User? Get(string username);
        void Add(User user);
    }
}
