using Data.Entities;

namespace Data.Repositories.FeesRepository
{
    public interface IFeesRepository
    {
        List<Fee> Get();
        void Modify(Fee fee);
    }
}
