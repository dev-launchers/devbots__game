using System.Collections.Generic;
using UnityEngine;
[RequireComponent( typeof(Rigidbody2D), typeof(BoxCollider2D) )]
public class Hurtbox : MonoBehaviour
{
    /* This will  */

    [SerializeField] private GameObject owner;
    [SerializeField] private HurtBoxType hurtBoxType; // Get this on Start

    // [SerializeField] private STATUS_CONTAINER_SCRIPT statusContainer;
    private void Start()
    {
	if(transform.parent.gameObject.GetComponent<BotSensor>().IsPlayer())
	{
		hurtBoxType = HurtBoxType.Player;
	}
	else hurtBoxType=HurtBoxType.Enemy;
        // hurtBoxLayer might need to be changed so that Player, Enemy and Ally Hurtbox Types are allocated correctly
    }

    public bool CheckHit(Collider2D collider, HurtboxMask enemyHurtMask, List<HitEffect> effects)
    {
        //Debug.Log("Checking for hitmask match in " + this.name);
        if (enemyHurtMask == (HurtboxMask)hurtBoxType)
        {
            foreach (HitEffect e in effects)
            {
                e.PerformEffect(transform.parent.gameObject);//this assume the hurtbox is a child of the bot which has a bot controller
            }
            return true;
        }

        return false;
    }

    /* This function is called on collision of another hitbox.  */
    public void HitResponse(List<HitEffect> effects)
    {
        foreach(HitEffect effect in effects)
        {
            effect.PerformEffect(gameObject);
        }
    }

}
