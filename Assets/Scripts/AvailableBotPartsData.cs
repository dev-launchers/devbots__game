using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Scriptable object class used to get all the available botparts for each slot position and can randomly pick a botpart for each slot.
/// </summary>
[CreateAssetMenu(fileName = "AvailableBotPartsData", menuName = "ScriptableObjects/AvailableBotPartsData", order = 1)]
public class AvailableBotPartsData : ScriptableObject
{
    [Header("These are the lists of botpart gameobjects belonging to each slot")]
    public List<GameObject> topSlotBotParts;
    public List<GameObject> sideSlotBotParts;
    public List<GameObject> backSlotBotParts;
    public List<GameObject> bottomSlotBotParts;

    /// <summary>
    /// This is used to pick a random botpart for a particular slot
    /// </summary>
    /// <param name="slotBotParts"></param>
    /// <returns>Bot part Gameobject</returns>
    public GameObject PickRandomBotPart(List<GameObject> slotBotParts)
    {
        return slotBotParts[Random.Range(0, slotBotParts.Count)];
    }
}