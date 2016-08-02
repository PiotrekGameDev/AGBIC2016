using UnityEngine;
using System.Collections;

public class NodeSpawner : MonoBehaviour {

    private float nextSpawn = 0.0F;
    public float spawnRate =  2f;
    public Transform player;
    public Transform[] prefabs;
    Transform[] nodes;

    // Use this for initialization
    void Start () {
        nodes = transform.GetComponentsInChildren<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextSpawn && nodes.Length>0)
        {
            nextSpawn = Time.time+1/spawnRate;
            Transform spawnNode = nodes[Random.Range(0, nodes.Length)];
            Transform newObject = (Transform)Instantiate(prefabs[Random.Range(0, prefabs.Length)], spawnNode.position, spawnNode.rotation);
            newObject.SendMessage("SetTarget", player, SendMessageOptions.DontRequireReceiver);
        }
    }
}
