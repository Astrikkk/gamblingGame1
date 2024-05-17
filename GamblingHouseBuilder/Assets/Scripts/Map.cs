using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Map : MonoBehaviour
{
    public List<Location> locations;
    public List<GameObject> LocationObjects;

    [SerializeField]
    private int currentlocation;

    private void FixedUpdate()
    {
        if (locations[currentlocation].isCompeleted && currentlocation < locations.Count - 1) 
        {
            LocationObjects[currentlocation].SetActive(false);
            currentlocation++;
            LocationObjects[currentlocation].SetActive(true);
        }
    }
}
