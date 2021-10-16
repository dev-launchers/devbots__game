using UnityEngine;
/// <summary>
/// This class is a type of botpart used on a bot. Fires projectiles based on cooldown which deal damage when impacting with enemy bots.
/// </summary>
public class GunPart : BotPart
{

    [SerializeField] private int _enemyLayer = default(int);
    [SerializeField] private float _damage = default(float);
    [SerializeField] private GameObject _projectile = default(GameObject); //Object to be fired by gun part
    [SerializeField] private float _projectileSpeed = default(float);
    [SerializeField] private GameObject _projectileStartPos;
    [SerializeField] private Vector2 _projectileSize = default(Vector2);
    [SerializeField] private bool _isRunning;
    private BotSensor _sensor;
    private BotController _controller;


        // Start is called before the first frame update
    void Start()
    {
        _sensor = GetComponentInParent<BotSensor>();
        _controller = GetComponentInParent<BotController>();
        _enemyLayer = _sensor.GetEnemyLayer();
        _timer = GetCoolDown();
    }

    public override void SetState(State state)
    {
        _isRunning = state.isActive;
    }

    public void AttackStep()
    {

        if (_isRunning) {

            if (!IsPartCoolingDown()){
                ResetCooldownTimer();
                

                int enemyDirection = _sensor.GetNearestSensedBotDirection();
    
                //Faces attack at enemy, handled as local position to bot part

                GameObject projectileInstance = Instantiate(_projectile, _projectileStartPos.transform.position, Quaternion.identity);
                //Create a projectile at the start position

                Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
                //Fetch script/data for projectile

                projectileScript.SetValues(enemyDirection, _damage, _projectileSpeed, _projectileSize, _enemyLayer);
                //Tells projectile values

                //TODO: Set projectile knockback

                _controller.PlayAudio("Hit");
            }
        }
    }

    public override void BotPartUpdate()
    {
        AttackStep();
    }
}
