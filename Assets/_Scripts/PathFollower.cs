using UnityEngine;
using System.Collections;
using UnityEditor;

public class PathFollower : MonoBehaviour {

    //public bool setPostionAsFirstNode = true;
    public Vector2[] nodes;
    int n=0;
    public float speed = 0.1f;
    int dir=1;
    public bool loop;

    // Use this for initialization
    void Start () {
        /*
        int firstNode = setPostionAsFirstNode ? 1 : 0;
        nodes = new Vector2[transform.childCount+firstNode];
        if(setPostionAsFirstNode) nodes[0] = transform.position;
        for (int c = 0; c < transform.childCount; c++)
        {
            nodes[c+firstNode] = transform.GetChild(c).position;
            transform.GetChild(c).gameObject.SetActive(false);
        }
        */
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, nodes[n], speed*Time.deltaTime);
        if ((Vector2)transform.position == nodes[n])
        {
            if (n == nodes.Length-1||(dir<0&&n==0))
            {
                if (loop)
                {
                    n =-dir;
                }
                else {
                    dir *= -1;
                }
            }
            n+=dir;
        }
	}
}
