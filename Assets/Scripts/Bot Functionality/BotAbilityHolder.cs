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
    private const string ACTIVE = "active";
    private const string COOLDOWN = "cooldown";
    
    private AbilityState state = AbilityState.Active;
    
    [SerializeField]private BotAbility ability;

    private float coolDownTime;

    private float activeTime = 0;

    [SerializeField] private bool isRunning;

    /// <summary>
    /// This method returns the cooldown data for this botpart ability
    /// </summary>
    /// <returns>item1 = current cooldownTimer, item2 = ability cooldown time</returns>
    public Tuple<float, float> GetCooldownData()
    {
        return new Tuple<float, float>(coolDownTime, ability.coolDownTime);
    }
    [SerializeField] private bool hasAnimation;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        SwitchToCooldown();//start in cooldown state
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case AbilityState.Ready:
                if (isRunning/*condition for any move*/)
                {
                    SwitchToActive();
                }
                break;
            case AbilityState.Active:
                if (!hasAnimation)
                {
                    activeTime -= Time.deltaTime;
                    if (activeTime <= 0.0f)
                    {
                        SwitchToCooldown();
                    }
                }
                break;
            
            case AbilityState.Cooldown: 
                coolDownTime -= Time.deltaTime;
                if(coolDownTime <= 0.0f)
                {
                    SwitchToReady();
                }
                break;
        }
    }


    public void SwitchToCooldown()
    {
        coolDownTime = ability.coolDownTime;
        state = AbilityState.Cooldown;
        if(animator != null)
        {
        animator.SetTrigger(COOLDOWN);
        }

        
        Debug.Log("Cooldown");
    }

    public void SwitchToReady()
    {
        if(animator != null)
        {
        animator.SetTrigger(ACTIVE);
        }

        Debug.Log("Ready");
        state = AbilityState.Ready;
    }

    public void SwitchToActive()
    {
        ability.Activate(gameObject);
        state = AbilityState.Active;
        activeTime = ability.activeTime;
        Debug.Log("Active");




    }

    public void SetState(State _state)
    {
        isRunning = true;
    }
}
