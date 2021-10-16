using UnityEngine;

/// <summary>
/// This class is a type of botpart used on a bot. Creates upward and slight horizontal force towards enemy based on cooldown, similar to “flappy bird” style movement.
/// </summary>
public class WingsPart : BotPart
{
    [Tooltip("Amount of wing force to apply")]
    [SerializeField] private Vector2 _wingForce = default(Vector2);//Amount of wing force to apply
    [SerializeField] private bool _isRunning;
    private WheelPart _wheelPart;//Wheel part script attatched to this bot
    private TeleporterPart _teleporterPart;//Teleporter part attatched to this bot
    private Rigidbody2D _rb;
    private BotSensor _sensor;
    private BotController _controller;


    public void Start()
    {
        _rb = gameObject.GetComponentInParent<Rigidbody2D>();
        _sensor = GetComponentInParent<BotSensor>();
        _controller = GetComponentInParent<BotController>();
    }

    public void BackStep()
    {
        if (_isRunning)
        {
            if (!IsPartCoolingDown())
            {
                ResetCooldownTimer();
                //Use add relative force to rigidbody to thrust bot up and slightly forward. 
                Vector2 appliedForce = new Vector2(_wingForce.x * _sensor.GetNearestSensedBotDirection(), _wingForce.y);
                _rb.AddRelativeForce(appliedForce, ForceMode2D.Impulse);
                _controller.PlayAudio("Move");
            }
        }
    }

    public override void SetState(State state)
    {
        _isRunning = state.isActive;
    }

    public override void BotPartUpdate()
    {
        BackStep();
    }
}
