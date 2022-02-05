using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Hit Effect", menuName = "Hit Effect Objects")]
public class HitEffect : ScriptableObject
{
    
    public virtual void PerformEffect(GameObject hurtBot)
    {
        Debug.Log("effect produced");
    }
}
