using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    /* This will  */

    [SerializeField] private GameObject owner;
    [SerializeField] private HurtBoxType hurtBoxType = HurtBoxType.Player; // Get this on Start

    // [SerializeField] private STATUS_CONTAINER_SCRIPT statusContainer;
    private void Start()
    {
        // hurtBoxLayer might need to be changed so that Player, Enemy and Ally Hurtbox Types are allocated correctly
    }

    public bool CheckHit(Collider2D collider, HurtboxMask enemyHurtMask, List<HitEffect> effects)
    {
        Debug.Log("Checking for hitmask match in " + this.name);
        if (enemyHurtMask == (HurtboxMask)hurtBoxType)
        {
            HitResponse(effects);
            return true;
        }

        return false;
    }

    /* This function is called on collision of another hitbox. It will take the hiteffect and the  */
    public void HitResponse(List<HitEffect> effects)
    {
        foreach(HitEffect effect in effects)
        {
            effect.PerformEffect(gameObject);
        }
    }

}
