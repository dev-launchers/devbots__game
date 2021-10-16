using UnityEngine;

/// <summary>
///  This class is a type of botpart used on a bot. Lunges bot forward and creates a large sweep attack based on cooldown.
/// </summary>
public class SwordPart : BotPart
{

    [SerializeField] private float _attackDistance = default(float);
    [SerializeField] private float _damage = default(float);
    [SerializeField] private float _knockback = default(float);
    [SerializeField] private Vector2 _thrustForce = default(Vector2);
    [SerializeField] private bool _isRunning;   
    private Vector2 _attackPos = default(Vector2);
    private int _enemyLayer;

    private Animator _swordAnimator;//Animator used for sword rotation
    private Rigidbody2D _rb;
    private BotSensor _sensor;

    public override void SetState(State state) {
        _isRunning = state.isActive;
    }

    private void Start()
    {
        _swordAnimator = GetComponent<Animator>();
        _rb = GetComponentInParent<Rigidbody2D>();
        _sensor = GetComponentInParent<BotSensor>();
        _enemyLayer = _sensor.GetEnemyLayer();
        _timer = GetCoolDown();
    }

    public void AttackStep()
    {

        if (_isRunning)
        {
            if(!IsPartCoolingDown())        
            {
                ResetCooldownTimer();

                // Set trigger to play animation of sword rotating 
                _swordAnimator.SetTrigger("swordAttack");
                // add thrust to lunge bot forward 
                Vector2 appliedForce = new Vector2(_thrustForce.x * _sensor.GetNearestSensedBotDirection(), _thrustForce.y);
                _rb.AddRelativeForce(appliedForce, ForceMode2D.Impulse);

                _attackPos = transform.position + new Vector3(_sensor.GetNearestSensedBotDirection(), 0, 0);
                //Should be cleaned up, but currently creates Vector2 for current position + 1 in direction of enemy
                Collider2D collision = Physics2D.OverlapCircle(_attackPos, _attackDistance); 
                //Needs to attack only in front using swordPos

                if (collision.gameObject.layer == _enemyLayer)
                {
                    print("collision");
                    BotController collisionController = collision.transform.GetComponent<BotController>();
                    collisionController.TakeDamage(_damage);
                    collisionController.ApplyForce(new Vector2(_knockback * _sensor.GetNearestSensedBotDirection(),0));
                }
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        // Display the attack radius when selected
        Gizmos.color = Color.green;
        _attackPos = transform.position;
        if (Application.isPlaying)
        {
        _attackPos = transform.position + new Vector3(_sensor.GetNearestSensedBotDirection(), 0, 0);
        }


        Gizmos.DrawWireSphere(_attackPos, _attackDistance);

    }

    public override void BotPartUpdate()
    {
        AttackStep();
    }
}
