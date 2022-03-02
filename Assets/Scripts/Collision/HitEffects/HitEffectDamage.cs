using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Damage Effect", menuName = "Hit Effect Objects/Damage")]

public class HitEffectDamage : HitEffect
{
    [SerializeField] private int damage;
    public override void PerformEffect(GameObject hurtBot)
    {
        base.PerformEffect(hurtBot);
        hurtBot.GetComponent<BotController>().TakeDamage(damage);
    }
}
