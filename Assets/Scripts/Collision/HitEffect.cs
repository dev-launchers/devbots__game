using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "HitEffectTest", menuName = "ScriptableObjects/HitEffects", order = 1)]
public class HitEffect : ScriptableObject
{
    public virtual void PerformEffect(GameObject hurtBot)
    {
        Debug.Log("effect produced");
    }
}
