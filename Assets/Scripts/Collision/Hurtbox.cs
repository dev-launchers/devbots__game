using System.Collections.Generic;
using UnityEngine;
[RequireComponent( typeof(Rigidbody2D), typeof(BoxCollider2D) )]
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
            // HitEffects<> enemyHitEffects = collider.GetComponent<HitEffects>();
            HitResponse(/*enemyHitEffects*/);
            foreach (HitEffect e in effects)
            {
                e.PerformEffect(transform.parent.gameObject);//this assume the hurtbox is a child of the bot which has a bot controller
            }
            return true;
        }

        return false;
    }

    /* This function is called on collision of another hitbox. It will take the hiteffect and the  */
    private void HitResponse(/*HitEffect<> hitEffects */)
    {
        Debug.Log(this.name + " was hit!");
        /* Pseudocode  */
        // statusContainer.addEffect(hitEffects);
    }

}
