using EMobility.Data;
using EMobility.WebApi.Dtos;
using EMobility.WebApi.Profiles;
using EMobility.WebApplication.Repositories;
using EMobility.WebApplication.Services;
using System.Collections.Generic;

namespace EMobility.WebApplication
{
    internal class ChargingPointsService : IChargingPointsService
    {
        private readonly IChargingPointsRepository repository;

        public ChargingPointsService(IChargingPointsRepository repository)
        {
            this.repository = repository;
        }

        public ChargingPoint Add(ChargingPoint newChargingPoint) => repository.Add(newChargingPoint);

        public void Delete(int id) => repository.Delete(id);

        public IEnumerable<ChargingPoint> GetAll() => repository.GetAll();

        public ChargingPoint GetById(int id) => repository.GetById(id);
        
    }
}