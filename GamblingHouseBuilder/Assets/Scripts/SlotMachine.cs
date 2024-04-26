using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//якщо хтось це читаЇ, вибачте за гавнокод
public class SlotMachine : MonoBehaviour
{
    public List<SpriteRenderer> Slots1;
    public List<SpriteRenderer> Slots2;
    public List<SpriteRenderer> Slots3;
    public List<SlotObj> SlotObjects;

    public List<Image> ResultImages;
    public TextMeshProUGUI ResultText;
    public GameObject ResultMenu;
    public List<string> ResultStrings;
    [SerializeField] private float timeAdder;
    [SerializeField] private float spinTime = 5f; // „ас, п≥сл€ €кого слоти зупин€ютьс€
    private float currentSpinTime = 0f; // ѕоточний час "сп≥ну"

    private SlotObj middleSlot1;
    private SlotObj middleSlot2;
    private SlotObj middleSlot3;
    private bool isSpinning = false;

    private void Update()
    {
        if (isSpinning)
        {
            SpinSlots();
        }
    }

    public void Spin()
    {
        if (!isSpinning)
        {
            isSpinning = true;
            currentSpinTime = 0f;
        }
    }

    private void SpinSlots()
    {
        currentSpinTime += Time.deltaTime;

        if (currentSpinTime >= spinTime)
        {
            isSpinning = false;
            currentSpinTime = 0f;
            ShowResult();
            return;
        }

        // якщо час сп≥ну ще не минув, м≥н€Їмо картинки кожн≥ 0.1 секунди
        if (Time.time >= timeAdder)
        {
            timeAdder = Time.time + 0.1f;
            ChangeImg();
        }
    }

    private void ChangeImg()
    {
        Slots1[2].sprite = Slots1[1].sprite;
        Slots2[2].sprite = Slots2[1].sprite;
        Slots3[2].sprite = Slots3[1].sprite;
        Slots1[1].sprite = Slots1[0].sprite;
        Slots2[1].sprite = Slots2[0].sprite;
        Slots3[1].sprite = Slots3[0].sprite;


        SpriteRenderer slot1 = Slots1[0];
        SpriteRenderer slot2 = Slots2[0];
        SpriteRenderer slot3 = Slots3[0];

        // ¬ибираЇмо випадкову картинку з≥ списку
        int randomIndex1 = Random.Range(0, SlotObjects.Count);
        SlotObj randomSlotObj1 = SlotObjects[randomIndex1];

        int randomIndex2 = Random.Range(0, SlotObjects.Count);
        SlotObj randomSlotObj2 = SlotObjects[randomIndex2];

        int randomIndex3 = Random.Range(0, SlotObjects.Count);
        SlotObj randomSlotObj3 = SlotObjects[randomIndex3];

        // «м≥нюЇмо картинку дл€ кожного слоту
        slot1.sprite = randomSlotObj1.sprite;
        slot2.sprite = randomSlotObj2.sprite;
        slot3.sprite = randomSlotObj3.sprite;
    }

    private void ShowResult()
    {
        // ќтримуЇмо ≥мена середн≥х картинок
        string middleSlot1Name = Slots1[1].sprite.name;
        string middleSlot2Name = Slots2[1].sprite.name;
        string middleSlot3Name = Slots3[1].sprite.name;

        // «находимо ≥ндекси цих ≥мен в SlotObjects
        int middleSlot1Index = SlotObjects.FindIndex(obj => obj.sprite.name == middleSlot1Name);
        int middleSlot2Index = SlotObjects.FindIndex(obj => obj.sprite.name == middleSlot2Name);
        int middleSlot3Index = SlotObjects.FindIndex(obj => obj.sprite.name == middleSlot3Name);

        if (middleSlot1Index != -1 && middleSlot2Index != -1 && middleSlot3Index != -1)
        {
            // ќтримуЇмо в≥дпов≥дн≥ SlotObj за знайденими ≥ндексами
            middleSlot1 = SlotObjects[middleSlot1Index];
            middleSlot2 = SlotObjects[middleSlot2Index];
            middleSlot3 = SlotObjects[middleSlot3Index];

            // ¬иводимо в дебаг ≥нформац≥ю про результат
            Debug.Log("Result:");
            Debug.Log("Slot 1: " + middleSlot1.Name);
            Debug.Log("Slot 2: " + middleSlot2.Name);
            Debug.Log("Slot 3: " + middleSlot3.Name);

            Invoke("ActivateResults", 1);
        }
        else
        {
            Debug.LogError("Error: One or more middle slots not found in SlotObjects.");
        }
    }

    private void ActivateResults()
    {
        ResultMenu.SetActive(true);
        ResultImages[0].sprite = middleSlot1.sprite;
        ResultImages[1].sprite = middleSlot2.sprite;
        ResultImages[2].sprite = middleSlot3.sprite;

        if (middleSlot1 == middleSlot2 == middleSlot3)
        {
            ResultText.text = middleSlot1.WinText;
            Player.Coins += middleSlot1.ThreeSlotCoins;
            Player.Diamonds += middleSlot1.ThreeSlotDiamonds;

            Debug.Log("Jackpot");
        }
        else if (middleSlot1 == middleSlot2 && middleSlot2 == middleSlot3 && middleSlot1 == middleSlot3)
        {
            ResultText.text = ResultStrings[Random.RandomRange(0, ResultStrings.Count - 1)];
            Player.Coins += middleSlot1.TwoSlotCoins;
            Player.Coins += middleSlot2.TwoSlotCoins;
            Player.Coins += middleSlot3.TwoSlotCoins;
            Player.Diamonds += middleSlot1.TwoSlotDiamonds;
            Player.Diamonds += middleSlot2.TwoSlotDiamonds;
            Player.Diamonds += middleSlot3.TwoSlotDiamonds;
        }
        else
        {
            ResultText.text = ResultStrings[Random.RandomRange(0, ResultStrings.Count - 1)];
            Player.Coins += middleSlot1.OneSlotCoins;
            Player.Coins += middleSlot2.OneSlotCoins;
            Player.Coins += middleSlot3.OneSlotCoins;
            Player.Diamonds += middleSlot1.OneSlotDiamonds;
            Player.Diamonds += middleSlot2.OneSlotDiamonds;
            Player.Diamonds += middleSlot3.OneSlotDiamonds;
        }
    }
}
