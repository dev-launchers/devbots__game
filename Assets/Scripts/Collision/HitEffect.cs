using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    
    public virtual void PerformEffect(GameObject hurtBot)
    {
        Debug.Log("effect produced");
    }
}
