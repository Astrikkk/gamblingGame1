using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[Serializable]
public class Stage
{
    public Sprite sprite;
    public int Value;
}
public class MapObject : MonoBehaviour
{
    public static Action<int, int> onUpgraded;
    public int ObjectNumber;

    public Stage [] Stages;
    public int CurrentStage;
    public TextMeshProUGUI PriceText;
    public GameObject button;

    private void Start()
    {
        UpdateObject();
    }
    public void Upgrade()
    {
        if (Player.Coins >= Stages[CurrentStage].Value)
        {
            Player.Coins -= Stages[CurrentStage].Value;
            onUpgraded?.Invoke(ObjectNumber, CurrentStage + 1);
            CurrentStage++;
        }
        UpdateObject();
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
    }


    private void getData(int Index, int Level)
    {
        if (Index == ObjectNumber)
        {
            CurrentStage = Level;
            UpdateObject();
        }
    }

    private void OnEnable()
    {
        Map.onLoadUpgrades += getData;
    }
    private void OnDisable()
    {
        Map.onLoadUpgrades -= getData;
    }
}


