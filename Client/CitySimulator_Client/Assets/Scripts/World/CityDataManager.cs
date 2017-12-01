using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Module: Point
/// Team: Client
/// Description: Coordinate Class model for the city data from the server
/// Author:
///  Name: Dongwon(Shawn) Kim    Date: 2017-11-25
/// Modified by:    
///  Name: N/A   Change: N/A         Date: N/A
/// Based on:  N/A
/// </summary>

[Serializable]
public class Point
{
    // x coordinate 
    public int x;

    // z coordinate
    public int z;
}

/// <summary>
/// Module: NewBuilding
/// Team: Client
/// Description: Building Class model for the city data from the server
/// Author:
///  Name: Dongwon(Shawn) Kim    Date: 2017-11-25
/// Modified by:    
///  Name: N/A   Change: N/A         Date: N/A
/// Based on:  N/A
/// </summary>
[Serializable]
public class NewBuilding
{
    // rate of the building
    public int Rating;

    // rate of the building
    public int Capcity;

    // I don't know what is this, not using on the client
    // but need to be here for deserialization
    public bool IsTall;

    // name of the building
    public string Name;

    // location of the building
    public Point Point;

    // type of building
    public string Type;

    // unique id for the building
    public string id;
}

/// <summary>
/// Module: CityData
/// Team: Client
/// Description: Class model for the people data from the server
/// Author: Harman Mahal Date: 2017-11-28
/// Modified by: N/A
/// Based on: N/A
/// </summary>
[Serializable]
public class PersonTravel
{
    //unique id for the person
    public string Id;

    //Starting point of the person
    public Point Origin;

    //Destination point of the person
    public Point Destination;
}

/// <summary>
/// Module: CityData
/// Team: Client
/// Description: Class model for the city data from the server
/// Author:
///  Name: Dongwon(Shawn) Kim    Date: 2017-11-25
/// Modified by:    
///  Name: N/A   Change: N/A         Date: N/A
/// Based on:  N/A
/// </summary>
[Serializable]
public class CityData
{
    // length of the grid for the city
    public int GridLength;

    // width of the grid for the city
    public int GridWidth;

    // hour for the city
    public int NetHours;

    // array of the points that has road
    public Point[] NewRoads;

    // array of buildings
    public NewBuilding[] NewBuildings;

    // array of people
    public PersonTravel[] PeopleMoving;

}

/// <summary>
/// Module: CityDataManager
/// Team: Client
/// Description: Handle the all the data of the city, for now it has been used
/// Author:
///	 Name: Dongwon(Shawn) Kim    Date: 2017-09-11
/// Modified by:	
///	 Name: Dongwon(Shawn) Kim   Change:	Start to use		 									Date: 2017-10-17
///	 Name: Dongwon(Shawn) Kim   Change:	bug fix		 		 									Date: 2017-10-18
///  Name: Lancelei Herradura	Change: Adding walkable path 									Date: 2017-10-31
///	 Name: Dongwon(Shawn) Kim   Change:	gridforTest	 		 									Date: 2017-11-13
///  Name Lancelei Herradura	Change: Add human dictionary 									Date: 2017-11-25
///  Name Harman Mahal			Change: integrate network connection and reqeust enable 		Date: 2017-11-28
///  Name Gisu Kim				Change: integrate network connection and reqeust enable 		Date: 2017-11-28
/// Based on:  
/// https://docs.unity3d.com/Manual/JSONSerialization.html
/// https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
/// </summary>
public class CityDataManager : MonoBehaviour
{
	// switch for testing
    // JSON dummy String for testing
    private static string jsonString = "{\"GridLength\":50,\"GridWidth\":49,\"NetHours\":0,\"NewRoads\":[{\"x\":24,\"z\":21},{\"x\":24,\"z\":28},{\"x\":25,\"z\":21},{\"x\":25,\"z\":28},{\"x\":26,\"z\":21},{\"x\":26,\"z\":28},{\"x\":27,\"z\":21},{\"x\":27,\"z\":28},{\"x\":24,\"z\":22},{\"x\":27,\"z\":22},{\"x\":24,\"z\":23},{\"x\":27,\"z\":23},{\"x\":24,\"z\":24},{\"x\":27,\"z\":24},{\"x\":24,\"z\":25},{\"x\":27,\"z\":25},{\"x\":24,\"z\":26},{\"x\":27,\"z\":26},{\"x\":24,\"z\":27},{\"x\":27,\"z\":27},{\"x\":21,\"z\":14},{\"x\":21,\"z\":21},{\"x\":22,\"z\":14},{\"x\":22,\"z\":21},{\"x\":23,\"z\":14},{\"x\":23,\"z\":21},{\"x\":24,\"z\":14},{\"x\":21,\"z\":15},{\"x\":24,\"z\":15},{\"x\":21,\"z\":16},{\"x\":24,\"z\":16},{\"x\":21,\"z\":17},{\"x\":24,\"z\":17},{\"x\":21,\"z\":18},{\"x\":24,\"z\":18},{\"x\":21,\"z\":19},{\"x\":24,\"z\":19},{\"x\":21,\"z\":20},{\"x\":24,\"z\":20},{\"x\":21,\"z\":7},{\"x\":22,\"z\":7},{\"x\":23,\"z\":7},{\"x\":24,\"z\":7},{\"x\":21,\"z\":8},{\"x\":24,\"z\":8},{\"x\":21,\"z\":9},{\"x\":24,\"z\":9},{\"x\":21,\"z\":10},{\"x\":24,\"z\":10},{\"x\":21,\"z\":11},{\"x\":24,\"z\":11},{\"x\":21,\"z\":12},{\"x\":24,\"z\":12},{\"x\":21,\"z\":13},{\"x\":24,\"z\":13}],\"NewBuildings\":[{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Mohr - Cole\",\"Point\":{\"x\":26,\"z\":22},\"Type\":\"I\",\"id\":\"bf6057e8-01f5-4527-8267-b646e2a26549\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Schmitt, Anderson and Ankunding\",\"Point\":{\"x\":22,\"z\":15},\"Type\":\"C\",\"id\":\"fded6fc8-c17a-461d-a3bf-ff7c5d3dd7dd\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Residence\",\"Point\":{\"x\":23,\"z\":11},\"Type\":\"H\",\"id\":\"eb787b72-09cb-4449-af75-becce92431a3\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Residence\",\"Point\":{\"x\":27,\"z\":14},\"Type\":\"H\",\"id\":\"cbd444ed-8478-4bb3-b24a-28d8d5aa97ed\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Haag, Beahan and Schmidt\",\"Point\":{\"x\":27,\"z\":28},\"Type\":\"C\",\"id\":\"355acd8d-37e7-4dca-ac85-5a368394126d\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Legros - Nikolaus\",\"Point\":{\"x\":24,\"z\":26},\"Type\":\"I\",\"id\":\"90d5132d-8181-437b-97ea-c3b2c93eeff0\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Residence\",\"Point\":{\"x\":30,\"z\":18},\"Type\":\"H\",\"id\":\"6172a91b-7fe5-4bb0-9335-d9dd3c6a3cab\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Greenfelder and Sons\",\"Point\":{\"x\":29,\"z\":27},\"Type\":\"C\",\"id\":\"8f11a6a4-b4c8-4707-b54b-0101be8f8476\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Reichert - Nolan\",\"Point\":{\"x\":27,\"z\":24},\"Type\":\"I\",\"id\":\"bf0eb97e-b38d-42f0-b5c8-dd89b2daca1f\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Residence\",\"Point\":{\"x\":28,\"z\":19},\"Type\":\"H\",\"id\":\"7d3b5b35-257c-4a44-96e1-bbf7ee604559\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Yundt, Collier and Vandervort\",\"Point\":{\"x\":29,\"z\":22},\"Type\":\"C\",\"id\":\"2dc6ec06-7623-43e3-bb32-16d3abc16495\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Jacobi, Rutherford and McCullough\",\"Point\":{\"x\":27,\"z\":28},\"Type\":\"I\",\"id\":\"b2f37a5e-f899-4f4a-9fdd-e8d022f173c5\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Residence\",\"Point\":{\"x\":30,\"z\":21},\"Type\":\"H\",\"id\":\"731ae43c-7eba-46a4-8a05-d5877a517f60\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Heaney LLC\",\"Point\":{\"x\":29,\"z\":24},\"Type\":\"C\",\"id\":\"03504da5-1ec8-4623-857f-43b60a5db873\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Heaney - Bogan\",\"Point\":{\"x\":25,\"z\":22},\"Type\":\"I\",\"id\":\"d7a2382b-9dd6-486f-821d-43825fa5c8c5\"}],\"PeopleMoving\":[{\"Id\":\"0fc0e5db-024a-417f-9ef2-394dbe637949\",\"Origin\":{\"x\":29,\"z\":18},\"Destination\":{\"x\":26,\"z\":22}},{\"Id\":\"4eafb343-70a1-4051-a4b1-6767de1d1303\",\"Origin\":{\"x\":26,\"z\":22},\"Destination\":{\"x\":29,\"z\":18}},{\"Id\":\"ab137598-309e-4277-82f4-a5f190c117ee\",\"Origin\":{\"x\":29,\"z\":18},\"Destination\":{\"x\":26,\"z\":22}},{\"Id\":\"c9eaf622-de1c-47bf-9b60-2bffa5f0e6d1\",\"Origin\":{\"x\":22,\"z\":15},\"Destination\":{\"x\":29,\"z\":18}},{\"Id\":\"5ab5bffc-f91c-49c0-921c-106d537f89dd\",\"Origin\":{\"x\":22,\"z\":15},\"Destination\":{\"x\":29,\"z\":18}},{\"Id\":\"a0e92f5a-5d52-4d59-8add-897115758b58\",\"Origin\":{\"x\":22,\"z\":15},\"Destination\":{\"x\":29,\"z\":18}},{\"Id\":\"d72ed02f-7047-4dbe-b62c-b3fbd4e90dea\",\"Origin\":{\"x\":22,\"z\":15},\"Destination\":{\"x\":29,\"z\":18}},{\"Id\":\"19a1d838-1575-4396-8741-31e1c351c7fd\",\"Origin\":{\"x\":29,\"z\":18},\"Destination\":{\"x\":26,\"z\":22}},{\"Id\":\"7fb3c484-8c19-4ae1-9aa7-378d2e36114f\",\"Origin\":{\"x\":29,\"z\":18},\"Destination\":{\"x\":26,\"z\":22}},{\"Id\":\"3414daf1-0e52-4120-9615-93ceece6cc27\",\"Origin\":{\"x\":29,\"z\":18},\"Destination\":{\"x\":22,\"z\":15}},{\"Id\":\"1452ef64-c61d-49ee-88b5-af9db9dd6b20\",\"Origin\":{\"x\":22,\"z\":15},\"Destination\":{\"x\":29,\"z\":18}},{\"Id\":\"9fd1c6c5-d5e4-47f0-bbc3-9c00e9e7b12a\",\"Origin\":{\"x\":27,\"z\":14},\"Destination\":{\"x\":27,\"z\":28}},{\"Id\":\"9651aefd-e5c0-48b6-b372-1da02adcdd40\",\"Origin\":{\"x\":27,\"z\":28},\"Destination\":{\"x\":27,\"z\":14}},{\"Id\":\"ad00871c-e04a-4c6a-b5ef-0158d070ec15\",\"Origin\":{\"x\":24,\"z\":26},\"Destination\":{\"x\":27,\"z\":14}},{\"Id\":\"6b973e86-4388-47fc-b5f4-ba60e83260bd\",\"Origin\":{\"x\":30,\"z\":18},\"Destination\":{\"x\":29,\"z\":27}},{\"Id\":\"a036659b-25b4-444a-b651-713a4f5fb841\",\"Origin\":{\"x\":30,\"z\":18},\"Destination\":{\"x\":29,\"z\":27}},{\"Id\":\"1bb12952-6c52-4ba1-87ae-bbfcc058dc40\",\"Origin\":{\"x\":30,\"z\":18},\"Destination\":{\"x\":27,\"z\":24}},{\"Id\":\"8783c972-0a90-409f-9588-663ed1e7696d\",\"Origin\":{\"x\":30,\"z\":18},\"Destination\":{\"x\":27,\"z\":24}},{\"Id\":\"1c558b2b-3de4-4d9c-acfb-273a17cae0ce\",\"Origin\":{\"x\":29,\"z\":27},\"Destination\":{\"x\":30,\"z\":18}},{\"Id\":\"546cdd01-d5dd-4b84-af2b-b61e8401de99\",\"Origin\":{\"x\":30,\"z\":18},\"Destination\":{\"x\":29,\"z\":27}},{\"Id\":\"0bd3bcd2-3242-4d01-916e-c9d6b6f027b8\",\"Origin\":{\"x\":29,\"z\":27},\"Destination\":{\"x\":30,\"z\":18}},{\"Id\":\"0dba873c-bef7-4b9a-8581-82671485e193\",\"Origin\":{\"x\":29,\"z\":27},\"Destination\":{\"x\":30,\"z\":18}},{\"Id\":\"3fba05e1-233b-4d6c-b5e6-b5af578175a9\",\"Origin\":{\"x\":27,\"z\":24},\"Destination\":{\"x\":30,\"z\":18}},{\"Id\":\"b399dbec-421f-49c0-9956-3bf30c2135da\",\"Origin\":{\"x\":27,\"z\":24},\"Destination\":{\"x\":30,\"z\":18}},{\"Id\":\"9195ce84-d895-4621-bd9a-ddabce07049c\",\"Origin\":{\"x\":27,\"z\":28},\"Destination\":{\"x\":28,\"z\":19}},{\"Id\":\"0ea63f25-a95e-4016-9757-25ff2a565d4f\",\"Origin\":{\"x\":28,\"z\":19},\"Destination\":{\"x\":29,\"z\":22}},{\"Id\":\"f0c1a9b2-3e3f-4096-9c9b-605cd58bee9d\",\"Origin\":{\"x\":28,\"z\":19},\"Destination\":{\"x\":27,\"z\":28}},{\"Id\":\"b781537e-7649-4444-ac12-f8898f656a1d\",\"Origin\":{\"x\":27,\"z\":28},\"Destination\":{\"x\":28,\"z\":19}},{\"Id\":\"711d26a8-fb1d-4e3a-ad72-05625337e319\",\"Origin\":{\"x\":27,\"z\":28},\"Destination\":{\"x\":28,\"z\":19}},{\"Id\":\"58bac41a-de7b-4cf8-b94d-86af8bda1133\",\"Origin\":{\"x\":29,\"z\":22},\"Destination\":{\"x\":28,\"z\":19}},{\"Id\":\"69a4c645-f2d1-4e79-99c6-21078ba221e4\",\"Origin\":{\"x\":27,\"z\":28},\"Destination\":{\"x\":28,\"z\":19}},{\"Id\":\"3bdfb487-e369-4658-9a33-ac57919ead83\",\"Origin\":{\"x\":28,\"z\":19},\"Destination\":{\"x\":29,\"z\":22}},{\"Id\":\"b76e6223-c03e-42e5-90a9-a44b09b47286\",\"Origin\":{\"x\":27,\"z\":28},\"Destination\":{\"x\":28,\"z\":19}},{\"Id\":\"cae2de95-55c6-4914-accf-4ad91a913fb5\",\"Origin\":{\"x\":28,\"z\":19},\"Destination\":{\"x\":29,\"z\":22}},{\"Id\":\"4a25eb94-6646-43eb-aa91-eef13f697b41\",\"Origin\":{\"x\":28,\"z\":19},\"Destination\":{\"x\":27,\"z\":28}},{\"Id\":\"407c1bdc-b5da-4504-8e73-2df1b18a0165\",\"Origin\":{\"x\":28,\"z\":19},\"Destination\":{\"x\":27,\"z\":28}},{\"Id\":\"d324e77a-9c0f-4214-9734-0ed6e382c393\",\"Origin\":{\"x\":28,\"z\":19},\"Destination\":{\"x\":27,\"z\":28}},{\"Id\":\"8e7bd824-9f04-443b-87c9-1c1abb44b6ef\",\"Origin\":{\"x\":30,\"z\":21},\"Destination\":{\"x\":29,\"z\":24}},{\"Id\":\"a8c07cc2-7e41-4bd1-aa63-677d6a08a98d\",\"Origin\":{\"x\":29,\"z\":24},\"Destination\":{\"x\":30,\"z\":21}},{\"Id\":\"898fe8d0-c81a-4e0d-99fd-b37c25d0f811\",\"Origin\":{\"x\":25,\"z\":22},\"Destination\":{\"x\":30,\"z\":21}},{\"Id\":\"1e122435-a7ee-4e1a-897f-f6dff89441b2\",\"Origin\":{\"x\":30,\"z\":21},\"Destination\":{\"x\":29,\"z\":24}},{\"Id\":\"4d1a2f66-f445-481e-832f-f62d5fa7d3d9\",\"Origin\":{\"x\":29,\"z\":24},\"Destination\":{\"x\":30,\"z\":21}},{\"Id\":\"41facc6d-fd76-4e07-9544-9f11bd9eaf8a\",\"Origin\":{\"x\":29,\"z\":24},\"Destination\":{\"x\":30,\"z\":21}},{\"Id\":\"566ca93e-ca5d-41ef-94ed-f22630e972a1\",\"Origin\":{\"x\":30,\"z\":21},\"Destination\":{\"x\":25,\"z\":22}},{\"Id\":\"6ad6907c-4325-4d2e-9a24-4795bf2517ae\",\"Origin\":{\"x\":30,\"z\":21},\"Destination\":{\"x\":25,\"z\":22}},{\"Id\":\"43c1500e-dabf-4628-9ced-4ae8ee1f5bb4\",\"Origin\":{\"x\":30,\"z\":21},\"Destination\":{\"x\":25,\"z\":22}},{\"Id\":\"c08266e1-85a1-49cd-8674-3e57fd3d4649\",\"Origin\":{\"x\":30,\"z\":21},\"Destination\":{\"x\":25,\"z\":22}},{\"Id\":\"b7b7c001-a1ee-43bd-959f-3da06af2e76e\",\"Origin\":{\"x\":30,\"z\":21},\"Destination\":{\"x\":25,\"z\":22}}]}";
    private static string jsonString2 ="{\"GridLength\":50,\"GridWidth\":49,\"NetHours\":1,\"NewRoads\":[{\"x\":24,\"z\":21},{\"x\":24,\"z\":28},{\"x\":25,\"z\":21},{\"x\":25,\"z\":28},{\"x\":26,\"z\":21},{\"x\":26,\"z\":28},{\"x\":27,\"z\":21},{\"x\":27,\"z\":28},{\"x\":24,\"z\":22},{\"x\":27,\"z\":22},{\"x\":24,\"z\":23},{\"x\":27,\"z\":23},{\"x\":24,\"z\":24},{\"x\":27,\"z\":24},{\"x\":24,\"z\":25},{\"x\":27,\"z\":25},{\"x\":24,\"z\":26},{\"x\":27,\"z\":26},{\"x\":24,\"z\":27},{\"x\":27,\"z\":27},{\"x\":28,\"z\":21},{\"x\":28,\"z\":28},{\"x\":29,\"z\":21},{\"x\":29,\"z\":28},{\"x\":30,\"z\":21},{\"x\":30,\"z\":28},{\"x\":30,\"z\":22},{\"x\":30,\"z\":23},{\"x\":30,\"z\":24},{\"x\":30,\"z\":25},{\"x\":30,\"z\":26},{\"x\":30,\"z\":27},{\"x\":27,\"z\":14},{\"x\":28,\"z\":14},{\"x\":29,\"z\":14},{\"x\":30,\"z\":14},{\"x\":27,\"z\":15},{\"x\":30,\"z\":15},{\"x\":27,\"z\":16},{\"x\":30,\"z\":16},{\"x\":27,\"z\":17},{\"x\":30,\"z\":17},{\"x\":27,\"z\":18},{\"x\":30,\"z\":18},{\"x\":27,\"z\":19},{\"x\":30,\"z\":19},{\"x\":27,\"z\":20},{\"x\":30,\"z\":20}],\"NewBuildings\":[{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Walter Group\",\"Point\":{\"x\":25,\"z\":25},\"Type\":\"I\",\"id\":\"8df9ad80-f092-4363-9bbf-5399f5131b63\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Terry, Lubowitz and Schuster\",\"Point\":{\"x\":28,\"z\":24},\"Type\":\"C\",\"id\":\"670a73c0-a5f3-41ac-99d7-3ebed8f45748\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Residence\",\"Point\":{\"x\":29,\"z\":18},\"Type\":\"H\",\"id\":\"a92a9b2b-2822-49e1-8a79-459fd5954be7\"}],\"PeopleMoving\":[]}";
    private static string jsonString3 ="{\"GridLength\":50,\"GridWidth\":49,\"NetHours\":1,\"NewRoads\":[{\"x\":24,\"z\":21},{\"x\":24,\"z\":28},{\"x\":25,\"z\":21},{\"x\":25,\"z\":28},{\"x\":26,\"z\":21},{\"x\":26,\"z\":28},{\"x\":27,\"z\":21},{\"x\":27,\"z\":28},{\"x\":24,\"z\":22},{\"x\":27,\"z\":22},{\"x\":24,\"z\":23},{\"x\":27,\"z\":23},{\"x\":24,\"z\":24},{\"x\":27,\"z\":24},{\"x\":24,\"z\":25},{\"x\":27,\"z\":25},{\"x\":24,\"z\":26},{\"x\":27,\"z\":26},{\"x\":24,\"z\":27},{\"x\":27,\"z\":27},{\"x\":28,\"z\":21},{\"x\":28,\"z\":28},{\"x\":29,\"z\":21},{\"x\":29,\"z\":28},{\"x\":30,\"z\":21},{\"x\":30,\"z\":28},{\"x\":30,\"z\":22},{\"x\":30,\"z\":23},{\"x\":30,\"z\":24},{\"x\":30,\"z\":25},{\"x\":30,\"z\":26},{\"x\":30,\"z\":27},{\"x\":27,\"z\":14},{\"x\":28,\"z\":14},{\"x\":29,\"z\":14},{\"x\":30,\"z\":14},{\"x\":27,\"z\":15},{\"x\":30,\"z\":15},{\"x\":27,\"z\":16},{\"x\":30,\"z\":16},{\"x\":27,\"z\":17},{\"x\":30,\"z\":17},{\"x\":27,\"z\":18},{\"x\":30,\"z\":18},{\"x\":27,\"z\":19},{\"x\":30,\"z\":19},{\"x\":27,\"z\":20},{\"x\":30,\"z\":20}],\"NewBuildings\":[{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Walter Group\",\"Point\":{\"x\":25,\"z\":25},\"Type\":\"I\",\"id\":\"8df9ad80-f092-4363-9bbf-5399f5131b63\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Terry, Lubowitz and Schuster\",\"Point\":{\"x\":28,\"z\":24},\"Type\":\"C\",\"id\":\"670a73c0-a5f3-41ac-99d7-3ebed8f45748\"},{\"Rating\":0,\"Capacity\":0,\"IsTall\":true,\"Name\":\"Residence\",\"Point\":{\"x\":29,\"z\":18},\"Type\":\"H\",\"id\":\"a92a9b2b-2822-49e1-8a79-459fd5954be7\"}],\"PeopleMoving\":[]}";
	// measure (full or partial) of a update type.
	private string initialCityState;
    private string partialCityState;

	// detect whether there was the last update.(In hours)
    private int? lastUpdate;

    // deserialize json to object
    private CityData cityData;

    public bool turnOnTestGrid;
    // update trigger
    public bool updateTheCity;

    // simulator's TimeStamp for start: hours + minute/60 + second/60
    public double systemStartedTimeStamp;

    // simualtor's TimeStampe for current: hours + minute/60 + second/60
    public double systemCurrentTimeStamp;

    // timeInHour
    public int timeInHour;

    // population of the city
    private int population = 1000;

    public GameObject buildingManager;

    public GameObject characterManager;

    public GameObject gridManager;

    public float nextTime;

    // Store human references here for easy access
    private Dictionary<int, GameObject> humans = new Dictionary<int, GameObject>();
    

    /// <summary>
    /// Gets the population.
    /// </summary>
    /// <returns>The population.</returns>
    public int Population
    {
        get
        {
            return population;
        }
        set
        {
            population = value;
        }
    }

    // x number of grids in horizontal
    private int size_x;

    /// <summary>
    /// Gets the size x.
    /// </summary>
    /// <returns>The size x.</returns>
    public int Size_x
    {
        get
        {
            return size_x;
        }
        set
        {
            size_x = value;
        }
    }

    // z number of grids in vertical
    private int size_z;

    /// <summary>
    /// Gets the size z.
    /// </summary>
    /// <returns>The size z.</returns>
    public int Size_z
    {
        get
        {
            return size_z;
        }
        set
        {
            size_z = value;
        }
    }

    // grid map contains the index of the zone
    private int[][] grid;

    /// <summary>
    /// Gets or sets the grid.
    /// </summary>
    /// <value>The grid.</value>
    public int[][] Grid
    {
        get
        {
            return grid;
        }
        set
        {
            grid = value;
        }
    }

    // grid map of the walkable path
    private bool[][] path;

    /// <summary>
    /// Gets or sets the path.
    /// </summary>
    /// <value>The path.</value>
    public bool[][] Path
    {
        get
        {
            return path;
        }
        set
        {
            path = value;
        }
    }

    public bool runOnce;
    public bool runOnce2;

    /// <summary>
    /// Gets or sets the humans.
    /// </summary>
    /// <value>The humans.</value>
    public Dictionary<int, GameObject> Humans
    {
        get
        {
            return humans;
        }
        set
        {
            humans = value;
        }
    }

	/// <summary>
	/// Make a request for partial update and request to the server 
	/// </summary>
    private void GetCityUpdate()
    {
        int lastPartialUpdate = lastUpdate ?? -1;

        PartialSimulationUpdateRequest partialUpdate = new PartialSimulationUpdateRequest
        {
            RequestType = "update",
            FullUpdate = false,
            LastUpdate = lastPartialUpdate
        };

        partialCityState = NetworkConnectionHandler.WriteForServer(JsonUtility.ToJson(partialUpdate));
        updateTheCity = true;
    }

    private void GetDatabaseResource()
    {

    }

	/// <summary>
	// Awake this instance.
	/// </summary>
	void Awake () {
        buildingManager = GameObject.Find("BuildingManager");
        characterManager = GameObject.Find("CharacterManager");
        gridManager = GameObject.Find("Grid");

        // Server request initial
        SimulationUpdateRequest fullRequest = new SimulationUpdateRequest
        {
            RequestType = "update",
            FullUpdate = true
        };

		//send a initial reqeust to the server and expect data for an initial update for the application back from the server
        initialCityState = NetworkConnectionHandler.WriteForServer(JsonUtility.ToJson(fullRequest));

        systemStartedTimeStamp = System.DateTime.Now.Minute;
        updateTheCity = false;


        // Debug.Log(cityData.GridLength);
        timeInHour = 0;
        nextTime = 0;
    }

    /// <summary>
    // Start this instance.
    /// </summary>
    void Start () {
        InvokeRepeating("GetCityUpdate", 60.0f, 60.0f);
        
    }

	/// <summary>
	/// Update this instance.
	/// </summary>
	void Update() {
		/// Will be used in the future
		systemCurrentTimeStamp = System.DateTime.Now.Minute;

        // TODO: request update
        if(runOnce)
        if(tryParseInitialCityData(jsonString)){
            if(gridManager.GetComponent<GridManager>().updateEntireGrid()){
                updateCityData();
                updateCity();
            }
            runOnce = false;
        }

        if(runOnce2){
            if(tryParseInitialCityData(jsonString2)){

                if(gridManager.GetComponent<GridManager>().updateEntireGrid()){
                    updateCityData();
                    updateCity();
                }
            }

            runOnce2 = false;
        }

        if (Time.time >= nextTime) {
            nextTime += 1; 
            updateClock((int)nextTime);
        }

		// if(initateCity){
            // if(turnOnTestGrid){ // if turned on for test, initiate test grid
            //     initiateGridForTest();
            // }

  //           if(buildingManager != null
  //               && characterManager != null){

                // if(runOnce){
                //     buildingManager.GetComponent<BuildingManager>().createBuilding("TESTGUID", 1, 1, 2, 2);
                //     runOnce = false;
                //     gridManager.GetComponent<GridManager>().createRoad(1, 2);
                //     gridManager.GetComponent<GridManager>().createRoad(2, 2);
                //     gridManager.GetComponent<GridManager>().createRoad(3, 2);
                //     gridManager.GetComponent<GridManager>().createRoad(4, 2);

                // }

                // if(runOnce2){
                //     buildingManager.GetComponent<BuildingManager>().disposeBuilding("TESTGUID");
                //     runOnce = false;
                // }

                // if (Time.time >= nextTime) {
                //     nextTime += 1; 
                //     updateClock((int)nextTime);
                // }

  //           }
		// }

        // if (updateTheCity) {
        //     cityData = JsonUtility.FromJson<CityData>(partialCityState);
        //     lastUpdate = cityData.netHours;
        //     //Update the city with the latest city data


        //     // TODO :Client team 
            
        //     updateTheCity = false;
        // }

	}

    /// <summary>
    /// try to parse initial city data and returns true false depends on succeed or not
    /// <returns>true false bool for succeed or not</returns>
    /// </summary>
    public bool tryParseInitialCityData(string jstring){
        
        cityData= JsonUtility.FromJson<CityData>(jstring);

        if(cityData == null)
            Debug.Log(cityData);

        return cityData != null ? true:false;
    }

    /// <summary>
    /// Initials the grid.
    /// </summary>
    public void initiateGrid() {

        size_x = cityData.GridWidth;
        size_z = cityData.GridLength;

        grid = new int[size_x][];

        // initalize 2d array
        for (int x = 0; x < grid.Length; x++)
        {
            grid[x] = new int[size_z];
        }

        // iterates grid and assign the zone
        for (int x = 0; x < size_x; x++)
        {
            for (int z = size_z - 1; z >= 0; z--)
            {
                grid[x][z] =  -1;          
            }
        }
    }

    public bool updateCityData(){

        // assign road
        foreach (Point point in cityData.NewRoads)
        {
            grid[point.x][point.z] = 0;
        }

        // assign building
        foreach (NewBuilding building in cityData.NewBuildings)
        {
            // update grid
            int type = -1;
            switch (building.Type[0])
            {
                case 'H':
                    type = 1;
                    break;

                case 'C':
                    type = 2;
                    break;

                case 'I':
                    type = 3;
                    break;

                default:
                    type = -1;
                    break;
            }

            // update CityDataManager.Grid for building
            if (building.Point.x < size_x || building.Point.z < size_z)
            {
                grid[building.Point.x][building.Point.z] = type;

            }
        }

        return true;
    }

    /// <summary>
    /// Creates actual city based on data
    /// </summary>
    public bool updateCity(){

        // new building
        foreach (NewBuilding building in cityData.NewBuildings)
        {

            // Debug.Log(building);
            // Debug.Log((string)building.Type + " "
            //         + (string)building.Name + " "
            //         + building.Point.x + " "
            //         + building.Point.z + " "
            //         + building.Rating + " "
            //         + building.IsTall);

            if (building.Type.Equals("H")){
                buildingManager.GetComponent<BuildingManager>().createBuilding(building.id,
                                                                                building.Point.x,
                                                                                building.Point.z,
                                                                                1,
                                                                                building.Rating);
            } else if (building.Type.Equals("C")){
                buildingManager.GetComponent<BuildingManager>().createBuilding(building.id,
                                                                                building.Point.x,
                                                                                building.Point.z,
                                                                                2,
                                                                                building.Rating);
            } else if (building.Type.Equals("I")){
                buildingManager.GetComponent<BuildingManager>().createBuilding(building.id,
                                                                                building.Point.x,
                                                                                building.Point.z,
                                                                                3,
                                                                                building.Rating);   
            } else {
                Debug.Log("CityDataManager: non deifined building");
            }

        }
        
        // new character
        foreach (PersonTravel person in cityData.PeopleMoving)
        {

            // Debug.Log(person);
            // Debug.Log((string)person.Id + " "
            //         + person.Origin.x + " "
            //         + person.Origin.z + " "
            //         + person.Destination.x + " "
            //         + person.Destination.z + " ");

            characterManager.GetComponent<CharacterCreation>().createCharacter(person.Id, person.Origin.x, person.Origin.z, person.Destination.x, person.Destination.z);
        }

        
        return true;
    }

   

	/// <summary>
	/// Turns on updateTheCity to update city 
	/// </summary>
	public void noticeUpdate(){
		updateTheCity = true;
	}

    /// <summary>
    /// update the clock's time
    /// </summary>
    public void updateClock(int entireHours){
        Text clock = GameObject.Find("Clock").transform.GetChild(0).GetComponent<Text>(); 
        Debug.Log(clock);
        int days = entireHours/24;
        int hours = entireHours%24;
        string textForHour = "";
        
        if(days < 1){
            textForHour = hours + " Hours";
        } else {
            textForHour = days + " Days " + hours + " Hours";
        }

        clock.text = textForHour;
    }


    /// <summary>
    /// Initiate grid for test
    /// </summary>
    public void initiateGridForTest() {

        // initializing and assigning arrays
        grid = new[] {
            new int [] { 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 3, 3, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 3, 3, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 3, 3, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 3, 3, 3, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 3, 3, 3, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 3, 3, 3, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 0, 1, 1, 1, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1}
        };

        // set the size to fit into array of
        size_x = grid.GetLength(0);
        size_z = grid[0].GetLength(0);
    }

    /// <summary>
    /// Gets the index of X.
    /// </summary>
    /// <returns>The index of X.</returns>
    /// <param name="x">The x coordinate.</param>
    /// <param name="z">The z coordinate.</param>
    public int getIndexOfXZ(int x, int z)
    {

        if (x > size_x && z > size_z)
        {
            return -1;
        }

        return grid[x][z];
    }
}
