using EMobility.Data;
using System.Collections.Generic;

namespace EMobility.WebApplication.Repositories
{

    public interface IChargingPointsRepository
    {
        IEnumerable<ChargingPoint> GetAll();

        ChargingPoint GetById(int id);

        ChargingPoint Add(ChargingPoint newChargingPoint);

        void Delete(int id);
    }
}