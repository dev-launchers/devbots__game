using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public enum AbilityState
{
    Ready,
    Active,
    Cooldown
};
public class BotAbilityHolder : MonoBehaviour
{
    

    private AbilityState state = AbilityState.Active;
    
    public BotAbility ability;

    private float coolDownTime;

    private float activeTime;

    [SerializeField] private bool isRunning;

    /// <summary>
    /// This method returns the cooldown data for this botpart ability
    /// </summary>
    /// <returns>item1 = current cooldownTimer, item2 = ability cooldown time</returns>
    public Tuple<float, float> GetCooldownData()
    {
        return new Tuple<float, float>(coolDownTime, ability.coolDownTime);
    }
    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case AbilityState.Ready:
                if (isRunning/*condition for any move*/)
                {
                    ability.Activate(gameObject);
                    state = AbilityState.Active;
                    activeTime = ability.activeTime;
                }
                break;
            
            case AbilityState.Active:
                if (activeTime > 0) activeTime -= Time.deltaTime;
                else
                {
                    state = AbilityState.Cooldown;
                    coolDownTime = ability.coolDownTime;
                }
                break;
            
            case AbilityState.Cooldown: 
                if (coolDownTime > 0) coolDownTime -= Time.deltaTime;
                else
                {
                    state = AbilityState.Ready;
                }
                break;
        }
    }
}
