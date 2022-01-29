using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Flags]
public enum DamageType
{
    None = 0,      // 00000b
    Fire = 1 << 0, // 00001b
    Slashing = 1 << 1, // 00010b
    Ice = 1 << 2,  // 00100b
    Blunt = 1<<3, //01000b
    Piercing = 1<<4 //10000b
}

public class StatusContainer : MonoBehaviour
{
   [SerializeField] DamageType type = DamageType.None;
    List<HitEffect> _hitEffects;
    float health = 50;
        
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HandleCollision(Hitbox hitBox,Hurtbox hurtbox)
    {
        hurtbox.HitResponse(_hitEffects);
    }

    void PopulateList(List<HitEffect> hitEffects)
    {
        _hitEffects = hitEffects;
    }

    void AddEffect(HitEffect hitEffect)
    {
        _hitEffects.Add(hitEffect);
    }
    
    void RemoveEffect(HitEffect hitEffect)
    {
        _hitEffects.Remove(hitEffect);
    }
}
