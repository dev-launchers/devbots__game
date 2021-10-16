using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///  This class is a type of botpart used on a bot. Deals damage to bots impacted by drill tip below this bot, damage scales with absolute value of downward velocity
/// </summary>
public class DrillBotPart : BotPart
{
    //TODO: create "private Animator drillAnimation;" // Animates the drill part when attack is active.

    [SerializeField] private Transform _attackPoint; // References the attack point of the drill in the scene.
    [SerializeField] private float _attackRange = 0.0f; // Range for attack to initiate.

    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private bool _isRunning;
    // Used to determine which objects are enemies by assigning all ememies to a layer using a layermask.

    /// <summary>
    /// Inherited from BotPart.
    /// </summary>
    /// <param name="state"></param>
    public override void SetState(State state)
    {
        _isRunning = state.isActive;
    }

    // Start is called before the first frame update
    void Start()
    {
        return;
    }

    
    public void DrillAttack()
    {
        if (_isRunning) {
            if(!IsPartCoolingDown()){
                ResetCooldownTimer();
                // TODO:  Play the drill attack animation.

                // Detect enemy in range of attack.
                Collider2D enemy = Physics2D.OverlapCircle(_attackPoint.position, _attackRange, _enemyLayers);

                // Damage enemy
                // TODO: Implement damage to enemy health.

                if(enemy)
                {
                    // Outputs message to Unity Editor Console to verify the attack.
                    Debug.Log(enemy.name + " was attacked by drill.");
                }
            }
        }
    }

    // Allows developer/user to see the attack radius in Unity editor.
    void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return; //Return if attackPoint has not been set.

        // Draw a wire sphere at attack position to show its range in Unity editor.
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }

    public override void BotPartUpdate()
    {     
        DrillAttack();
    }
}
