using EMobility.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMobility.WebApplication.Repositories

{
    public class ChargingPointsRepository : IChargingPointsRepository
    {
        private List<ChargingPoint> ChargingPoints { get; } = new();

        public ChargingPointsRepository()
        {
            CreateData();
        }

        public ChargingPoint Add(ChargingPoint newChargingPoint)
        {
            ChargingPoints.Add(newChargingPoint);
            int nextId = ChargingPoints.Select(cp => cp.Id).Max() + 1;
            newChargingPoint.Id = nextId;
            return newChargingPoint;
        }

        public IEnumerable<ChargingPoint> GetAll() => ChargingPoints;

        public ChargingPoint GetById(int id) => ChargingPoints.FirstOrDefault(cp => cp.Id == id);

        public void Delete(int id)
        {
            var chargingPointToDelete = GetById(id);
            if (chargingPointToDelete == null)
            {
                throw new ArgumentException($"No charging point available with the given id [{id}]", nameof(id));
            }
            ChargingPoints.Remove(chargingPointToDelete);
        }

        private void CreateData()
        {
            ChargingPoints.Add(new(-4, "TG Stellplatz 4", "http://172.16.0.146/rest/", "1384202.00082"));     // Besucherparkplatz TG 4
            ChargingPoints.Add(new(-5, "TG Stellplatz 5", "http://172.16.0.147/rest/", "1384202.00080"));     // Mayer Thomas
            ChargingPoints.Add(new(1, "Stellplatz 1", "http://172.16.0.148:81/rest/", "140812422.00057"));    // Besucherparkplatz 1
            ChargingPoints.Add(new(2, "Stellplatz 2", "http://172.16.0.148:82/rest/", "140812422.00057#2"));  // Besucherparkplatz 2  / slave
            ChargingPoints.Add(new(3, "Stellplatz 3", "http://172.16.0.149:81/rest/", "140612412.00041"));    // Besucherparkplatz 3
            ChargingPoints.Add(new(4, "Stellplatz 4", "http://172.16.0.149:82/rest/", "140612412.00041#2"));  // Besucherparkplatz 4  / slave
        }
    }

}
