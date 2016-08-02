using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;

[CustomEditor(typeof(Tiles))]
public class TilesEditor : Editor
{
    string[] toolbarStrings = new string[] { "Paint", "Erase" };
    int toolbarInt = 0;
    int selectedTile = 1;
    Texture2D preview;

    public override void OnInspectorGUI()
    {
        Tiles ts = (Tiles)target;
        DrawDefaultInspector();
        if (ts.tileset != null)
        {
            selectedTile = EditorGUILayout.IntSlider("Tile Index", selectedTile, 1, (ts.tileset.width / ts.tilesResolution) * (ts.tileset.height / ts.tilesResolution));
        }
        else if (ts.images.Length > 0)
        {
            selectedTile = EditorGUILayout.IntSlider("Tile Index", selectedTile, 1, ts.images.Length);
        }
        if (ts.tileset != null)
        {
            preview = new Texture2D(ts.tilesResolution*4, ts.tilesResolution*4);
            int rowLength = ts.tileset.width / ts.tilesResolution;
            int row = (selectedTile - 1) / rowLength;
            Color[] colors = ts.tileset.GetPixels(((selectedTile - 1) - rowLength * row) * ts.tilesResolution, ts.tileset.height - ts.tilesResolution * (row + 1), ts.tilesResolution, ts.tilesResolution, 0);
            //Color[] resized = new Color[(ts.tilesResolution * 2) * (ts.tilesResolution * 2)];
            int rx = 0;
            int ry = 0;
            for (int x = 0; x < ts.tilesResolution; x++)
            {
                for (int y = 0; y < ts.tilesResolution; y++)
                {
                    preview.SetPixel(rx, ry, colors[x+y*ts.tilesResolution]);
                    ry ++;
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                }
                rx++;
                for (int y = 0; y < ts.tilesResolution; y++)
                {
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                }
                rx++;
                for (int y = 0; y < ts.tilesResolution; y++)
                {
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                }
                rx++;
                for (int y = 0; y < ts.tilesResolution; y++)
                {
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                    preview.SetPixel(rx, ry, colors[x + y * ts.tilesResolution]);
                    ry++;
                }
                rx++;
            }
            preview.Apply();
            //preview.SetPixels(resized);
            GUILayout.Label(preview);
        }
        else
        {
            GUILayout.Label(ts.images[0]);
        }
        toolbarInt = GUILayout.Toolbar(toolbarInt, toolbarStrings);
        if (GUILayout.Button("Update Collider")) ts.UpdateColliders();
        if (GUILayout.Button("Clear"))
        {
            ts.tilesArray = new int[ts.w*ts.h];
            for (int x = 0; x < ts.w; x++)
            {
                for (int y = 0; y < ts.h; y++)
                {
                    ts.tilesArray[x + y * ts.w] = 0;
                }
            }
            ts.UpdateTexture();
        }
    }

    public void OnSceneGUI()
    {
        Tiles ts = (Tiles)target;
        Vector2 newMousePosition = Event.current.mousePosition;
        newMousePosition.y = Screen.height - (Event.current.mousePosition.y + 50);
        Vector2 mouseP = Camera.current.ScreenToWorldPoint(newMousePosition);
        HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));
        Vector2 gridPosition = new Vector2(Mathf.RoundToInt(mouseP.x), Mathf.RoundToInt(mouseP.y));
        if ((Event.current.type == EventType.MouseDrag || Event.current.type == EventType.mouseDown) && Event.current.button == 0)
        {
            Rect tileMapBounds = new Rect(0, 0, ts.w, ts.h);
            if (tileMapBounds.Contains(gridPosition))
            {
                if (toolbarInt == 0)
                {
                    ts.tilesArray[(int)gridPosition.x+ (int)gridPosition.y*ts.w] = selectedTile;
                } else
                {
                    ts.tilesArray[(int)gridPosition.x+ (int)gridPosition.y*ts.w] = 0;
                }
                EditorUtility.SetDirty(target);
                ts.UpdateTiles();
            }
        }
    }
}
