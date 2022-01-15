using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MineDropper Ability", menuName = "Ability Objects/Attack Ability/MineDropper")]
public class MineDropperAbility : BotAbility
{
    [SerializeField] private GameObject landMine;
    [SerializeField] private float damage;
    [SerializeField] private Vector3 projectileSize;
    public override void Activate(GameObject parent)
    {
        Transform mineLocation = parent.GetComponent<GunPosition>().shootPosition;
        //Create a landmine at the start position
        GameObject landmineInstance = Instantiate(landMine, mineLocation.transform.position, Quaternion.identity);
        BotSensor sensor = parent.GetComponentInParent<BotSensor>();
        int enemyLayer = sensor.GetEnemyLayer();
        //Fetch script/data for landmine
        Landmine projectileScript = landmineInstance.GetComponent<Landmine>();

        //Tells landmine values
        projectileScript.SetValues(damage, projectileSize, enemyLayer);

        // call a destroy function on landmineInstance

        //TODO: Set landmine knockback
    }
}
