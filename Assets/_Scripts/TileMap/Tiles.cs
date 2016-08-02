using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class Tiles : MonoBehaviour {

    [System.Serializable]
    public class IntRange
    {
        public IntRange(int mn, int mx)
        {
            min = mn;
            max = mx;
        }
        public bool InRange(int variable)
        {
            return (variable>=min)&&(variable< max);
        }
        public int min;
        public int max;
    }
    public Texture2D tileset;
    public Texture2D[] images;
    public int w;
    public int h;
    [SerializeField]
    [HideInInspector]
    public int[] tilesArray;
    public int tilesResolution = 5;
    public IntRange collidingTiles;
    ArrayList colliderFaces;
    public bool mesh;

    class Line {
        public Line(Vector2 nstart, Vector2 nend)
        {
            start = nstart;
            end = nend;
        }
        public Vector2 start;
        public Vector2 end;
    }

    // Use this for initialization
    void Start () {
        if (tilesArray == null || tilesArray.Length != w * h)
        {
            tilesArray = new int[w * h];
        }
        colliderFaces = new ArrayList();
        UpdateTiles();
        UpdateColliders();
    }

    public void UpdateTiles()
    {
        if (mesh) { UpdateMesh(); } else { UpdateTexture(); };
    }
	
	// Update is called once per frame
	public void UpdateTexture () {
        colliderFaces = new ArrayList();

        Texture2D newTexture = new Texture2D(w * tilesResolution, h * tilesResolution);
        Vector2 newScale = new Vector2(w, h);
        transform.localScale = newScale;
        transform.position = new Vector3(newScale.x / 2, newScale.y/2, transform.position.z);
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                Color [] colors = new Color[tilesResolution * tilesResolution];
                int tile = tilesArray[x + y * w];
                if (tile == 0)
                {
                    colors = new Color[tilesResolution * tilesResolution];
                    for (int i = 0; i < colors.Length; i++)
                    {
                        colors[i] = Color.clear;
                    }
                } else
                {
                    if (tileset != null)
                    {
                        int rowLength = tileset.width / tilesResolution;
                        int row = (tile - 1) / rowLength;
                        colors = tileset.GetPixels(((tile - 1) - rowLength * row) * tilesResolution, tileset.height - tilesResolution * (row + 1), tilesResolution, tilesResolution, 0);
                    }
                    else {
                        colors = images[tilesArray[x + y * w] - 1].GetPixels(0, 0, tilesResolution, tilesResolution, 0);
                    }
                    TileColliderData(x,y);
                }
                newTexture.SetPixels(x * tilesResolution, y * tilesResolution, tilesResolution, tilesResolution, colors, 0);
            }
        }
        newTexture.Apply(false);
        newTexture.filterMode = 0;
        GetComponent<Renderer>().sharedMaterial.mainTexture = newTexture;
    }

    private List<Vector3> newVertices = new List<Vector3>();
    private List<int> newTriangles = new List<int>();
    private List<Vector2> newUV = new List<Vector2>();
    private int squareCount = 0;

    void UpdateMesh()
    {
        squareCount = 0;
        colliderFaces = new ArrayList();
        transform.localScale = Vector2.one;
        Mesh mesh = new Mesh();

        transform.position = transform.localScale/2;
        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                int tile = tilesArray[x + y * w];
                if (tile != 0)
                {
                    GenerateTileMesh(x, y, tile);
                    TileColliderData(x, y);
                }
                
            }
        }
        mesh.Clear();
        mesh.vertices = newVertices.ToArray();
        mesh.uv = newUV.ToArray();
        mesh.triangles = newTriangles.ToArray();
        mesh.Optimize();
        mesh.RecalculateNormals();

        newVertices.Clear();
        newUV.Clear();
        newTriangles.Clear();
        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    void GenerateTileMesh(int x,int y, int tile)
    {
        newVertices.Add(new Vector3(x - 0.5f, y + 0.5f));
        newVertices.Add(new Vector3(x + 0.5f, y + 0.5f));
        newVertices.Add(new Vector3(x + 0.5f, y - 0.5f));
        newVertices.Add(new Vector3(x - 0.5f, y - 0.5f));

        newTriangles.Add((squareCount * 4) + 0);
        newTriangles.Add((squareCount * 4) + 1);
        newTriangles.Add((squareCount * 4) + 3);
        newTriangles.Add((squareCount * 4) + 1);
        newTriangles.Add((squareCount * 4) + 2);
        newTriangles.Add((squareCount * 4) + 3);

        int rowLength = tileset.width / tilesResolution;
        int row = (tile - 1) / rowLength;

        int ugrid = (tile - 1) - rowLength * row;
        int vgrid = row;

        float r = (float)tilesResolution/ (float)tileset.width;
        float c = (float)tilesResolution/ (float)tileset.height;

        newUV.Add(new Vector2(r*ugrid, 1-vgrid*c));
        newUV.Add(new Vector2(r*ugrid+r, 1-vgrid*c));
        newUV.Add(new Vector2(r*ugrid+r, 1-vgrid*c-c));
        newUV.Add(new Vector2(r*ugrid, 1-vgrid*c-c));
        squareCount++;
    }

    void TileColliderData(int x, int y)
    {
        if (collidingTiles.InRange(tilesArray[x + y * w]))
        {
            if (y < h - 1 && (tilesArray[x + (y + 1) * w] == 0 || !collidingTiles.InRange(tilesArray[x + (y + 1) * w]))) colliderFaces.Add(new Line(new Vector2(x, y + 1), new Vector2(x + 1, y + 1)));
            if (y > 0 && (tilesArray[x + (y - 1) * w] == 0 || !collidingTiles.InRange(tilesArray[x + (y - 1) * w]))) colliderFaces.Add(new Line(new Vector2(x + 1, y), new Vector2(x, y)));
            if (x > 0 && (tilesArray[(x - 1) + y * w] == 0 || !collidingTiles.InRange(tilesArray[(x - 1) + y * w]))) colliderFaces.Add(new Line(new Vector2(x, y), new Vector2(x, y + 1)));
            if (x < w - 1 && (tilesArray[(x + 1) + y * w] == 0 || !collidingTiles.InRange(tilesArray[(x + 1) + y * w]))) colliderFaces.Add(new Line(new Vector2(x + 1, y + 1), new Vector2(x + 1, y)));
        }
    }

    public void UpdateColliders()
    {
        if (colliderFaces!=null && colliderFaces.Count > 0) {
            EdgeCollider2D[] oldColliders = GetComponents<EdgeCollider2D>();
            for (int i = 0; i < oldColliders.Length; i++)
            {
                DestroyImmediate(oldColliders[i]);
            }
            while (colliderFaces.Count > 0)
            {
                GenerateCollider();
            }
        }
    }

    void GenerateCollider()
    {
        //Color colColor = new Color(Random.value, Random.value, Random.value);
        ArrayList nColliderFaces = new ArrayList();
        Line firstL = colliderFaces[0] as Line;
        nColliderFaces.Add(firstL);
        colliderFaces.RemoveAt(0);
        nColliderFaces.AddRange(FindNeighbors(firstL));
        if (firstL.start == (nColliderFaces[nColliderFaces.Count-1]as Line).end)
        {
            nColliderFaces.Add(firstL);
        }
        EdgeCollider2D col = gameObject.AddComponent<EdgeCollider2D>();
        Vector2[] nColPoints = new Vector2[nColliderFaces.Count+1];//Every line has two vectors
        if (mesh)
        {
            for (int i = 0; i < nColliderFaces.Count; i++)//Iterating through collider faces
            {
                nColPoints[i] = new Vector2((nColliderFaces[i] as Line).start.x, (nColliderFaces[i] as Line).start.y) - Vector2.one / 2;
            }
            nColPoints[nColPoints.Length - 1] = new Vector2((nColliderFaces[nColliderFaces.Count - 1] as Line).end.x, (nColliderFaces[nColliderFaces.Count - 1] as Line).end.y) - Vector2.one / 2;
        }
        else
        {
            for (int i = 0; i < nColliderFaces.Count; i++)//Iterating through collider faces
            {
                nColPoints[i] = new Vector2((nColliderFaces[i] as Line).start.x / w, (nColliderFaces[i] as Line).start.y / h) - Vector2.one / 2;
            }
            nColPoints[nColPoints.Length - 1] = new Vector2((nColliderFaces[nColliderFaces.Count - 1] as Line).end.x / w, (nColliderFaces[nColliderFaces.Count - 1] as Line).end.y / h) - Vector2.one / 2;
        }
        col.points = nColPoints;
    }

    ArrayList FindNeighbors(Line line)
    {
        ArrayList neighbors = new ArrayList();
        for (int i = 0; i < colliderFaces.Count; i++)
        {
            if (
                (colliderFaces[i] as Line).start == line.end
                
                )
            {
                Line foundN = colliderFaces[i] as Line;
                neighbors.Add(foundN);
                colliderFaces.RemoveAt(i);
                neighbors.AddRange(FindNeighbors(foundN));
            }
        }
        return neighbors;
    }
}

/*
Line[] sortedColliderFaces = new Line[nColliderFaces.Count];

        sortedColliderFaces[0] = nColliderFaces[0] as Line;
        foreach (Line face in nColliderFaces)
        {
            if (face.start == sortedColliderFaces[0].end)
            {
                sortedColliderFaces[1] = face;
                nColliderFaces.Remove(face);
                break;
            }
        }
        */