using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Lotery : MonoBehaviour
{
    public TMP_InputField MoneyInputField;
    public TMP_InputField DiamondsInputField;
    public int Increment = 2;
    public GameObject WinMenu;
    public GameObject LoseMenu;
    public TextMeshProUGUI WinCoinText;
    public TextMeshProUGUI WinDiamondsText;
    public Slider CoinSlider;
    public Slider DiamondSlider;
    [SerializeField] private AudioClip jackpotSound;
    [SerializeField] private AudioSource audioSource;
    public GameObject NotEnoughMoneyBar;
    public TextMeshProUGUI NotEnoughMoneyText;
    private int coinsBet;

    public int CoinsBet
    {
        get { return coinsBet; }
        set { coinsBet = value;
            CoinSlider.value = CoinsBet;
            UpdateMenues();
        }
    }
    private int diamondsBet;

    public int DiamondsBet
    {
        get { return diamondsBet; }
        set {
            diamondsBet = value;
            DiamondSlider.value = DiamondsBet;
            UpdateMenues();
        }
    }


    private void Start()
    {
        UpdateMenues();
    }
    

    public void PlayRulette()
    {
        
        if (!int.TryParse(DiamondsInputField.text, out diamondsBet))
        {
            DiamondsBet = 0;
        }

        if (!int.TryParse(MoneyInputField.text, out coinsBet))
        {
            CoinsBet = 0;
        }
        if (DiamondsBet == 0 && CoinsBet == 0) return;

        if(Player.Coins<CoinsBet && Player.Diamonds<DiamondsBet)
        {
            NotEnoughMoney("Not enough coins and diamonds! \n Recomended bet (diamonds/coins): " +Mathf.Abs(Player.Diamonds / 2).ToString() +"/"+(Player.Coins / 2).ToString());
            return;
        }
        else if (Player.Diamonds < DiamondsBet)
        {
            NotEnoughMoney("Not enough diamonds! \n Recomended bet: " + Mathf.Abs(Player.Diamonds / 2));
            return;
        }
        else if (Player.Coins < CoinsBet)
        {
            NotEnoughMoney("Not enough coins! \n Recomended bet: " + Mathf.Abs(Player.Coins / 2));
            return;
        }

        // Deduct bets from player's resources
        Player.Coins -= CoinsBet;
        Player.Diamonds -= DiamondsBet;

        // Check if player wins or loses
        if (RandomChance(100 / Increment))
        {
            Player.Coins += (CoinsBet * Increment);
            Player.Diamonds += (DiamondsBet * Increment);
            WinMenu.SetActive(true);
            audioSource.PlayOneShot(jackpotSound);
            WinCoinText.text = (CoinsBet * Increment).ToString();
            WinDiamondsText.text = (DiamondsBet * Increment).ToString();
        }
        else
        {
            LoseMenu.SetActive(true);
        }
        UpdateMenues();
    }
    public void SetBetFromSlider()
    {
        CoinsBet = (int)CoinSlider.value;
        DiamondsBet = (int)DiamondSlider.value;
    }
    private void UpdateMenues()
    {
        DiamondSlider.maxValue = Player.Diamonds;
        CoinSlider.maxValue = Player.Coins;
        DiamondsInputField.text = DiamondsBet.ToString();
        MoneyInputField.text = CoinsBet.ToString();
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
