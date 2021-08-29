using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMobility.WebApi.Services
{
    public class ChargingSessionRepository : IChargingSessionRepository
    {
        private List<ChargingSession> ChargingSessions { get; } = new();

        public ChargingSessionRepository()
        {
            CreateData();
        }

        public ChargingSession Add(ChargingSession newChargingSessionModel)
        {
            ChargingSessions.Add(newChargingSessionModel);
            return newChargingSessionModel;
        }

        public IEnumerable<ChargingSession> GetAll() => ChargingSessions;

        public ChargingSession GetById(int id) => ChargingSessions.FirstOrDefault(session => session.Id == id);

        public void Delete(int id)
        {
            var ChargingSessionToDelete = GetById(id);
            if (ChargingSessionToDelete == null)
            {
                throw new ArgumentException("No charging session available with the given id", nameof(id));
            }
            ChargingSessions.Remove(ChargingSessionToDelete);
        }

        ChargingSession IChargingSessionRepository.Update(int id, ChargingSession value)
        {
            var toUpdate = ChargingSessions.FirstOrDefault(cs => cs.Id == id);
            if (toUpdate != null)
            {
                ChargingSessions.Remove(toUpdate);
                ChargingSessions.Add(value);
            }
            return value;
        }

        private void CreateData()
        {
            for (int i = 1; i < 10; i++)
            {
                ChargingSession session = new()
                {
                    SessionId = i,
                    StartDate = DateTime.UtcNow,
                    LocalStartTime = TimeSpan.FromSeconds(i + 10),
                    DurationOf = TimeSpan.FromSeconds(i + 10),
                    Energy = 3,
                    RfidTag = "RFID",
                    ChargingStation = "Station"
                };
                ChargingSessions.Add(session);
            }
        }
    }
}
