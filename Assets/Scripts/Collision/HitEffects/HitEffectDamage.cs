using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Damage Effect", menuName = "Hit Effect Objects/Damage")]

public class HitEffectDamage : HitEffect
{
    [SerializeField] private float damage;
    public override void PerformEffect(GameObject hurtBot)
    {
        //base.PerformEffect(hurtBot);
        Debug.Log("Damage has been done");
	hurtBot.GetComponent<BotController>().TakeDamage(damage);
    }
}
