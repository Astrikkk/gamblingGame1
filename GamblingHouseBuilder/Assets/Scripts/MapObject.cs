using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;

[Serializable]
public class Stage
{
    public Sprite sprite;
    public int Value;
}

public class MapObject : MonoBehaviour
{
    public Stage[] Stages;
    public int CurrentStage;
    public TextMeshProUGUI PriceText;
    public GameObject button;
    public Location Location;

    public string SaveFileName = "map_object_data.json";
    private GameManager gameManager;
    private AudioSource Audio;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        Audio = gameObject.GetComponent<AudioSource>();
        LoadData();
        UpdateObject();
    }

    public void Upgrade()
    {
        if (Player.Coins >= Stages[CurrentStage].Value)
        {
            Player.Coins -= Stages[CurrentStage].Value;
            CurrentStage++;
            Audio.Play();
            SaveData();
        }
        else
        {
            gameManager.NotEnoughMoneyText();
        }
        UpdateObject();
        Player.Save();
    }

    private void SaveData()
    {
        MapObjectData data = new MapObjectData();
        data.CurrentStage = CurrentStage;

        string jsonData = JsonUtility.ToJson(data);
        File.WriteAllText(GetSaveFilePath(), jsonData);
    }

    private void LoadData()
    {
        string filePath = GetSaveFilePath();
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            MapObjectData data = JsonUtility.FromJson<MapObjectData>(jsonData);
            CurrentStage = data.CurrentStage;
        }
    }

    private string GetSaveFilePath()
    {
        return Path.Combine(Application.persistentDataPath, SaveFileName);
    }

    public void UpdateObject()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = Stages[CurrentStage].sprite;
        if (CurrentStage + 1 < Stages.Length)
        {
            PriceText.text = Stages[CurrentStage].Value.ToString();
        }
        else
        {
            PriceText.text = "";
        }
        button.SetActive(CurrentStage + 1 < Stages.Length);
        Location.UpdateCompeled();
    }
}

[Serializable]
public class MapObjectData
{
    public int CurrentStage;
}
