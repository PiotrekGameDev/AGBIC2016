using UnityEngine;
using System.Collections;

public class ArcadeShooter : MonoBehaviour {

    private float nextFire = 0.0F;
    public Rigidbody2D bulletPrefab;
    public float bulletKillTime = 1;
    public float bulletSpeed = 1;
    public bool shoot = false;
    public bool useInput=true;
    public bool faceMouse;
    [HideInInspector]
    public Vector2 shootDirection;

    public Weapon[] weapons;
    public int weapon = 0;
    public PickableItem itemPrefab;

    public bool aim;
    public Transform hand;

    public enum EquipMethod { Touch, TouchAndButton, TouchAndClick };
    public EquipMethod equipMetod;
    public int mouseButtonIndex = 0;
    public string buttonName = "e";
    public GameObject selectedObject;

    // Use this for initialization
    void Start () {
        transform.GetChild(0).GetChild(weapon).gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.timeScale == 0) return;
        if (useInput)
        {
            shoot = ((weapons[weapon].isAutomatic && (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Z))) ||
                    (!weapons[weapon].isAutomatic && (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Z))));
        }
        if (useInput)
        {
            Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (aim)
            {
                shootDirection = (cameraPosition - transform.position);
                shootDirection.Normalize();
                if (transform.localScale.x < 0)
                {
                    hand.right = new Vector2(transform.position.x - cameraPosition.x, cameraPosition.y - transform.position.y);
                }
                else
                {
                    hand.right = (Vector2)(cameraPosition - transform.position);
                }
            }
        }
        else if(hand!=null){
            hand.right = shootDirection;
        }
        if (faceMouse)
        {
            if ((shootDirection.x > 0 && transform.localScale.x < 0) ||
                   (shootDirection.x < 0 && transform.localScale.x > 0))
            {
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        if (!aim)
        {
            if (transform.localScale.x < 0)
            {
                shootDirection = Vector2.left;
            }
            else
            {
                shootDirection = Vector2.right;
            }
        }
        if (shoot && Time.time > nextFire && weapons[weapon].type == TypeOfWeaopn.Gun)
        {
            nextFire = Time.time + 1 / weapons[weapon].rateOfFire;
            ShootBullet();
        }
        if (selectedObject != null)
        {
            switch (equipMetod)
            {
                case EquipMethod.Touch:
                    selectedObject.SendMessage("Interact", gameObject, SendMessageOptions.DontRequireReceiver);
                    break;
                case EquipMethod.TouchAndButton:
                    if (Input.GetKeyDown(buttonName))
                    {
                        selectedObject.SendMessage("Interact", gameObject, SendMessageOptions.DontRequireReceiver);
                    }
                    break;
                case EquipMethod.TouchAndClick:
                    if (Vector2.Distance(Camera.main.ScreenToWorldPoint(Input.mousePosition), selectedObject.transform.position) < 1 && Input.GetMouseButtonDown(mouseButtonIndex))
                    {
                        selectedObject.SendMessage("Interact", gameObject, SendMessageOptions.DontRequireReceiver);

                    }
                    break;
            }
        }
    }

    void ShootBullet()
    {
        Vector2 bulletPosition;
        bulletPosition = (Vector2)transform.position + shootDirection*1.5f;
        
        //Quaternion bulletRotation = Quaternion.AngleAxis(Vector2.Angle(transform.right, shootDirection), transform.forward);
        Rigidbody2D newBullet = (Rigidbody2D)Instantiate(bulletPrefab, bulletPosition, Quaternion.identity);
        newBullet.transform.right = shootDirection;
        if (transform.localScale.x < 0)
        {
            newBullet.transform.localScale = new Vector3(-newBullet.transform.localScale.x, newBullet.transform.localScale.y, newBullet.transform.localScale.z);
        }
       newBullet.AddForce(shootDirection * bulletSpeed, ForceMode2D.Impulse);
        
        Destroy(newBullet.gameObject, bulletKillTime);
    }

    public void Equip(int itemID)
    {
            PickableItem dropped = (PickableItem)Instantiate(itemPrefab, transform.position, Quaternion.identity);
            dropped.id = weapon;
            if(equipMetod==EquipMethod.Touch)dropped.droppedBy = transform;
            transform.GetChild(0).GetChild(weapon).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(weapon = itemID).gameObject.SetActive(true);
        
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (shoot && Time.time > nextFire && weapons[weapon].type == TypeOfWeaopn.Melee)
        {
            nextFire = Time.time + 1 / weapons[weapon].rateOfFire;
            other.gameObject.SendMessage("Damage", weapons[weapon].damagePerShot, SendMessageOptions.DontRequireReceiver);
        }
    }
}

/*TODO:
Vector2 mouse = Camera.main.ScreenToWorldPoint (Input.mousePosition)-transform.position;
		transform.GetChild(0).up = mouse;
*/
