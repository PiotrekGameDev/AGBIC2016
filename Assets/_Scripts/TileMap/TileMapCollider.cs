using UnityEngine;
using System.Collections;

public class TileMapCollider : MonoBehaviour {

    public Tiles tiles;
    Collider2D col;

	// Use this for initialization
	void Start () {
        col = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 gridPosition = new Vector2(Mathf.RoundToInt(transform.position.x-transform.localScale.x/2), Mathf.RoundToInt(transform.position.y-transform.localScale.y / 2));
        col.enabled = tiles.tilesArray[(int)(gridPosition.x + gridPosition.y * 16)] != 0;
	}
}
