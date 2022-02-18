using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BotController : MonoBehaviour, IHurtResponder
{
    private BotSensor sensor;
    //private AudioManager audioManager;
    private Rigidbody2D rb;
    public UnityEvent DamageTakenEvent;
    [SerializeField] private float HP = 1;
    [SerializeField] private float deathAnimationTime = 0;
    //Set true if this bot should spawn random game objects for each slot
    [SerializeField] private bool startWithRandomBotParts = false;
    //This is used to get all botpart prefabs.
    [SerializeField] private AvailableBotPartsData availableBotPartsData = default(AvailableBotPartsData);
    //class used to locate and change slots and the botparts which are on each slot
    public Slots slots;
    //bool used to determine whether this bot has already been created
    public static bool created = false;

    private List<Bot_Hurtbox> m_hurtboxes = new List<Bot_Hurtbox>(); // If there are multiple hurtboxese per sprite, place this script in the most parent bot object.

    //Get this bot's current HP
    public float GetGetHP()
    {
        return HP;
    }


    public void OnEnable()
    {
        //Delegate used to trigger Onsceneloaded method when a new scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
        slots.Initialize();
        if (startWithRandomBotParts)
        {
        slots.SetSlotBotPart(SlotPosition.Back, availableBotPartsData.PickRandomBotPart(availableBotPartsData.backSlotBotParts));
        slots.SetSlotBotPart(SlotPosition.Top, availableBotPartsData.PickRandomBotPart(availableBotPartsData.topSlotBotParts));
        slots.SetSlotBotPart(SlotPosition.Bottom, availableBotPartsData.PickRandomBotPart(availableBotPartsData.bottomSlotBotParts));
        slots.SetSlotBotPart(SlotPosition.Side, availableBotPartsData.PickRandomBotPart(availableBotPartsData.sideSlotBotParts));
        }

    }

    public void Start()
    {
        sensor = GetComponent<BotSensor>();
        if (!created && sensor.IsPlayer())
        {
            //if this bot hasn't been created add it to dontdestroy on load
            DontDestroyOnLoad(this);
            created = true;
        }

        rb = GetComponent<Rigidbody2D>();
        if (DamageTakenEvent == null)
            DamageTakenEvent = new UnityEvent();

        m_hurtboxes = new List<Bot_Hurtbox>(GetComponentsInChildren<Bot_Hurtbox>());
        Debug.Log(this.gameObject.name + " Hurtresponder's start function");
        foreach (Bot_Hurtbox _hurtbox in m_hurtboxes)
        {
            _hurtbox.hurtResponder = this;
        }

    }

    public void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        if (currentScene == "Combat" || currentScene == "General Testing Scene")
        {
            FaceEnemy();
        }

    }
    /// <summary>
    /// Method called when a scene is loaded
    /// </summary>
    /// <param name="scene">the name of new scene that is loaded</param>
    /// <param name="loadSceneMode"></param>
    private void OnSceneLoaded(Scene scene,LoadSceneMode loadSceneMode)
    {
        rb = GetComponent<Rigidbody2D>();
        sensor = GetComponent<BotSensor>();
        if (sensor.IsPlayer())
        {
            //check new loaded scene's name
            switch (scene.name)
            {
                case "Main Menu Scene":
                case "Bot Customize Scene":
                case "Combat":


                    //activate this bot
                    gameObject.SetActive(true);

                    break;
                case "Marketplace Scene":
                case "Settings Scene":
                case "Victory Scene":
                case "Lose Scene":
                    gameObject.SetActive(false);
                    break;

            }
            if (scene.name == "Main Menu Scene" || scene.name == "Bot Customize Scene")
            {

                //set bot's position to the botStartPos gameobjects position
                transform.position = GameObject.Find("botStartPos").transform.position;
                //freeze this bots gameobject
                rb.constraints = RigidbodyConstraints2D.FreezeAll;


            }
            else
            {

                //unfreeze this bots gameobject
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;

            }
        }
    }

    private void FaceEnemy()
    {
        foreach (Transform childtransform in transform)
        {
            childtransform.localScale = new Vector3(sensor.GetNearestSensedBotDirection(), 1, 1);
        }

        // loop over the slots and perform that code for it
        foreach (GameObject childTransform in slots.GetSlotsList())
        {
            childTransform.transform.localScale = new Vector3(sensor.GetNearestSensedBotDirection(), 1, 1);
        }
    }

    public void SetPosition(Vector3 newPosition)
    {
        //The desired new position is sent by the attacking bot, but may be countered by certain effects
        rb.position = newPosition;
    }

    public void ApplyForce(Vector3 force)
    {
        //The desired force is sent by the attacking bot, but may be countered by certain effects
        Debug.Log("Applying force is active");
        rb.AddRelativeForce(force, ForceMode2D.Impulse);
    }

 /*   public void PlayAudio(string audioName)
    {
        //audioManager.Play(audioName);
    }
*/
    public void TakeDamage(float damage)
    {
        HP -= damage;
        DamageTakenEvent.Invoke();
        if (HP <= 0.0f)
        {
          

            //start botdestroyed coroutine when bot reaches zero health
            /* Commneted out to test collision */
            //StartCoroutine(BotDestroyed());

            //Destroy(sensor.GetNearestSensedBot());
            //Destroy(gameObject);


            //audioManager.Play("Death");
            //animator.Play("death");
            //Make a new gameObject for dead hull, or disable scripts?
            //Instantiate(deathFX, transform.position, Quaternion.identity);
        }
        else
        {
            //Instantiate(damageFX, transform.position, Quaternion.identity);
        }
    }
    public IEnumerator BotDestroyed()
    {
        //run death animation here and change deathAnimationTime in the inspector
  Debug.Log("dead");
        //delay to play animation before changing scene
        yield return new WaitForSeconds(deathAnimationTime);

        //check if bot desstroyed is the players or not then load appropiate scene
        if (sensor.IsPlayer())
        {
            SceneHandler.LoadLoseScene();
        }
        else
        {
            SceneHandler.LoadVictoryScene();
        }
    }

    public bool CheckHit(HitData hitData)
    {
        Debug.Log("Validating hit inside botcontroller");
        return true;
    }

    public void Response(HitData hitData)
    {
        Debug.Log(this.gameObject + " lost " + hitData.damage + " health!");
    }
}
