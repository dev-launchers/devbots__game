using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This class is where the bots botpart slots are located 
/// </summary>
[System.Serializable]
public class Slots
{
    [Header("These are the game objects used as slots on the bot where the bot's botpart gameobject's belong")]
    public GameObject Top;
    public GameObject Side;
    public GameObject Bottom;
    public GameObject Back;

    //List of all the slots
    private List<GameObject> slots;


    public Slots()
    {
        SetSlots();
    }
    //Initiate slots list by adding all slots to list
    private void SetSlots()
    {
        slots = new List<GameObject>();
        slots.Add(Top);
        slots.Add(Side);
        slots.Add(Bottom);
        slots.Add(Back);
    }


    /// <summary>
    /// Get the slot gameobject by using the slotpositioon enum
    /// </summary>
    /// <param name="slotPosition"></param>
    /// <returns></returns>
    private GameObject GetSlot(SlotPosition slotPosition)
    {
        GameObject slot = default(GameObject);
        //match slotposition to correct slot
        switch (slotPosition)
        {
            case SlotPosition.Top:
                slot = Top;
                break;
            case SlotPosition.Side:
                slot = Side;
                break;
            case SlotPosition.Bottom:
                slot = Bottom;
                break;
            case SlotPosition.Back:
                slot = Back;
                break;
            default:
                break;
        }
        //return correct gamebject located at slot
        return slot;
    }

    /// <summary>
    /// check to see if botpart is in slot position
    /// </summary>
    /// <param name="slotPosition"></param>
    /// <returns></returns>
    public bool IsBotPartInSlot(SlotPosition slotPosition)
    {
        //get slot using slot position
        var slot = GetSlot(slotPosition);
        return slot.GetComponent<BotPart>();
    }

    /// <summary>
    /// Set a new botpart in the botpart slot
    /// </summary>
    /// <param name="slotPosition"></param>
    /// <param name="botPartGameObject"></param>
    public void SetSlotBotPart(SlotPosition slotPosition, GameObject botPartGameObject)
    {
        //get slot using slot position
        var slot = GetSlot(slotPosition);
        //check if any other gameobjects are located on this slot
        if (slot.transform.childCount > 0)
        {
            //destroy gameobject on this slot
            Object.Destroy(GetSlotBotPartGameObject(slot));
        }
        //Instantiate botpart gameobject and parent it to this slot
        Object.Instantiate(botPartGameObject, slot.transform.position, slot.transform.rotation, slot.transform);

    }

    /// <summary>
    /// Get the botpart gameobject located at this slot
    /// </summary>
    /// <param name="slot"></param>
    /// <returns></returns>
    private GameObject GetSlotBotPartGameObject(GameObject slot)
    {
        return slot.transform.GetChild(0).gameObject;
    }

    /// <summary>
    /// Get the  botpart located at this slot
    /// </summary>
    /// <param name="slotPosition"></param>
    /// <returns></returns>
    public BotPart GetSlotBotPart(SlotPosition slotPosition)
    {
        GameObject slot = GetSlot(slotPosition);
        return slot.GetComponentInChildren<BotPart>();
    }

}
