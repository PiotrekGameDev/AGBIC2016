using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class Killable : MonoBehaviour {

    public int hp = 100;
    private int currentHP;
    public Transform corpsePrefab;
    [System.Serializable]
    public class Loot
    {
        public Transform prefab;
        public float frequency = 0f;
    }
    public Loot[] lootList;
    public UnityEvent onDeath;

    void Start()
    {
        currentHP = hp;
    }

    void Update()
    {
        if (transform.position.y < -1000)
        {
            Damage(100);
        }
    }

    void Damage(int amount)
    {
        currentHP -= amount;
        if (currentHP <= 0)
        {
            if (corpsePrefab != null)
            {
                Instantiate(corpsePrefab, transform.position, Quaternion.identity);
            }
            foreach (Loot loot in lootList)
            {
                if (Random.value < loot.frequency)
                {
                    Instantiate(loot.prefab, transform.position, Quaternion.identity);
                }
            }
            onDeath.Invoke();
            Destroy(gameObject);
        }
    }

    void Heal(int amount)
    {
        if (currentHP + amount < hp)
        {
            currentHP += amount;
        }
        else
        {
            currentHP = hp;
        }
    }
}
