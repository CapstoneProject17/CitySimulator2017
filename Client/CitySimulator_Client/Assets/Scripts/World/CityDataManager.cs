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
    private static string jsonString = "{\"GridLength\":50,\"GridWidth\":49,\"NetHours\":0,\"NewRoads\":[{\"X\":24,\"Z\":21},{\"X\":24,\"Z\":28},{\"X\":25,\"Z\":21},{\"X\":25,\"Z\":28},{\"X\":26,\"Z\":21},{\"X\":26,\"Z\":28},{\"X\":27,\"Z\":21},{\"X\":27,\"Z\":28},{\"X\":24,\"Z\":22},{\"X\":27,\"Z\":22},{\"X\":24,\"Z\":23},{\"X\":27,\"Z\":23},{\"X\":24,\"Z\":24},{\"X\":27,\"Z\":24},{\"X\":24,\"Z\":25},{\"X\":27,\"Z\":25},{\"X\":24,\"Z\":26},{\"X\":27,\"Z\":26},{\"X\":24,\"Z\":27},{\"X\":27,\"Z\":27},{\"X\":21,\"Z\":21},{\"X\":21,\"Z\":28},{\"X\":22,\"Z\":21},{\"X\":22,\"Z\":28},{\"X\":23,\"Z\":21},{\"X\":23,\"Z\":28},{\"X\":21,\"Z\":22},{\"X\":21,\"Z\":23},{\"X\":21,\"Z\":24},{\"X\":21,\"Z\":25},{\"X\":21,\"Z\":26},{\"X\":21,\"Z\":27},{\"X\":24,\"Z\":14},{\"X\":25,\"Z\":14},{\"X\":26,\"Z\":14},{\"X\":27,\"Z\":14},{\"X\":24,\"Z\":15},{\"X\":27,\"Z\":15},{\"X\":24,\"Z\":16},{\"X\":27,\"Z\":16},{\"X\":24,\"Z\":17},{\"X\":27,\"Z\":17},{\"X\":24,\"Z\":18},{\"X\":27,\"Z\":18},{\"X\":24,\"Z\":19},{\"X\":27,\"Z\":19},{\"X\":24,\"Z\":20},{\"X\":27,\"Z\":20}],\"NewBuildings\":[{\"Rating\":0,\"Capacity\":50,\"IsTall\":true,\"Name\":\"Tremblay, Hudson and Abernathy\",\"Point\":{\"X\":25,\"Z\":24},\"Type\":\"I\",\"Id\":\"e50bd0d7-246a-4f0f-a4e6-2718416f4151\"},{\"Rating\":0,\"Capacity\":50,\"IsTall\":true,\"Name\":\"Davis, Grimes and Champlin\",\"Point\":{\"X\":22,\"Z\":24},\"Type\":\"C\",\"Id\":\"5bdb506e-4df0-4c9b-8699-c880df2b8c95\"},{\"Rating\":0,\"Capacity\":50,\"IsTall\":true,\"Name\":\"Residence\",\"Point\":{\"X\":25,\"Z\":16},\"Type\":\"H\",\"Id\":\"4598989a-309f-4293-9150-736081c2fb58\"},{\"Rating\":0,\"Capacity\":50,\"IsTall\":true,\"Name\":\"Residence\",\"Point\":{\"X\":26,\"Z\":15},\"Type\":\"H\",\"Id\":\"5377f77d-44fd-4719-8dc0-48a4a880b30d\"}],\"PeopleMoving\":[{\"Id\":\"15304cdf-2a98-4c92-8dae-a7a101d21cc6\",\"Origin\":{\"X\":25,\"Z\":16},\"Destination\":{\"X\":25,\"Z\":24}},{\"Id\":\"153dfafa-a27f-442f-aaa4-fa0fc750cf7b\",\"Origin\":{\"X\":25,\"Z\":24},\"Destination\":{\"X\":25,\"Z\":16}},{\"Id\":\"e69d08a1-7fef-4ebb-9907-32ef1912f57a\",\"Origin\":{\"X\":25,\"Z\":24},\"Destination\":{\"X\":25,\"Z\":16}},{\"Id\":\"d1b55e8e-4ac9-4e60-bcc2-c386a0c42c92\",\"Origin\":{\"X\":25,\"Z\":16},\"Destination\":{\"X\":25,\"Z\":24}},{\"Id\":\"0991a29e-7fad-4d79-8fd4-effb3b220b2a\",\"Origin\":{\"X\":25,\"Z\":24},\"Destination\":{\"X\":25,\"Z\":16}},{\"Id\":\"502ad4d0-8adf-45c8-82a9-7be7ad64e750\",\"Origin\":{\"X\":22,\"Z\":24},\"Destination\":{\"X\":26,\"Z\":15}},{\"Id\":\"091b0c4d-6912-4953-a1c3-08f207fbbde0\",\"Origin\":{\"X\":22,\"Z\":24},\"Destination\":{\"X\":26,\"Z\":15}},{\"Id\":\"3f4683fd-09e2-4038-a53c-2d4dd98397fa\",\"Origin\":{\"X\":26,\"Z\":15},\"Destination\":{\"X\":22,\"Z\":24}}]}";
    
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

    public bool runOnce = true;
    public bool runOnce2 = true;

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
        if(tryParseInitialCityData(jsonString)){
            initiateGrid();  
            updateCityData();
        }
        
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
            initiateGridForTest();

            if(gridManager.GetComponent<GridManager>().updateEntireGrid()){
                 updateCityForTest();
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

    public List<Point> getPossibleDest(){

        List<Point> possibleDest = new List<Point>();

        for (int x = 0; x < size_x; x++)
        {
            for (int z = 0; z < size_z; z++){
                if(grid[x][z] == 0) {
                    possibleDest.Add(new Point{ x = x, z = z });
                }
            }
        }

		return possibleDest;
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
