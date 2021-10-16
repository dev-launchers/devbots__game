using UnityEngine;

/// <summary>
/// This class is is used to provide sensory methods for the bot.
/// </summary>
public class BotSensor : MonoBehaviour
{
    private GameObject _nearestBot;
    private GameObject[] _activeBots;
    private AudioManager _audioManager;
    private Rigidbody2D _rb;
    private int _enemyLayer;
    private bool _isPlayer;

    public void Awake()
    {
        _activeBots = GameObject.FindGameObjectsWithTag("Bot");
        _rb = GetComponent<Rigidbody2D>();
        SenseStep(); //In multi-bot fights, needs to be called in Update

        if (gameObject.layer == 9) { //This bot is Player
            _isPlayer = true;
            _enemyLayer = 10;
        }
        else { //This bot is Opponent
            _isPlayer = false;
            _enemyLayer = 9;
        }
    }

    public int GetEnemyLayer()
    {
        return _enemyLayer;
    }

    public GameObject GetNearestSensedBot() 
    {
        UpdateActiveBots();
        return _nearestBot;
    }

    public Vector2 GetNearestSensedBotPosition() 
    {
        UpdateActiveBots();
        return _nearestBot.transform.position;
    }

    /// <summary>
    /// Returns -1 if left, and 1 if right
    /// </summary>
    /// <returns></returns>
    public int GetNearestSensedBotDirection()
    {
        UpdateActiveBots();
        int enemyDirection = 1;
        //Find if enemy to the left or right
        if (gameObject.transform.position.x > _nearestBot.transform.position.x) {
            enemyDirection = -1;
        }
        return enemyDirection;
    }


    /// <summary>
    ///  GetNearestSensedBotAbove returns 1 if nearestBot is above,
   ///    and -1 if below.
    /// @param xPosBuffer is a valid float representing the maximum distance
    ///    from the left or right of the players center mass.
    /// @param yPosBuffer is a valid float representing the minimum distance
    ///    above player center mass for enemy bot to be considerd above.
    /// </summary>
    /// <param name="xMaxPos"></param>
    /// <param name="yMinPos"></param>
    /// <returns></returns>
    public int GetNearestSensedBotAbove(float xMaxPos, float yMinPos)
    {
        int enemyBotAbove = -1; //Enemy bot is NOT above player bot. 
        float playerYPos = gameObject.transform.position.y;
        float playerXPos = gameObject.transform.position.x;
        float enemyYPos  = _nearestBot.transform.position.y;
        float enemyXPos  = _nearestBot.transform.position.x;

        UpdateActiveBots();
        
        // Verify enemy is within the maximum X range distance from player.
        if(enemyXPos >= playerXPos - xMaxPos && enemyXPos <= playerXPos + xMaxPos)
        {
            // Verify enemy is above the minimum domain distance from player.
            if (enemyYPos >= playerYPos + yMinPos)
            {
                enemyBotAbove = 1; // Enemy bot is above player bot.
            }
        }
        return enemyBotAbove;
    }

    public void SenseStep() 
    {
        //Updates the current "Nearest Bot," always the enemy in 1v1, closest enemy in multibot
        foreach(GameObject activeBot in _activeBots) {
            if (activeBot != this.gameObject) {
                _nearestBot = activeBot;
            }
        }
    }

    private void UpdateActiveBots() 
    {
        if(_nearestBot == null) { //This is a temporary workaround and needs to be fixed so that the sensor activates and gets bot list at proper time
            _activeBots = GameObject.FindGameObjectsWithTag("Bot");
            SenseStep();
        }
    }
    
    public Vector3 GetPosition() 
    {
        return _rb.position;
    }

    public bool IsPlayer() 
    {
        return _isPlayer;
    }
}
