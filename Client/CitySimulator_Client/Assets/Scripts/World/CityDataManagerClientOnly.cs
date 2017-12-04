using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Module: CityDataManagerClientOnly
/// Team: Client
/// Description: Handle the all the data of the city, for now it has been used for client only
/// Author:
///  Name: Dongwon(Shawn) Kim    Date: 2017-12-01
/// Modified by:    
/// Based on:  
/// https://docs.unity3d.com/Manual/JSONSerialization.html
/// https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
/// </summary>
public class CityDataManagerClientOnly : MonoBehaviour {

    public int textFileIndex = 0;
    // switch for testing
    // JSON dummy String for testing
    private  string jsonString;
    
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
    public float trackTime;

    // Store human references here for easy access
    private Dictionary<int, GameObject> humans = new Dictionary<int, GameObject>();

    public bool runOnce2 = false;


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
        // int lastPartialUpdate = lastUpdate ?? -1;

        // PartialSimulationUpdateRequest partialUpdate = new PartialSimulationUpdateRequest
        // {
        //     RequestType = "update",
        //     FullUpdate = false,
        //     LastUpdate = lastPartialUpdate
        // };

        // partialCityState = NetworkConnectionHandler.WriteForServer(JsonUtility.ToJson(partialUpdate));
        // updateTheCity = true;
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

        // // Server request initial
        // SimulationUpdateRequest fullRequest = new SimulationUpdateRequest
        // {
        //     RequestType = "update",
        //     FullUpdate = true
        // };

        // //send a initial reqeust to the server and expect data for an initial update for the application back from the server
        // initialCityState = NetworkConnectionHandler.WriteForServer(JsonUtility.ToJson(fullRequest));

        // systemStartedTimeStamp = System.DateTime.Now.Minute;
        // updateTheCity = false;
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

        if(runOnce2){
            initiateGridForTest();

            if(gridManager.GetComponent<GridManager>().updateEntireGridForClient()){

                
                 updateCityForTest();
            }
            runOnce2 = false;
        }

        if (Time.time >= (nextTime*30)) {
            trackTime = Time.time;
            nextTime += 1;
            updateClock((int)nextTime);
        }

    }

    /// <summary>
    /// try to parse initial city data and returns true false depends on succeed or not
    /// <returns>true false bool for succeed or not</returns>
    /// </summary>
    public bool tryParseInitialCityData(string jstring){
        
        cityData= JsonUtility.FromJson<CityData>(jstring);

        if(cityData == null)
            Debug.Log("tryParseInitialCityData: null");

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

    /// <summary>
    /// Creates actual city based on data
    /// </summary>
    public bool updateCityForTest(){

        int indexB = 0;
        int indexP = 0;
        // new building
        for (int x = 0; x < size_x; x++)
        {
            for (int z = 0; z < size_z; z++){
                
                if(grid[x][z] >= 1 && grid[x][z] <= 3){
                    int rate = UnityEngine.Random.Range (0, 3);
                    buildingManager.GetComponent<BuildingManager>().createBuilding("TEST BUILDING" + indexB++,
                                                                                x,
                                                                                z,
                                                                                grid[x][z],
                                                                                rate );
                }

                if(x < 24 && z < 13)
                if(grid[x][z] == 0) {
                    characterManager.GetComponent<CharacterCreation>().createCharacter("TEST PERSON" + indexP++, x, z, 24, 13);
                }
            }
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
        // Debug.Log(clock);
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