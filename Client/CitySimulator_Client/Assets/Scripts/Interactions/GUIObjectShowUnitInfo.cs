using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;
using System;

/// <summary>
/// Module: ShowUnitInfo
/// Team: Client
/// Description: Show information of units
/// Author: Benjamin Hao Date: Oct. 28th, 2017
/// Modified by:
///     Name: Benjamin Hao   Change: modified Select() method  Date: Oct.29th, 2017
///     Name: Benjamin Hao   Change: Removed Thumbnails for information bubble Date: 2017-11-25
///     Name: Benjamin Hao   Change: Added GUID for each object. Date: 2017-11-28
/// Based on:  N/A
/// </summary>

public class GUIObjectShowUnitInfo : Interaction
{
    public TextMesh GUID;  // the name of units
    //public TextMesh info1;   // the information of units
    //public TextMesh info2; // TODO: something needs to be added
    //public Sprite ProfilePic;    // the profile picture of units

    // JSON dummy String for testing
    private static string jsonString = "{\"GridLength\":99,\"GridWidth\":58,\"netHours\":0,\"NewRoads\":[{\"x\":27,\"z\":49},{\"x\":27,\"z\":56},{\"x\":28,\"z\":49},{\"x\":28,\"z\":56},{\"x\":29,\"z\":49},{\"x\":29,\"z\":56},{\"x\":30,\"z\":49},{\"x\":30,\"z\":56},{\"x\":27,\"z\":50},{\"x\":30,\"z\":50},{\"x\":27,\"z\":51},{\"x\":30,\"z\":51},{\"x\":27,\"z\":52},{\"x\":30,\"z\":52},{\"x\":27,\"z\":53},{\"x\":30,\"z\":53},{\"x\":27,\"z\":54},{\"x\":30,\"z\":54},{\"x\":27,\"z\":55},{\"x\":30,\"z\":55},{\"x\":24,\"z\":56},{\"x\":24,\"z\":63},{\"x\":25,\"z\":56},{\"x\":25,\"z\":63},{\"x\":26,\"z\":56},{\"x\":26,\"z\":63},{\"x\":27,\"z\":63},{\"x\":24,\"z\":57},{\"x\":27,\"z\":57},{\"x\":24,\"z\":58},{\"x\":27,\"z\":58},{\"x\":24,\"z\":59},{\"x\":27,\"z\":59},{\"x\":24,\"z\":60},{\"x\":27,\"z\":60},{\"x\":24,\"z\":61},{\"x\":27,\"z\":61},{\"x\":24,\"z\":62},{\"x\":27,\"z\":62},{\"x\":21,\"z\":63},{\"x\":21,\"z\":70},{\"x\":22,\"z\":63},{\"x\":22,\"z\":70},{\"x\":23,\"z\":63},{\"x\":23,\"z\":70},{\"x\":24,\"z\":70},{\"x\":21,\"z\":64},{\"x\":24,\"z\":64},{\"x\":21,\"z\":65},{\"x\":24,\"z\":65},{\"x\":21,\"z\":66},{\"x\":24,\"z\":66},{\"x\":21,\"z\":67},{\"x\":24,\"z\":67},{\"x\":21,\"z\":68},{\"x\":24,\"z\":68},{\"x\":21,\"z\":69},{\"x\":24,\"z\":69}],\"NewBuildings\":[{\"id\":\"24132329-e85a-4072-b9c8-1dab463b8443\",\"Name\":\"Pacocha Inc\",\"Point\":{\"x\":28,\"z\":54},\"Type\":\"I\",\"Rating\":0,\"IsTall\":true},{\"id\":\"0a6a8518-fc33-4d7d-bf88-ef7464f72d5e\",\"Name\":\"Hilll, Kohler and Effertz\",\"Point\":{\"x\":25,\"z\":59},\"Type\":\"C\",\"Rating\":0,\"IsTall\":true},{\"id\":\"43018e9e-b03b-45d1-b214-ae7a623d5a8a\",\"Name\":\"Residence\",\"Point\":{\"x\":22,\"z\":69},\"Type\":\"H\",\"Rating\":0,\"IsTall\":true}]}";
    // deserialize json to object
    private CityData cityData = JsonUtility.FromJson<CityData>(jsonString);

    /// <summary>
    /// override Select() method
    /// </summary>
    public override void Select()
    {
        //GUIObjectInfoManager.Current.SetPic(ProfilePic);
        //GUIObjectInfoManager.Current.SetLines(
        //    GUID,
        //    info1,
        //    info2);
        //GUID = GetComponentInChildren(typeof(TextMesh)) as TextMesh;
        //info1 = GetComponentInChildren(typeof(TextMesh)) as TextMesh;
        //info2 = GetComponentInChildren(typeof(TextMesh)) as TextMesh;
        //string guid = GetInstanceID().ToString();
        //GUID.text = guid;
        //info1.text = "test2";
        //info2.text = "test3";
        //Guid g = Guid.NewGuid();
        //GUID.text = g.ToString();

        // TODO: interact with database
        foreach (NewBuilding building in cityData.NewBuildings)
        {
            GUID.text = building.id + "\n"
                + building.Name + "\n"
                + building.Point.ToString() + "\n"
                + building.Type + "\n"
                + building.Rating.ToString() + "\n"
                + building.IsTall.ToString();
        }
    }

    /// <summary>
    /// override Deselect() method.
    /// </summary>
    public override void Deselect()
    {
        //GUIObjectInfoManager.Current.ClearPic();
        //GUIObjectInfoManager.Current.ClearLines();
    }
}