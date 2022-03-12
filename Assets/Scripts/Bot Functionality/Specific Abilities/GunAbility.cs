using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Gun Ability", menuName = "Ability Objects/Attack Ability/Gun", order = 1)]

public class GunAbility : BotAbility
{
    public GameObject projectile;

    public float speed;

    [SerializeField] private float damage;
    [SerializeField] private Vector2 projectileSize = default(Vector2);

    public override void Activate(GameObject parent)
    {
       // Debug.Log("Shoot");

        // This is pretty ugly, TO-DO cache the sensor
        BotSensor sensor = parent.transform.parent.transform.parent.GetComponent<BotSensor>();
        int enemyDirection = sensor.GetNearestSensedBotDirection();

        Transform shootPos =parent.GetComponent<GunPosition>().shootPosition;
        GameObject projectileInstance = Instantiate(projectile, shootPos.position, Quaternion.identity);

        Projectile projectileGO = projectileInstance.GetComponent<Projectile>();
        projectileGO.SetValues(enemyDirection, damage, speed, projectileSize, sensor.GetEnemyLayer());
    }
}
