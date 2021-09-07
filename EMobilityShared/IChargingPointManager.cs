using System.Threading;
using System.Threading.Tasks;

namespace EMobility
{
    public interface IChargingPointManager
    {
        void CheckVehicleConnectionStates(CancellationToken cancelationToken);
        Task CheckVehicleConnectionStatesAsync(CancellationToken cancelationToken);
    }
}