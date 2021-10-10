using EMobility.Data;
using System.Collections.Generic;
using static EMobility.WebApi.Services.ChargingSessionRepository;

namespace EMobility.WebApi.Services
{
    public interface IChargingSessionRepository
    {
        
        IEnumerable<ChargingSession> GetAll();

        ChargingSession GetById(int id);

        ChargingSession Add(ChargingSession newChargingSessionModel);

        ChargingSession Update(int id, ChargingSession value);

        void Delete(int id);

    }
}