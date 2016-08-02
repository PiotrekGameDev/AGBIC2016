using UnityEngine;
using System.Collections;

public class ShopItem : MonoBehaviour {

    public int id;
    public int price;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        ArcadeShooter arcShooter = other.GetComponent<ArcadeShooter>();
        if (arcShooter != null)
        {
            arcShooter.selectedObject = gameObject;
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
        PointsCounter pointsC = caller.GetComponent<PointsCounter>();
        if (pointsC!=null)
        {
            if (pointsC.points >= price)
            {
                pointsC.points -= price;
                caller.SendMessage("Equip", id, SendMessageOptions.DontRequireReceiver);
                Destroy(gameObject);
            }
        }
    }
}
