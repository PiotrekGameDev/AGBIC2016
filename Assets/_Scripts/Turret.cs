using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{

    public Transform player;
    ArcadeShooter shooter;

    void Start()
    {
        shooter = GetComponent<ArcadeShooter>();
        shooter.useInput = false;
        shooter.aim = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;
        shooter.shootDirection = (player.position - transform.position);
        shooter.shootDirection.Normalize();
        shooter.shoot = true;
    }
}
