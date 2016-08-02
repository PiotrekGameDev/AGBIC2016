using UnityEngine;
using System.Collections;

public class MousePlayer : MonoBehaviour {

    Rigidbody2D body;
    Vector2 targetPosition;
    public float speed = 10;
    public bool lockY = false;

	// Use this for initialization
	void Start () {
        body = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D groundHit = Physics2D.Raycast(mousePosition, -Vector2.up);
        if (Input.GetMouseButtonDown(0)&&(groundHit.distance<0.75f||!lockY))
        {
            targetPosition = mousePosition;
        }
        if (lockY)
        {
            if (targetPosition.x - transform.position.x > 0.2)
            {
                body.velocity = new Vector2(speed, body.velocity.y);
            }
            else if (targetPosition.x - transform.position.x < -0.2)
            {
                body.velocity = new Vector2(-speed, body.velocity.y);
            }
            else
            {
                body.velocity = new Vector2(0, body.velocity.y);
            }
        }
        else {
            if ((targetPosition - (Vector2)transform.position).magnitude > 0.2)
            {
                body.velocity = (targetPosition - (Vector2)transform.position).normalized * speed;
            }
            else
            {
                body.velocity = Vector2.zero;
            }
        }
    }
}
