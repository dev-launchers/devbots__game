using UnityEngine;

/// <summary>
///  This class is a type of botpart used on a bot. Lunges bot forward and creates a large sweep attack based on cooldown.
/// </summary>
public class TailFinPart : BotPart
{ 
    [SerializeField] private bool isRunning;
    [Tooltip("Amount of backforce to apply")]
    [SerializeField] private Vector2 _backThrust = default(Vector2);//Amount of backforce to apply
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
        //wheelPart = transform.parent.GetComponentInChildren<WheelPart>();
        //teleporterPart = transform.parent.GetComponentInChildren<TeleporterPart>();
        _timer = GetCoolDown();  
    }

    public void BackStep()
    {
        if (isRunning)
        {
            if (!IsPartCoolingDown())
            {
                ResetCooldownTimer();

                //Use add relative force to rigidbody to thrust bot backwards. 
                Vector2 appliedForce = new Vector2(-_backThrust.x * _sensor.GetNearestSensedBotDirection(), _backThrust.y);
                _rb.AddRelativeForce(appliedForce, ForceMode2D.Impulse);
                _controller.PlayAudio("Move");
            }
        }
    }

    public override void SetState(State state)
    {
        isRunning = state.isActive;
    }

    public override void BotPartUpdate()
    {        
        BackStep();
    }
}
