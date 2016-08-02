using UnityEngine;
using System.Collections;

public class EnemyBrain : MonoBehaviour {

    PlatformerPlayer motor;
    ArcadeShooter shooter;
    public int dir = 1;
    public Vector2 cliffRayOffset=new Vector2(0.75f, -0.5f);
    public Transform player;
    public Vector2 playerDetectorOffset = new Vector2(1.2f, 0);
    public float playerDetectorRange = 5f;
    public float playerDetectorAngle = 30f;

    public bool patrolling = true;
    public bool seePlayer = false;
    public float chasingTime = 20f;
    float toPatrol = 0f;
    float lastJump = 0f;

	// Use this for initialization
	void Start () {
        motor = GetComponent<PlatformerPlayer>();
        motor.useInput = false;
        shooter = GetComponent<ArcadeShooter>();
        shooter.useInput = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (player == null)
        {
            shooter.shoot = false;
            return;
        }
        bool lastCheck = seePlayer;
        if (dir == 0 && patrolling)
        {
            if (transform.localScale.x > 0)
            {
                dir = 1;
            }
            else
            {
                dir = -1;
            }
        }
        Vector2 playerDetectorOrigin = (Vector2)transform.position + new Vector2(playerDetectorOffset.x * dir, playerDetectorOffset.y);

        if (Vector2.Angle(Vector2.right * dir, (Vector2)player.transform.position-playerDetectorOrigin) < playerDetectorAngle) {
            RaycastHit2D playerDetectorHit = Physics2D.Raycast(playerDetectorOrigin, player.transform.position - transform.position, playerDetectorRange);
            Debug.DrawRay(playerDetectorOrigin, (Vector2)player.transform.position - (Vector2)playerDetectorOrigin, Color.red);
            seePlayer = (playerDetectorHit.transform == player && playerDetectorHit);
        }
        else
        {
            seePlayer = false;
        }
        if(Vector2.Distance(transform.position, player.transform.position) < 1)
        {
            seePlayer=true;
        }
        if (seePlayer)
        {
            patrolling = false;
        }
        else if(lastCheck)
        {
            toPatrol = Time.time + chasingTime;
        }
        else if(toPatrol<=Time.time)
        {
            patrolling = true;
        }
        if (patrolling)
        {
            UpdatePatrol();
        }
        else
        {
            UpdateChase();
        }
        motor.UpdateMovement(dir);
    }

    void UpdatePatrol()
    {
        shooter.shoot = false;
        Vector2 cliffRayOrigin = (Vector2)transform.position + new Vector2(cliffRayOffset.x * dir, cliffRayOffset.y);
        RaycastHit2D cliffhHit = Physics2D.Raycast(cliffRayOrigin, -Vector2.up, 10);
        Debug.DrawRay(cliffRayOrigin, -Vector2.up * cliffhHit.distance, Color.blue);
        if ((cliffhHit.distance > 2 || !cliffhHit) && motor.ctouch == Touch.Ground)
        {
            dir *= -1;
        }
    }

    void UpdateChase()
    {
        if (seePlayer)
        {
            shooter.shoot = true;
        }
        else
        {
            shooter.shoot = false;
        }
        if (transform.position.x - player.position.x > 1.5&&transform.position.y-player.position.y<1)
        {
            dir = -1;
        }
        else if (transform.position.x - player.position.x < -1.5 && transform.position.y - player.position.y < 1)
        {
            dir = 1;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (player == null) return;
        if (patrolling&&motor.ctouch==Touch.Ground&&!(other.transform.parent!=null&& other.transform.parent.gameObject.layer==9))
        {
            dir *= -1;
        }
        else if(!patrolling&&other.transform!=player&&other.transform.parent!=player&&motor.ctouch==Touch.Ground&&!seePlayer&&Time.time-lastJump>1)
        {
            motor.Jump();
            lastJump = Time.time;
            motor.ctouch = Touch.Nothing;
        }
    }

    public void SetTarget(Transform target)
    {
        player = target;
    }
}
