using UnityEngine;
using System.Collections;

public enum Touch { Nothing, Ground, Ceiling, Left, Right };
public class PlatformerPlayer : MonoBehaviour {

    public float jumpPower = 750f;
    public float speed = 6f;
    public float airControl = 0.1f;
    public bool canMove=true;
    public bool canJump=true;
    public bool canWallJump = true;
    public bool useInput = true;
    public bool faceDirection=true;

    Rigidbody2D body;
    Animator anim;

    public Touch ctouch;

    void Start () {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update () {
        if (useInput)
        {
            if (canMove) UpdateMovement(Input.GetAxis("Horizontal"));
            if (canJump && (Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))) Jump();
            if (canWallJump) UpdateWallJump();
        }
        if (ctouch == Touch.Nothing)
        {
            anim.SetInteger("state", 3);
        }
    }

    public void UpdateMovement(float xAxis)
    {
        if (
            (ctouch != Touch.Right && ctouch != Touch.Left)
            || (xAxis < 0 && ctouch == Touch.Right)
            || (xAxis > 0 && ctouch == Touch.Left))
            {
            if (anim != null)
            {
                if (xAxis != 0 && ctouch!=Touch.Nothing)
                {
                    anim.SetInteger("state", 1);
                }
                else
                {
                    anim.SetInteger("state", 0);
                }
            }
            if (ctouch == Touch.Ground)
            {
                body.velocity = new Vector2(xAxis * speed, body.velocity.y);
            }
            else if(xAxis!=0)
            {
                body.velocity = new Vector2(xAxis*speed*airControl + body.velocity.x*(1-airControl), body.velocity.y);
            }
            if(faceDirection && ctouch != Touch.Right && ctouch != Touch.Left){
                if (((xAxis < 0 && transform.localScale.x>0)
                || (xAxis > 0 && transform.localScale.x<0))) {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }
        }
        else
        {
            anim.SetInteger("state", 0);
        }
    }

    public void Jump()
    {
        if (ctouch == Touch.Ground)
        {
            ctouch = Touch.Nothing;
            body.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        }
    }
    void UpdateWallJump()
    {
        if ((ctouch == Touch.Left || ctouch == Touch.Right) && ( Input.GetButton("Jump") || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) ) )
        {
            anim.SetInteger("state", 2);
            if (Input.GetAxis("Horizontal") < 0 && ctouch == Touch.Right)
            {
                body.drag = 0;
                body.AddForce(new Vector2(-1, 1) * jumpPower, ForceMode2D.Impulse);
                ctouch = Touch.Nothing;
            }
            else if (Input.GetAxis("Horizontal") > 0 && ctouch == Touch.Left)
            {
                body.drag = 0;
                body.AddForce(new Vector2(1, 1) * jumpPower, ForceMode2D.Impulse);
                ctouch = Touch.Nothing;
            }
            else
            {
                body.drag = 300;
                if (((ctouch == Touch.Right && transform.localScale.x > 0)
                || (ctouch == Touch.Left && transform.localScale.x < 0))
                && faceDirection)
                {
                    transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }
        }
        else
        {
            body.drag = 0;
        }
    }

    void FixedUpdate()
    {

    }

    bool grounded = false;
    void OnCollisionEnter2D (Collision2D col)
    {
        foreach (ContactPoint2D contact in col.contacts)
        {
            if (contact.normal.y > 0)
            {
                ctouch = Touch.Ground;
                grounded = true;
            }
            if (contact.normal.y < 0)
            {
                ctouch = Touch.Ceiling;
                grounded = false;
            }
            if (contact.normal.x>0 && ctouch != Touch.Ground && ctouch != Touch.Ceiling)
            {
                ctouch = Touch.Left;
            }
            if (contact.normal.x < 0 && ctouch != Touch.Ground && ctouch != Touch.Ceiling)
            {
                ctouch = Touch.Right;
            }
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        ctouch = Touch.Nothing;
        if (grounded)
        {
            foreach (ContactPoint2D contact in col.contacts)
            {
                if (grounded && (contact.normal.x > 0 || grounded && contact.normal.x < 0))
                {
                    ctouch = Touch.Ground;
                }
            }
        }
    }
}
