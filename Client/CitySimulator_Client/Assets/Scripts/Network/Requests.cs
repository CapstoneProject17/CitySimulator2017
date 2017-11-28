using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class BaseRequest
{
    public string RequestType { get; set; }
}

/// <summary>
/// Contains update type and update measure (full or partial).
/// </summary>
public class SimulationUpdateRequest : BaseRequest
{
    public bool FullUpdate { get; set; }
}

/// <summary>
/// Contains last update information.
/// </summary>
public class PartialSimulationUpdateRequest : SimulationUpdateRequest
{
    public int LastUpdate { get; set; }
}

/// <summary>
/// Contains resource type and resource id.
/// </summary>
public class DatabaseResourceRequest : BaseRequest
{
    public string ResourceType { get; set; }
    public string ResourceID { get; set; }
}