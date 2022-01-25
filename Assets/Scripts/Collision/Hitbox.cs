using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    [SerializeField] private GameObject owner;
    [SerializeField] private Collider2D collider;
    [SerializeField] private HurtboxMask hurtMask;
    [SerializeField] private float radius = 0;
    [SerializeField] private List<HitEffect> effects;
    

    // [SerializeField] private hitEffects<> hitEffects;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Hurtbox hurtbox = collision.gameObject.GetComponentInChildren<Hurtbox>();
        Debug.Log("Collided");

        if (hurtbox == null) return;
        
        Debug.Log("Successfull hit" + collider.name);
        hurtbox.CheckHit(collider, hurtMask, effects);
        
    }

    private void CheckHit()
    {
        Physics2D.OverlapCircleAll(collider.transform.position, radius);
    }
}
