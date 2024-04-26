using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class MapObjectData
{
    public int ObjIndex;
    public int ObjLevel;
}

public class Map : MonoBehaviour
{
    private List<MapObjectData> mapObjectDatas;
    public static Action<int, int> onLoadUpgrades;

    private string filePath;

    void Start()
    {
        filePath = Application.persistentDataPath + "/MapData.json";

        mapObjectDatas = LoadData();
    }

    private void CollectData(int ObjIndex, int ObjLevel)
    {
        MapObjectData newData = new MapObjectData();
        newData.ObjIndex = ObjIndex;
        newData.ObjLevel = ObjLevel;

        mapObjectDatas.Add(newData);
        SaveData();
    }

    private void OnEnable()
    {
        MapObject.onUpgraded += CollectData;
    }

    private void OnDisable()
    {
        MapObject.onUpgraded -= CollectData;
        SaveData();
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

    void SaveData()
    {
        string jsonData = JsonUtility.ToJson(mapObjectDatas);
        File.WriteAllText(filePath, jsonData);
    }

    List<MapObjectData> LoadData()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            List<MapObjectData> data = JsonUtility.FromJson<List<MapObjectData>>(jsonData);
            foreach (var item in data)
            {
                onLoadUpgrades?.Invoke(item.ObjIndex, item.ObjLevel);
            }
            return data;
        }
        else
        {
            Debug.LogWarning("No save data found!");
            return new List<MapObjectData>();
        }
    }
}
