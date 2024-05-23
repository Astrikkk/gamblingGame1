using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Map : MonoBehaviour
{
    public List<Location> locations;
    public List<GameObject> LocationObjects;

    public int ProgressLevel;
    public Slider ProgressBar;
    public TextMeshProUGUI LevelText;

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

    public void UpdateData()
    {
        LevelText.text=(currentlocation + 1).ToString();
        if(currentlocation >= locations.Count - 1) LevelText.text="Max level";
        int currentProgress = 0;
        int maxProgress = 0;
        for (int i = 0; i < locations.Count; i++)
        {
            for (int j = 0; j < locations[i].mapObjects.Count; j++)
            {
                currentProgress += locations[i].mapObjects[j].CurrentStage + 1;
                maxProgress += locations[i].mapObjects[j].Stages.Length;
            }
        }
        ProgressBar.value = currentProgress;
        ProgressBar.maxValue = maxProgress;
    }
}
