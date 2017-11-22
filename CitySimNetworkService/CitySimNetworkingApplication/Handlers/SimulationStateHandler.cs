using System;

namespace CitySimNetworkService
{
    /// <summary>
    /// Contains handlers for simulation matters.
    /// </summary>
    /// 
    /// <author>
    /// Harman Mahal
    /// </author>
    internal class SimulationStateHandler
    {
        SimulationStateQueue partialUpdateBuffer;
        SimulationStateQueue fullUpdateBuffer;

        /// <summary>
        /// Specifies application buffers.
        /// </summary>
        /// 
        /// <param name="_partialBuffer">
        /// Partial request buffer to set.
        /// </param>
        /// 
        /// <param name="_fullBuffer">
        /// Full request buffer to set.
        /// </param>
        public SimulationStateHandler(SimulationStateQueue _partialBuffer, SimulationStateQueue _fullBuffer)
        {
            partialUpdateBuffer = _partialBuffer;
            fullUpdateBuffer = _fullBuffer;
        }

        /// <summary>
        /// Tracks update per request.
        /// </summary>
        /// 
        /// <param name="request">
        /// Contains last update information.
        /// </param>
        /// 
        /// <returns> 
        /// JSON object.
        /// </returns>
        internal string PartialUpdateRequestHandler(PartialSimulationUpdateRequest request)
        {
            int updateId = request.LastUpdate + 1;
            return partialUpdateBuffer.GetPartialStateByID(updateId);
        }

        /// <summary>
        /// Handler for full update request.
        /// </summary>
        /// 
        /// <param name="request">
        /// Contains update type and update measure (full or partial).
        /// </param>
        /// 
        /// <returns>
        /// JSON object.
        /// </returns>
        internal string FullUpdateRequestHandler(SimulationUpdateRequest request)
        {
            return fullUpdateBuffer.Peek();
        }

        /// <summary>
        /// Filters request to proper handler.
        /// </summary>
        /// 
        /// <param name="request">
        /// Contains update type and update measure (full or partial).
        /// </param>
        /// 
        /// <returns>
        /// JSON object from a handler.
        /// </returns>
        internal string HandleUpdateRequest(SimulationUpdateRequest request)
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