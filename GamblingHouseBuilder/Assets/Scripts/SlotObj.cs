using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Slot Object", menuName = "Slot Object")]
public class SlotObj : ScriptableObject
{
    public string Name;
    public Sprite sprite;
    public int OneSlotCoins;
    public int TwoSlotCoins;
    public int ThreeSlotCoins;
    public int OneSlotDiamonds;
    public int TwoSlotDiamonds;
    public int ThreeSlotDiamonds;
    public string WinText;
}
