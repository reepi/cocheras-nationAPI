using Data.Entities;

namespace Data.Repositories.ParkingRepository
{
    public interface ISlotsRepository
    {
        List<Slot> Get();
        void Add(Slot slot);
        void Modify(Slot slot);
        void Delete(Slot slot);
    }
}