using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Module: GUIMouseEventManager
/// Team: Client
/// Description: Add functionality for mouse clicking, in order to select objects
/// Author: 
///     Name: Benjamin Hao Date: 2017-10-24
/// Modified by:    
///     Name: Benjamin Hao   Change: added skip functionality to increase performance    Date: 2017-10-30
///     Name: Benjamin Hao   Change: added Shift key functionality: Mutipul Select       Date: 2017-10-30
///     Name: Benjamin Hao   Change: added Control key functionality: Zoom in            Date: 2017-11-29
/// Based on: https://docs.unity3d.com/ScriptReference/Input.GetMouseButtonDown.html
///           https://docs.unity3d.com/ScriptReference/Physics.Raycast.html
/// </summary>

public class GUIMouseEventManager : MonoBehaviour
{

    private List<GUIObjectInteractive> Selections = new List<GUIObjectInteractive>();  // the list of selected objects
    private RaycastHit hit;
    // Following is for double click functionality
    public float moveSpeed = 1.0f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))    // If there's no clicking, then skip. To increase performance.
        {
            var es = UnityEngine.EventSystems.EventSystem.current;
            if (es != null && es.IsPointerOverGameObject()) // If the user click 2D objects(such as UI), then need to avoid selecting 3D objects.
                                                            // Meanwhile, we need to check if the user clicked 2D objects
                return;

            if (Selections.Count > 0)
            {
                if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))   // Remove the objects selected by right click and Shift key
                {
                    foreach (var sel in Selections)
                    {
                        if (sel != null) sel.Deselect();
                    }
                    Selections.Clear();
                }
            }

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))  // If nothing is clicked, return
                return;

            var interact = hit.transform.GetComponent<GUIObjectInteractive>();
            if (interact == null) // Check "Interactive" module, if Null, then return
                return;

            Selections.Add(interact);
            interact.Select();
        }

        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.LeftControl))
            Camera.main.transform.position = Vector3.Lerp(transform.position, hit.transform.position, moveSpeed * Time.deltaTime);
    }
}