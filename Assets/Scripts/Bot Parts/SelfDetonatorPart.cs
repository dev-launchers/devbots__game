using UnityEngine;

/// <summary>
///  This class is a type of botpart used on a bot. Explodes when in close range of the enemy bot, dealing massive damage to opponent and some to self, and causes huge knockback.
/// </summary>
public class SelfDetonatorPart : BotPart
{
    //TODO: create "private Animator sideDetonatorAnimation;"
    // Animates the side detonator part when attack is active
    [SerializeField] private Transform _attackPoint;
    // References the attack point of the side detonator in the scene.

    [SerializeField] private float _attackRange = 0.0f;
    // Range for attack to initiate.
    
    [SerializeField] private float _knockBackStrength;
    [SerializeField] private float _upwardForce;
    [SerializeField] private LayerMask _enemyLayers;

    /// <summary>
    /// Inherited from BotPart
    /// </summary>
    /// <param name="state"></param>
    override public void SetState(State state)
    {
        return;
    }


    
    public void SelfDetonatorAttack()
    {
        // Detect enemy in range of attack.
        Collider2D enemyCollider2D = Physics2D.OverlapCircle(_attackPoint.position,
                                                   _attackRange,
                                                   _enemyLayers);
        if (enemyCollider2D)
        {
            if (!IsPartCoolingDown()){
                ResetCooldownTimer();

                Debug.Log(enemyCollider2D.name + " was attacked by self detonator part.");
                // TODO: Play the side detonator attack animation.
                // TODO: Implement damage to enemy health. (Use separate class?)
                // TODO: Implement small damage to player health. (Use separate class?)

                // Knockback opponent
                BotController controller = enemyCollider2D.GetComponentInParent<BotController>();
                BotSensor sensor = enemyCollider2D.GetComponentInParent<BotSensor>();
                if (controller != null)
                {
                    Vector2 direction = sensor.GetPosition() - transform.position;
                    controller.ApplyForce((direction.normalized * _knockBackStrength)
                                         +(new Vector2(0.0f,_upwardForce)));
                }
            }
        }
    }
    
    void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public override void BotPartUpdate()
    {
        SelfDetonatorAttack();
    }
}
