using EMobility.Data;
using EMobility.WebApi.Dtos;
using System.Collections.Generic;

namespace EMobility.WebApplication.Services
{
    public interface IChargingPointsService
    {
        IEnumerable<ChargingPoint> GetAll();

        ChargingPoint GetById(int id);

        ChargingPoint Add(ChargingPoint newChargingPoint);

        void Delete(int id);
    }
}