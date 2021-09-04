using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmobilityWebApp.Models;
using Refit;

namespace EmobilityWebApp.DataAccess
{
    interface IChargingStations
    {
        [Get("/ChargingPoint")]
        Task<List<ChargerModel>> GetChargingPoints();

        [Get("/ChargingPoint/{id}")]
        Task<ChargerModel> GetChargingPoint(int id);

        [Post("/ChargingPoint")]
        Task CreateChargingPoint([Body] ChargerModel value);

        [Put("/ChargingPoint/{id}")]
        Task UpdateChargingPoint(int id, [Body] ChargerModel model);

        [Delete("/ChargingPoint/{id}")]
        Task DeleteChargingPoint(int id);

    }
}
