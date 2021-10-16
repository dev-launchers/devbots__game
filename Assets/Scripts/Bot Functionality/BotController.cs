using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is used to control the bot.
/// </summary>
public class BotController : MonoBehaviour
{
    private BotSensor _sensor;
    private AudioManager _audioManager;
    private Rigidbody2D _rb;
    [SerializeField] private float _HP = 1;
    public UnityEvent DamageTakenEvent;
    //class used to locate and change slots and the botparts which are on each slot
    public Slots Slots;

    /// <summary>
    /// Get this bot's current HP
    /// </summary>
    /// <returns></returns>
    public float GetGetHP()
    { 
        return _HP; 
    }

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void Start()
    {
        _sensor = GetComponent<BotSensor>();
        _audioManager = FindObjectOfType<AudioManager>();
        _rb = GetComponent<Rigidbody2D>();
        if (DamageTakenEvent == null)
            DamageTakenEvent = new UnityEvent();



    }

    public void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Combat" || currentScene == "General Testing Scene")
        {
            FaceEnemy();
        }
    }

    private void FaceEnemy()
    {
        //Get each child transform attachhed to this transform
        foreach (Transform childtransform in transform)
        {
            childtransform.localScale = new Vector3(_sensor.GetNearestSensedBotDirection(), 1, 1);
        }
    }

    public void SetPosition(Vector3 newPosition)
    {
        //The desired new position is sent by the attacking bot, but may be countered by certain effects
        _rb.position = newPosition;
    }

    public void ApplyForce(Vector3 force)
    {
        //The desired force is sent by the attacking bot, but may be countered by certain effects
        _rb.AddRelativeForce(force, ForceMode2D.Impulse);
    }

    public void PlayAudio(string audioName)
    {
        _audioManager.Play(audioName);
    }

    public void TakeDamage(float damage)
    {
        _HP -= damage;
        DamageTakenEvent.Invoke();
        if (_HP <= 0.0f)
        {
            //When health of bot is 0 or under destroy both bots
            Destroy(_sensor.GetNearestSensedBot());
            Destroy(gameObject);
            SceneManager.LoadScene(0);
            //audioManager.Play("Death");
            //animator.Play("death");
            //Make a new gameObject for dead hull, or disable scripts?
            //Instantiate(deathFX, transform.position, Quaternion.identity);
        }
        else
        {
            _audioManager.Play("Hit");
            //Instantiate(damageFX, transform.position, Quaternion.identity);
        }
    }
}
