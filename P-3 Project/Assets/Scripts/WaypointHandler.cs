using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WaypointHandler : MonoBehaviour
{
   [SerializeField] private List<GameObject> waypoints = new List<GameObject>(); //store waypoints
    public List<GameObject> Waypoints { get { return waypoints; } } // getter to return yhe list
    public static WaypointHandler Instance { set; get; }
    // Start is called before the first frame update
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);

            Instance.Waypoints.AddRange(GameObject.FindGameObjectsWithTag("Waypoint"));
            Instance.waypoints = Instance.waypoints.OrderBy(waypoint => ExtractNumber(waypoint.name)).ToList();
            // Debug.Log(Instance.waypoints.Count);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Helper function to extract numeric value from GameObject name
    int ExtractNumber(string name)
    {
        string numberString = new string(name.Where(char.IsDigit).ToArray()); // extract the numbers into a new string 
        return int.TryParse(numberString, out int number) ? number : int.MaxValue; // convert the string to an int and return that.
    }
}