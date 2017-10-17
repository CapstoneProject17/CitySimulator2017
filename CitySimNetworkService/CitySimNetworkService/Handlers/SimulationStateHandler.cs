using System;
using System.Json;

namespace CitySimNetworkService
{
    internal class SimulationStateHandler
    {

        SimulationStateQueue partialUpdateBuffer;
        SimulationStateQueue fullUpdateBuffer;

        public SimulationStateHandler(SimulationStateQueue _partialBuffer, SimulationStateQueue _fullBuffer)
        {
            partialUpdateBuffer = _partialBuffer;
            fullUpdateBuffer = _fullBuffer;
        }

        internal JsonObject PartialUpdateRequestHandler(PartialSimulationUpdateRequest request)
        {
            int updateId = request.LastUpdate + 1;
            return partialUpdateBuffer.GetPartialStateByID(updateId);
        }

        internal JsonObject FullUpdateRequestHandler(SimulationUpdateRequest request)
        {
            return fullUpdateBuffer.Peek();
        }

        internal JsonObject HandleUpdateRequest(SimulationUpdateRequest request)
        {
            if (request.FullUpdate)
            {
                return FullUpdateRequestHandler(request);
            }
            else
            {
                return PartialUpdateRequestHandler((PartialSimulationUpdateRequest) request);
            }
        }
    }
}