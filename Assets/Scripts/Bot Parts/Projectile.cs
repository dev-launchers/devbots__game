using UnityEngine;

/// <summary>
/// This class is used to control the projectile gameobject.
/// </summary>
public class Projectile : MonoBehaviour
{
    private float _damage;
    private float _speed;
    private int _enemyLayer;
    private int _enemyDirection;
    private Rigidbody2D _rb;
    //[SerializeField] private UnityEvent projectileCollisionEvent;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(new Vector2(_enemyDirection * _speed,0), ForceMode2D.Impulse);

    }

    // Update is called once per frame
    void Update()
    {
        //On collision enter 2d?
        //BotSensor hitSensor = collision.GetComponent<BotSensor>();
        //hitSensor.TakeDamage(damage);
        //hitSensor.GetPosition
        //Vector3 newPos = Calculate new position
        //hitSensor.TakeKnockback(newPos);
    }
    /// <summary>
    /// This method is used to set the values of the projectiles fields and its size
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="dmg"></param>
    /// <param name="spd"></param>
    /// <param name="size"></param>
    /// <param name="layer"></param>
    public void SetValues(int dir, float dmg, float spd, Vector2 size, int layer) {
        _enemyDirection = dir; //Set the direction of the projectile
        _damage = dmg; //Set the damage of the projectile
        _speed = spd; //Set the speed of the projectile
        gameObject.transform.localScale = size; //Set the projectile size
        _enemyLayer = layer; //Set the target of the projectile, so it only hits the desired bot, will likely need to be array of layers for self-damaging items 
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Check what layer collided game object is
        if (collision.gameObject.layer == _enemyLayer)
        {
            //event invoke for unity event. can add to in editor
            //projectileColisionEvent.Invoke();   

            //Deal damage to collided enemy
            collision.gameObject.GetComponent<BotController>().TakeDamage(_damage);
        }

        //Destroy projectile
        Destroy(this.gameObject);


    }

    //For an exploding bullet: 
    //Collider2D collision = Physics2D.OverlapCircle(new Vector2 (0,0), 1, "Bot"); 
    //when it explodes
}
