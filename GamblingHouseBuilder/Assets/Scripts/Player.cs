using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static int Coins;
    public static int Diamonds;
    public static int Level;

    public List<TextMeshProUGUI> CoinsText;
    public List<TextMeshProUGUI> DiamondsText;


    private void Start()
    {
        Load();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < CoinsText.Count; i++)
        {
            CoinsText[i].text = Coins.ToString();
        }
        for (int i = 0; i < DiamondsText.Count; i++)
        {
            DiamondsText[i].text = Diamonds.ToString();
        }
    }

    private static void Load()
    {
        Coins = PlayerPrefs.GetInt("PlayerCoins", 0);
        Diamonds = PlayerPrefs.GetInt("PlayerDiamonds", 0);
        Level = PlayerPrefs.GetInt("PlayerLevel", 0);
    }
    public static void Save()
    {
        PlayerPrefs.SetInt("PlayerCoins", Coins);
        PlayerPrefs.SetInt("PlayerDiamonds", Diamonds);
        PlayerPrefs.SetInt("PlayerLevel", Level);
    }

    public void AddMoney()
    {
        Coins += 100;
    }
}
