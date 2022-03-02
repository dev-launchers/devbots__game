using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // List.Any()

public class BotCustomizer : MonoBehaviour
{

    public GameObject targetBot;
    public GameObject targetSlot;
    //Used to set the slotposition of this instance
    [SerializeField] SlotPosition slotPosition;
    public List<GameObject> options = default(List<GameObject>);
    //used to get the slots that belong to the bot
    Slots slots = default(Slots);
    private int currentOption = 0;

    private void Awake()
    {
        targetBot = GameObject.FindGameObjectWithTag("Bot");
        slots = targetBot.GetComponent<BotController>().slots;
        targetSlot = slots.GetSlot(slotPosition);
    }
    public void NextOption() {
        currentOption++;
        if (currentOption >= options.Count) {
            currentOption = 0;
        }
        UpdateSlot();
    }

    public void PrevOption() {
        currentOption--;
        if (currentOption < 0) {
            currentOption = options.Count-1;
        }
        UpdateSlot();
    }

    public void UpdateSlot()
    {      
        if (!targetBot)
        {
      targetBot = GameObject.FindGameObjectWithTag("Bot");

            slots = targetBot.GetComponent<BotController>().slots;

        }

        //Set the current option botpart to the correct slot
        slots.SetSlotBotPart(slotPosition, options[currentOption]);
    }


}
