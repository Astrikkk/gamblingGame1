using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    public List<MapObject> mapObjects;

    public bool isCompeleted = false;


    public void UpdateCompeled()
    {
        bool temp = true;
        for (int i = 0; i < mapObjects.Count; i++)
        {
            if (mapObjects[i].CurrentStage + 1 != mapObjects[i].Stages.Length)
            {
                temp = false;
            }
        }
        isCompeleted = temp;
    }
}
