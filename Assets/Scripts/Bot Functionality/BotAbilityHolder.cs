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
        //activeTime += Time.deltaTime;
        switch(state)
        {
            case AbilityState.Ready:
                if (isRunning/*condition for any move*/)
                {
                    SwitchToActive();
                }
                break;
            
            case AbilityState.Active:
                if(!hasAnimation)
                    if (activeTime > 0) activeTime -= Time.deltaTime;
                    else
                    {
                        SwitchToCooldown();
                    }
                break;
            
            case AbilityState.Cooldown: 
                if (coolDownTime > 0) coolDownTime -= Time.deltaTime;
                else
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
        
        animator.SetTrigger(COOLDOWN);
        
        Debug.Log("Cooldown");
    }

    public void SwitchToReady()
    {
        animator.SetTrigger(ACTIVE);
        Debug.Log("Ready");
        state = AbilityState.Ready;
    }

    public void SwitchToActive()
    {
        ability.Activate(gameObject);
        state = AbilityState.Active;
        activeTime = ability.activeTime;
    }
}
