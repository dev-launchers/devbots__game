using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent( typeof(Rigidbody2D), typeof(BoxCollider2D) )]

public class Hitbox : MonoBehaviour
{
    [SerializeField] private GameObject owner;
    [SerializeField] private Collider2D collider;
    [SerializeField] private HurtboxMask hurtMask;
    [SerializeField] private float radius = 0;
    [SerializeField] private List<HitEffect> effects;
    [SerializeField] private float timeBetweenCollisions;
    private bool canHit=true;

    // [SerializeField] private hitEffects<> hitEffects;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canHit) return;
        //Debug.Log("Collided");
        Hurtbox hurtbox = collision.gameObject.GetComponentInChildren<Hurtbox>();
        if (hurtbox == null) hurtbox = collision.gameObject.GetComponent<Hurtbox>();// in case the hurtbox is not on the child.
        if (hurtbox == null) return;
        
        Debug.Log("Successfull hit on" + collider.name);
        hurtbox.CheckHit(collider, hurtMask, effects);
        canHit = false;
        Invoke(nameof(ResetHit),timeBetweenCollisions);
    }

    private void ResetHit()//we turn off can Hit for a period of time to prevent multiple collisions in a succession of frames
    {
        canHit = true;
    }
    
    private void CheckHit()
    {
        Physics2D.OverlapCircleAll(collider.transform.position, radius);
    }
    
    
}
