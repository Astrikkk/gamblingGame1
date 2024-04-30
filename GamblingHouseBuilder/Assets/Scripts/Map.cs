using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public List<Location> locations;
    public List<GameObject> LocationObjects;

    [SerializeField]
    private int currentlocation;

    private void FixedUpdate()
    {
        if (locations[currentlocation].isCompeleted) 
        {
            LocationObjects[currentlocation].SetActive(false);
            currentlocation++;
            LocationObjects[currentlocation].SetActive(true);
        }
    }
}
