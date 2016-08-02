using UnityEngine;
using System.Collections;

public class PickableItem : MonoBehaviour {

    public int id;
    public Transform droppedBy;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(droppedBy!=null&&Vector2.Distance(transform.position, droppedBy.position) > 1)
        {
            droppedBy = null;
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (droppedBy==null)
        {
            ArcadeShooter arcShooter = other.GetComponent<ArcadeShooter>();
            if (arcShooter != null)
            {
                arcShooter.selectedObject = gameObject;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        ArcadeShooter arcShooter = other.GetComponent<ArcadeShooter>();
        if (arcShooter != null)
        {
            if (arcShooter.selectedObject == gameObject)
            {
                arcShooter.selectedObject = null;
            }
        }
    }
    void Interact(GameObject caller)
    {
        caller.SendMessage("Equip", id, SendMessageOptions.DontRequireReceiver);
        Destroy(gameObject);
    }
}
