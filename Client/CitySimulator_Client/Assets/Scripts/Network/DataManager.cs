using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// Module: CityDataManager
/// Team: Network
/// Description: Manage the all the data of the city
/// Author:
///	 Name: Gisu Kim    Date: 2017-11 12
/// Modified by:	
///	 Name: Gisu Kim   	Date: 2017-11-13
///	 Name: Gisu Kim   	Date: 2017-11-14
///  Name: Gisu Kim		Date: 2017-11-20
///	 Name: Gisu Kim   	Date: 2017-11-24
///  Name Gisu Kim		Date: 2017-11-25
/// Based on:  N/A
/// </summary>
/// 
public class DataManager{
	public static bool isClock = false;
	public static bool isGridObject = false;
	public static bool isPerson = false;
	public static bool isProduct = false;
	public static bool isState = false;


	/// <summary>
	/// detect data and parse into an object
	/// </summary>
	/// <returns>N/A.</returns>
	public static void ToObject(string data) { 

		DetectDataType (data);

		if (isClock) {
			ReturnClockObj (data);
		} else if (isGridObject) {
			ReturnGridObj (data);
		} else if (isPerson) {
			ReturnPersonObj (data);
		} else if (isProduct) {
			ReturnProductObj (data);
		} else if (isState) {
			ReturnStateObj (data);
		}

		

	}

	/// <summary>
	/// detect data type
	/// </summary>
	/// <returns>N/A.</returns>
	public static void DetectDataType(string data) {
		//parse a stream into a readable string 
		//detect an object typoe and turn one of the boolean value to be true 
		int type = 0;

		switch(type) {
		case 0: 
			isClock = true;
			break;
		case 1: 
			isGridObject = true;
			break;
		case 2: 
			isPerson = true;
			break;
		case 3: 
			isProduct = true;
			break;
		case 4: 
			isState = true;
			break;
		}
	}

	/// <summary>
	///  parse into an Clock object
	/// </summary>
	/// <returns>an Clock object.</returns>
	public static Clock ReturnClockObj (string data) {
		int netMinutes;
		int netHours;
		int netDays;
		int netYears;

		Clock clockObj = new Clock ();
		//deserialize json string into object
		clockObj = JsonUtility.FromJson<Clock>(data);


		//enable to overwrite already-created json object
		JsonUtility.FromJsonOverwrite(data, clockObj);

		return clockObj;
	}

	/// <summary>
	///  parse into an Grid object
	/// </summary>
	/// <returns>an Grid object.</returns>
	public static GridObject ReturnGridObj (string data) {
		string guid;
		int xPoint;
		int yPoint;

		GridObject gridObj = new GridObject();
		//deserialize json string into object
		gridObj = JsonUtility.FromJson<GridObject>(data);


		//enable to overwrite already-created json object
		JsonUtility.FromJsonOverwrite(data, gridObj);

		return gridObj;
	}

	/// <summary>
	///  parse into an Person object
	/// </summary>
	/// <returns>an Person object.</returns>
	public static Person ReturnPersonObj (string data) {
		string guid;
		string firstName;
		string lastName;
		int monthlyIncome;
		int accountBalance;
		string workPlaceID;
		int workplaceX;
		int workplaceY;
		string homeID; 
		int homeX;
		int homeY;
		int daysLeft;
		int age;
		int startShift;
		int endShift;

		Person personObj = new Person();
		//deserialize json string into object
		personObj = JsonUtility.FromJson<Person>(data);


		//enable to overwrite already-created json object
		JsonUtility.FromJsonOverwrite(data, personObj);

		return personObj;
	}


	/// <summary>
	///  parse into an Product object
	/// </summary>
	/// <returns>an Product object.</returns>
	public static Product ReturnProductObj (string data) {
		string name;
		int globalCount;

		Product productObj = new Product();
		//deserialize json string into object
		productObj = JsonUtility.FromJson<Product>(data);


		//enable to overwrite already-created json object
		JsonUtility.FromJsonOverwrite(data, productObj);

		return productObj;
	}


	/// <summary>
	///  parse into an State object
	/// </summary>
	/// <returns>an State object.</returns>
	public static SaveState ReturnStateObj (string data) {
		SaveState stateObj = new SaveState();
		//deserialize json string into object
		stateObj = JsonUtility.FromJson<SaveState>(data);


		//enable to overwrite already-created json object
		JsonUtility.FromJsonOverwrite(data, stateObj);

		return stateObj;
	}

}
