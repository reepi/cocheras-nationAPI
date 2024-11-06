using Data.Entities;

namespace Data.Repositories.ParkingRepository
{
    public interface IParkingRepository
    {
        List<Parking> Get();
        Parking? Get(string Plate);
        void Add(Parking parking);
        void Modify(Parking parking);
    }
}
