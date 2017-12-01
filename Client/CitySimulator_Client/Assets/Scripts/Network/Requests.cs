using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/// /// <summary>
/// Team: Network
/// Description: request will be an object type everytime communication between server and client
/// Author:
///  Name: Harman Mahal    	Date: 2017-11-28
///  Name: Gisu Kim    		Date: 2017-11-28
///  Name: N/A   Change: N/A         Date: N/A
/// Based on:  N/A
/// </summary>


/// <summary>
/// Contains update type.
/// </summary>
[Serializable]
public class BaseRequest
{
    public string RequestType;

    public BaseRequest(string requestType)
    {
        RequestType = requestType;
    }
}

/// <summary>
/// Contains update type and update measure (full or partial).
/// </summary>
[Serializable]
public class SimulationUpdateRequest : BaseRequest
{
    public bool FullUpdate;

    public SimulationUpdateRequest(string requestType, bool fullUpdate): base(requestType)
    {
        FullUpdate = fullUpdate;
    }
}

/// <summary>
/// Contains last update information.
/// </summary>
[Serializable]
public class PartialSimulationUpdateRequest : SimulationUpdateRequest
{
    public int LastUpdate; 

    public PartialSimulationUpdateRequest(string requestType, bool fullUpdate, int lastUpdate): base(requestType, fullUpdate)
    {
        LastUpdate = lastUpdate;
    }
}

/// <summary>
/// Contains resource type and resource id.
/// </summary>
[Serializable]
public class DatabaseResourceRequest : BaseRequest
{
    public string ResourceType;
    public string ResourceID;

    public DatabaseResourceRequest(string requestType, string resourceType, string resourceId): base(requestType)
    {
        ResourceType = resourceType;
        ResourceID = resourceId;
    }
}