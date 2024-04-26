using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public static int Coins;
    public static int Diamonds;
    public static int Level;

    public TextMeshProUGUI CoinsText;
    public TextMeshProUGUI DiamondsText;


    private void Start()
    {
        Load();
    }

    private void FixedUpdate()
    {
        CoinsText.text = Coins.ToString();
        DiamondsText.text = Diamonds.ToString();
    }

    private void Load()
    {
        Coins = PlayerPrefs.GetInt("PlayerCoins", 0);
        Diamonds = PlayerPrefs.GetInt("PlayerDiamonds", 0);
        Level = PlayerPrefs.GetInt("PlayerLevel", 0);
    }
    public void Save()
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
