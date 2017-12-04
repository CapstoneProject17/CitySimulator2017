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
    public int X;

    // z coordinate
    public int Z;
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
    public string Id;
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

    // measure (full or partial) of a update type.
    private string receivedCityState;

    // detect whether there was the last update.(In hours)
    private int? lastUpdate;

    // deserialize json to object
    private CityData cityData;

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

    // each manager reference
    public GameObject buildingManager;
    public GameObject characterManager;
    public GameObject gridManager;

    // time trigger
    public float nextTime;

    // Store human references here for easy access
    private Dictionary<int, GameObject> humans = new Dictionary<int, GameObject>();


    // human render every frame
    public bool humanSwitch = true;
    public PersonTravel[] people;
    public int humanCounter;

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

        PartialSimulationUpdateRequest partialUpdate = new PartialSimulationUpdateRequest("update", false, lastPartialUpdate);
        receivedCityState = AsynchronousClient.StartClient(JsonUtility.ToJson(partialUpdate));
        updateTheCity = true;
    }

    private void GetDatabaseResource()
    {

    }

    /// <summary>
    // Awake this instance.
    /// </summary>
    void Awake()
    {
        buildingManager = GameObject.Find("BuildingManager");
        characterManager = GameObject.Find("CharacterManager");
        gridManager = GameObject.Find("Grid");

        // Server request initial
        SimulationUpdateRequest fullRequest = new SimulationUpdateRequest("update", true);

        //send a initial reqeust to the server and expect data for an initial update for the application back from the server
        receivedCityState = AsynchronousClient.StartClient(JsonUtility.ToJson(fullRequest));

        systemStartedTimeStamp = System.DateTime.Now.Minute;
        updateTheCity = true;


        // Debug.Log(cityData.GridLength);
        timeInHour = 0;
        nextTime = 0;
    }

    /// <summary>
    // Start this instance.
    /// </summary>
    void Start()
    {
        InvokeRepeating("GetCityUpdate", 15.0f, 15.0f);
        if (updateTheCity && tryParseInitialCityData(receivedCityState))
        {
            initiateGrid();
            updateCityData();
            humanCounter = cityData.PeopleMoving.Length;
        }
    }

    /// <summary>
    /// Update this instance.
    /// </summary>
    void Update()
    {
        /// Will be used in the future
        systemCurrentTimeStamp = System.DateTime.Now.Minute;

        if (updateTheCity && tryParseInitialCityData(receivedCityState))
        {
            updateCityData();

            if (gridManager.GetComponent<GridManager>().updateEntireGrid())
            {
                updateCity();
            }

            people = cityData.PeopleMoving;

            updateTheCity = false;
        }

        if(Time.time >= nextTime){
            if(people != null && humanCounter >= 0){   
                characterManager.GetComponent<CharacterCreation>().createCharacter(
                        people[humanCounter].Id,
                        people[humanCounter].Origin.X,
                        people[humanCounter].Origin.Z, 
                        people[humanCounter].Destination.X,
                        people[humanCounter].Destination.Z);
                humanCounter--;
            }
        }


        if (Time.time >= nextTime * 15)
        {
            nextTime += 1;
            updateClock((int)nextTime);
        }

    }

    /// <summary>
    /// try to parse initial city data and returns true false depends on succeed or not
    /// <returns>true false bool for succeed or not</returns>
    /// </summary>
    public bool tryParseInitialCityData(string jstring)
    {

        cityData = JsonUtility.FromJson<CityData>(receivedCityState);

        if (cityData == null)
            Debug.Log("tryParseInitialCityData: null");

        lastUpdate = cityData.NetHours;
        return cityData != null ? true : false;
    }

    /// <summary>
    /// Initials the grid.
    /// </summary>
    public void initiateGrid()
    {

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
                grid[x][z] = -1;
            }
        }
    }

    public bool updateCityData()
    {

        // assign road
        foreach (Point point in cityData.NewRoads)
        {
            grid[point.X][point.Z] = 0;
        }

        // assign building
        foreach (NewBuilding building in cityData.NewBuildings)
        {
            if (building.Type.Equals("H"))
            {
                grid[building.Point.X][building.Point.Z] = 1;
            }
            else if (building.Type.Equals("C"))
            {
                grid[building.Point.X][building.Point.Z] = 2;
            }
            else if (building.Type.Equals("I"))
            {
                grid[building.Point.X][building.Point.Z] = 3;
            }
            else { }


        }

        return true;
    }

    /// <summary>
    /// Creates actual city based on data
    /// </summary>
    public bool updateCity()
    {

        // new building
        foreach (NewBuilding building in cityData.NewBuildings)
        {

            Debug.Log(building.Id);
            // Debug.Log((string)building.Type + " "
            //         + (string)building.Name + " "
            //         + building.Point.X + " "
            //         + building.Point.Z + " "
            //         + building.Rating + " "
            //         + building.IsTall);

            if (building.Type.Equals("H"))
            {
                buildingManager.GetComponent<BuildingManager>().createBuilding(building.Id,
                                                                                building.Point.X,
                                                                                building.Point.Z,
                                                                                1,
                                                                                building.Rating);
            }
            else if (building.Type.Equals("C"))
            {
                buildingManager.GetComponent<BuildingManager>().createBuilding(building.Id,
                                                                                building.Point.X,
                                                                                building.Point.Z,
                                                                                2,
                                                                                building.Rating);
            }
            else if (building.Type.Equals("I"))
            {
                buildingManager.GetComponent<BuildingManager>().createBuilding(building.Id,
                                                                                building.Point.X,
                                                                                building.Point.Z,
                                                                                3,
                                                                                building.Rating);
            }
            else
            {
                Debug.Log("CityDataManager: non deifined building");
            }

        }

        // new character
        // foreach (PersonTravel person in cityData.PeopleMoving)
        // {

        //     // Debug.Log(person);
        //     // Debug.Log((string)person.Id + " "
        //     //         + person.Origin.X + " "
        //     //         + person.Origin.Z + " "
        //     //         + person.Destination.X + " "
        //     //         + person.Destination.Z + " ");

        //     characterManager.GetComponent<CharacterCreation>().createCharacter(person.Id, person.Origin.X, person.Origin.Z, person.Destination.X, person.Destination.Z);
        // }


        return true;
    }


    /// <summary>
    /// Creates actual city based on data
    /// </summary>
    public bool updateCityForTest()
    {

        int indexB = 0;
        int indexP = 0;
        // new building
        for (int x = 0; x < size_x; x++)
        {
            for (int z = 0; z < size_z; z++)
            {

                if (grid[x][z] >= 1 && grid[x][z] <= 3)
                {
                    int rate = UnityEngine.Random.Range(0, 3);
                    buildingManager.GetComponent<BuildingManager>().createBuilding("TEST BUILDING" + indexB++,
                                                                                x,
                                                                                z,
                                                                                grid[x][z],
                                                                                rate);
                }

                if (x < 24 && z < 13)
                    if (grid[x][z] == 0)
                    {
                        characterManager.GetComponent<CharacterCreation>().createCharacter("TEST PERSON" + indexP++, x, z, 24, 13);
                    }
            }
        }

        return true;
    }

    /// <summary>
    /// Turns on updateTheCity to update city 
    /// </summary>
    public void noticeUpdate()
    {
        updateTheCity = true;
    }

    /// <summary>
    /// update the clock's time
    /// </summary>
    public void updateClock(int entireHours)
    {
        Text clock = GameObject.Find("Clock").transform.GetChild(0).GetComponent<Text>();
        // Debug.Log(clock);
        int days = entireHours / 24;
        int hours = entireHours % 24;
        string textForHour = "";

        if (days < 1)
        {
            textForHour = hours + " Hours";
        }
        else
        {
            textForHour = days + " Days " + hours + " Hours";
        }

        clock.text = textForHour;
    }


    /// <summary>
    /// Initiate grid for test
    /// </summary>
    public void initiateGridForTest()
    {

        // initializing and assigning arrays
        grid = new[] {
            new int [] { 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 3, 3, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 1, 1, 1, 1, 0, 3, 3, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 1, 0, 2, 2, 2, 1, 0, 3, 3, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 3, 3, 3, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 3, 3, 3, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 3, 3, 3, 0, 1, 1, 1, 1, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 2, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 2, 0, 3, 3, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
            new int [] { 0, 1, 1, 1, 2, 0, 2, 2, 3, 3, 0, 1, 1, 0, 1, 1, 3, 3, 3, 1, 1},
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
