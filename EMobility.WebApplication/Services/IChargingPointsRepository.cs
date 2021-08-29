using System.Collections.Generic;
using static EMobility.WebApplication.Services.ChargingPointsRepository;

namespace EMobility.WebApplication.Services
{
    public interface IChargingPointsRepository
    {
        ChargingPoint Add(ChargingPoint newChargingPoint);

        void Delete(int id);

        ChargingPoint GetById(int id);
        
        IEnumerable<ChargingPoint> GetAll();
    }
}