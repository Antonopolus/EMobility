using System.Collections.Generic;
using static EMobility.WebApplication.Services.ChargingPointsRepository;

namespace EMobility.WebApplication.Services
{
    public interface IChargingPointsRepository
    {
        ChargingPointMod Add(ChargingPointMod newChargingPoint);

        void Delete(int id);
        
        ChargingPointMod GetById(int id);
        
        IEnumerable<ChargingPointMod> GetAll();
    }
}