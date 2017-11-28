using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// /// <summary>
/// Module: Clock
/// Team: Network
/// Description: Clock used for timing and synchronization
/// Author:
///  Name: Gisu Kim    Date: 2017-11-23
/// Modified by:     Gisu Kim  
///  Name: N/A   Change: N/A         Date: N/A
/// Based on:  Server logic models 
/// </summary>
[System.Serializable]
public class Clock {

	/// <summary>
	/// Initializes a new instance of the <see cref="Clock"/> class.
	/// </summary>
	public Clock(){
	}


	/// <summary>
	/// Initializes a new instance of the <see cref="Clock"/> class with necessary values.
	/// </summary>
	/// <param name="netMinutes">Net minutes.</param>
	/// <param name="netHours">Net hours.</param>
	/// <param name="netDays">Net days.</param>
	/// <param name="netYears">Net years.</param>
	public Clock(int netMinutes, int netHours, int netDays, int netYears)
	{
		NetMinutes = netMinutes;
		NetHours = netHours;
		NetDays = netDays;
		NetYears = netYears;
	}

	/// <summary>
	/// BSON objectId stored in the database.
	/// </summary>
	public string _id { get; set; }

	/// <summary>
	/// NetMinutes must be an unsigned int between 0 - 59
	/// </summary>
	public int NetMinutes { get; set; }

	/// <summary>
	/// NetHours must be an unsigned int between 0 - 23
	/// </summary>
	public int NetHours { get; set; }

	/// <summary>
	/// NetDays must be an unsigned int between 0 - 365
	/// </summary>
	public int NetDays { get; set; }

	/// <summary>
	/// NetYears must be an unsigned int
	/// </summary>
	public int NetYears { get; set; }

}

/// /// <summary>
/// Module: GridObject
/// Team: Network
/// Description: GridObject contains all the general fields of grid objects
/// Author:
///  Name: Gisu Kim    Date: 2017-11-23
/// Modified by:     Gisu Kim  
///  Name: N/A   Change: N/A         Date: N/A
/// Based on:  Server logic models 
/// </summary>

[System.Serializable]
public class GridObject
{
	/// <summary>
	/// Initializes a new instance of the <see cref="GridObject"/> class.
	/// </summary>
	/// <param name="guid">GUID.</param>
	/// <param name="xPoint">X point.</param>
	/// <param name="yPoint">Y point.</param>
	public GridObject() {
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="GridObject"/> class with necessary vlaues.
	/// </summary>
	/// <param name="guid"></param>
	/// <param name="xPoint"></param>
	/// <param name="yPoint"></param>
	public GridObject(string guid, int xPoint, int yPoint)
	{
		Guid = guid;
		XPoint = xPoint;
		YPoint = yPoint;
	}

	/// <summary>
	/// BSON objectId stored in the database.
	/// </summary>
	public string _id { get; set; }

	/// <summary>
	/// ID for the grid object
	/// </summary>
	public string Guid { get; set; }

	/// <summary>
	/// XPoint must be a valid X-Coordinate
	/// </summary>
	public int XPoint { get; set; }

	/// <summary>
	/// YPoint must be a valid Y-Coordinate
	/// </summary>
	public int YPoint { get; set; }
}

/// /// <summary>
/// Module: Person
/// Team: Network
/// Description: Person class mapped to the Person collection of the database and the "actors" in the simulation.
/// Author:
///  Name: Gisu Kim    Date: 2017-11-24
/// Modified by:     Gisu Kim  
///  Name: N/A   Change: N/A         Date: N/A
/// Based on:  Server logic models 
/// </summary>

[System.Serializable]
public class Person
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Person"/> class.
	/// </summary>
	/// <param name="guid">GUID.</param>
	/// <param name="firstName">First name.</param>
	/// <param name="lastName">Last name.</param>
	/// <param name="monthlyIncome">Monthly income.</param>
	/// <param name="accountBalance">Account balance.</param>
	/// <param name="workPlaceID">Work place I.</param>
	/// <param name="workplaceX">Workplace x.</param>
	/// <param name="workplaceY">Workplace y.</param>
	/// <param name="homeID">Home I.</param>
	/// <param name="homeX">Home x.</param>
	/// <param name="homeY">Home y.</param>
	/// <param name="daysLeft">Days left.</param>
	/// <param name="age">Age.</param>
	/// <param name="startShift">Start shift.</param>
	/// <param name="endShift">End shift.</param>
	public Person() {

	}
	/// <summary>
	/// Initializes a new instance of the <see cref="Person"/> class with necessary values.
	/// </summary>
	/// <param name="guid">The person's uniquele generated ID</param>
	/// <param name="firstName">The person's first name</param>
	/// <param name="lastName">The person's last name</param>
	/// <param name="monthlyIncome">The monthly income of the person</param>
	/// <param name="accountBalance">The account balance of the person</param>
	/// <param name="workPlaceID">The Guid of the person's workplace</param>
	/// <param name="workplaceX">The X coordinate of the person's workplace</param>
	/// <param name="workplaceY">The Y coordinate of the person's workplace</param>
	/// <param name="homeID">The Guid of the person's home</param>
	/// <param name="homeX">The X coordinate of the person's home</param>
	/// <param name="homeY">The Y coordinate of the person's home</param>
	/// <param name="daysLeft">The number of days the person has left to live</param>
	/// <param name="age">The age of the person</param>
	/// <param name="startShift">The time the person starts their work</param>
	/// <param name="endShift">The time the person finishes their work</param>
	public Person(string guid, string firstName, string lastName, int monthlyIncome, int accountBalance, string workPlaceID, int workplaceX, int workplaceY, string homeID, int homeX, int homeY, int daysLeft, int age, int startShift, int endShift)
	{
		Guid = guid;
		FirstName = firstName;
		LastName = lastName;
		MonthlyIncome = monthlyIncome;
		AccountBalance = accountBalance;
		WorkplaceID = workPlaceID;
		WorkplaceX = workplaceX;
		WorkplaceY = workplaceY;
		HomeID = homeID;
		HomeX = homeX;
		HomeY = homeY;
		DaysLeft = daysLeft;
		Age = age;
		StartShift = startShift;
		EndShift = endShift;
	}

	/// <summary>
	/// BSON objectId stored in the database.
	/// </summary>
	public string _id { get; set; }

	/// <summary>
	/// ID for the person
	/// </summary>
	public string Guid { get; set; }

	/// <summary>
	/// 1. FirstName can not be null
	/// 2. FirstName can not be empty
	/// 3. FirstName can not be longer than 30 letters
	/// </summary>
	public string FirstName { get; set; }

	/// <summary>
	/// 1. LastName can not be null
	/// 2. LastName can not be empty
	/// 3. LastName can not be longer than 30 letters
	/// </summary>
	public string LastName { get; set; }

	/// <summary>
	/// MonthlyIncome can not be negative
	/// </summary>
	public int MonthlyIncome { get; set; }

	/// <summary>
	/// AccountBalance can not be negative
	/// </summary>
	public int AccountBalance { get; set; }

	/// <summary>
	/// WorkplaceID must be a valid Guid
	/// </summary>
	public string WorkplaceID { get; set; }

	/// <summary>
	/// WorkplaceX must be a valid X-Coordinate
	/// </summary>
	public int WorkplaceX { get; set; }

	/// <summary>
	/// WorkplaceY must be a valid Y-Coordinate
	/// </summary>
	public int WorkplaceY { get; set; }

	/// <summary>
	/// HomeID must be a valid Guid
	/// </summary>
	public string HomeID { get; set; }

	/// <summary>
	/// HomeX must be a valid X-Coordinate
	/// </summary>
	public int HomeX { get; set; }

	/// <summary>
	/// HomeY must be a valid Y-Coordinate
	/// </summary>
	public int HomeY { get; set; }

	/// <summary>
	/// DaysLeft must be an int between 0 - 125
	/// </summary>
	public int DaysLeft { get; set; }

	/// <summary>
	/// Age can not be negative
	/// </summary>
	public int Age { get; set; }

	/// <summary>
	/// start shift time must be an int between 0 - 23
	/// </summary>
	public int StartShift { get; set; }

	/// <summary>
	/// end shift time must be an int between 0 - 23
	/// </summary>
	public int EndShift { get; set; }
}

/// /// <summary>
/// Module: Product
/// Team: Network
/// Description: Products being bought/sold within the simulated economy.
/// Author:
///  Name: Gisu Kim    Date: 2017-11-25
/// Modified by:     Gisu Kim  
///  Name: N/A   Change: N/A         Date: N/A
/// Based on:  Server logic models 
/// </summary>



[System.Serializable]
public class Product
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Product"/> class.
	/// </summary>
	/// <param name="name">Name.</param>
	/// <param name="globalCount">Global count.</param>
	public Product (){

	}
	/// <summary>
	/// Initializes a new instance of the <see cref="Product"/> class with necessary values.
	/// </summary>
	/// <param name="name">The name of the product</param>
	/// <param name="globalCount">The total number of this product in the simulation</param>
	public Product(string name, int globalCount)
	{
		Name = name;
		GlobalCount = globalCount;
	}

	/// <summary>
	/// BSON objectId stored in the database.
	/// </summary>
	public string _id { get; set; }

	/// <summary>
	/// 1. Name can not be null
	/// 2. Name can not be empty
	/// 3. Name can not be longer than 30 letters
	/// </summary>
	public string Name { get; set; }

	/// <summary>
	/// GlobalCount int no limit
	/// </summary>
	public int GlobalCount { get; set; }
}

/// /// <summary>
/// Module: SaveState
/// Team: Network
/// Description: SaveState backs up the current state of the simulation.
/// Author:
///  Name: Gisu Kim    Date: 2017-11-25
/// Modified by:     Gisu Kim  
///  Name: N/A   Change: N/A         Date: N/A
/// Based on:  Server logic models 
/// </summary>

[System.Serializable]
public class SaveState {
	public string Id { get; set; }
	public List<SaveState> BackupState { get; set; }
}