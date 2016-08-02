using UnityEngine;
using System.Collections;

public class PlatformerCamera : MonoBehaviour {

    public PlatformerPlayer player;
    public float speed=1;
    public bool checkIfGrounded = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (player != null)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = player.transform.position.x;
            if (player.ctouch == Touch.Ground || !checkIfGrounded)
            {
                newPosition.y = player.transform.position.y;
            }
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * speed);
        }
	}
}
