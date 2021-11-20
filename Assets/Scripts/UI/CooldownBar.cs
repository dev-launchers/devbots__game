using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    [Tooltip("Used to pick which slot position tis cooldown bar belongs to")]
    [SerializeField] private SlotPosition slotPosition;
    [Tooltip("The slider for the cooldown bar")]
    [SerializeField] Slider slider;
    //The botcontroller this cooldown bar belongs to
    private BotController botController;
    //The ability holder that this cooldown bar is going to be using
    BotAbilityHolder botAbilityHolder;
    private void Start()
    {
        Debug.Log(transform.parent.GetComponentInParent<HealthAndCDHolder>().name);
        //Define which bot is which
        if (transform.parent.GetComponentInParent<HealthAndCDHolder>().isPlayerHolder)
        {
            botController = SceneHandler.GetPlayerBotControllerInScene();
        }
        else
        {
            botController = SceneHandler.GetEnemyBotControllerInScene();
        }
       
        //get the bot ability holder
        botAbilityHolder = botController.slots.GetSlotBotAbilityHolder(slotPosition);
        //check if bot ability holder does not exists in slot
        if (!botAbilityHolder)
        {
            //get all children transforms for this cooldown bar
            var childrenTransforms = GetComponentsInChildren<Transform>();
            //Loop through each transform
            foreach (var _transform in childrenTransforms)
            {
                //if botpart doesn't exist in slot gameobject will be deactivated
                _transform.gameObject.SetActive(false);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        //check if bot ability holder exists in slot
        if (botAbilityHolder)
        {
            ///Set slider maxvalue to botpart cooldown time
            if (slider.maxValue != botAbilityHolder.GetCooldownData().Item2)
            {
                slider.maxValue = botAbilityHolder.GetCooldownData().Item2;
            }
           
            //Update the value of the slider
            slider.value = botAbilityHolder.GetCooldownData().Item2 - botAbilityHolder.GetCooldownData().Item1;
        }

    }
}
