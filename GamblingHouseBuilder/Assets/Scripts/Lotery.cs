using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Lotery : MonoBehaviour
{
    public TMP_InputField MoneyInputField;
    public TMP_InputField DiamondsInputField;
    public int Increment = 2;
    public GameObject WinMenu;
    public GameObject LoseMenu;
    public TextMeshProUGUI WinCoinText;
    public TextMeshProUGUI WinDiamondsText;
    [SerializeField] private AudioClip jackpotSound;
    [SerializeField] private AudioSource audioSource;
    public GameObject NotEnoughMoneyBar;
    public TextMeshProUGUI NotEnoughMoneyText;


    public void PlayRulette()
    {
        int diamondsBet;
        int coinsBet;
        
        if (!int.TryParse(DiamondsInputField.text, out diamondsBet))
        {
            diamondsBet = 0;
        }

        if (!int.TryParse(MoneyInputField.text, out coinsBet))
        {
            coinsBet = 0;
        }
        if (diamondsBet == 0 && coinsBet == 0) return;

        if(Player.Coins<coinsBet && Player.Diamonds<diamondsBet)
        {
            NotEnoughMoney("Not enough coins and diamonds! \n Recomended bet (diamonds/coins): " +Mathf.Abs(Player.Diamonds / 2).ToString() +"/"+(Player.Coins / 2).ToString());
            return;
        }
        else if (Player.Diamonds < diamondsBet)
        {
            NotEnoughMoney("Not enough diamonds! \n Recomended bet: " + Mathf.Abs(Player.Diamonds / 2));
            return;
        }
        else if (Player.Coins < coinsBet)
        {
            NotEnoughMoney("Not enough coins! \n Recomended bet: " + Mathf.Abs(Player.Coins / 2));
            return;
        }

        // Deduct bets from player's resources
        Player.Coins -= coinsBet;
        Player.Diamonds -= diamondsBet;

        // Check if player wins or loses
        if (RandomChance(100 / Increment))
        {
            Player.Coins += (coinsBet * Increment);
            Player.Diamonds += (diamondsBet * Increment);
            WinMenu.SetActive(true);
            audioSource.PlayOneShot(jackpotSound);
            WinCoinText.text = (coinsBet * Increment).ToString();
            WinDiamondsText.text = (diamondsBet * Increment).ToString();
        }
        else
        {
            LoseMenu.SetActive(true);
        }
    }

    public void SetIncrement(int a)
    {
        Increment = a;
    }

    bool RandomChance(int probability)
    {
        return Random.Range(0, 100) < probability;
    }

    public void NotEnoughMoney(string text)
    {
        NotEnoughMoneyBar.SetActive(true);
        NotEnoughMoneyText.text = text;
        MoneyInputField.text = "";
        DiamondsInputField.text = "";
    }
}
