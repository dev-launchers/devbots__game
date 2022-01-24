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

    public bool CheckHit(Collider2D collider, HurtboxMask enemyHurtMask)
    {
        Debug.Log("Checking for hitmask match in " + this.name);
        if (enemyHurtMask == (HurtboxMask)hurtBoxType)
        {
            // HitEffects<> enemyHitEffects = collider.GetComponent<HitEffects>();
            HitResponse(/*enemyHitEffects*/);
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
