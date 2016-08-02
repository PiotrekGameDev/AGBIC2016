using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(PathFollower))]
public class PathFollowerEditor : Editor {

    public void OnSceneGUI()
    {
        PathFollower pf = (PathFollower)target;
        if (pf.nodes.Length == 0) pf.nodes = new Vector2[1];
        if(!EditorApplication.isPlaying)pf.nodes[0] = pf.transform.position;

        Handles.color = Color.white;

        EditorGUI.BeginChangeCheck();
        for (int n = 0; n < pf.nodes.Length; n++)
        {
            pf.nodes[n] = Handles.PositionHandle(pf.nodes[n], Quaternion.identity);
            if (n > 0)
            {
                Handles.DrawLine(pf.nodes[n], pf.nodes[n - 1]);
                //Handles.ConeCap(0, (pf.nodes[n]+pf.nodes[n - 1])/2, Quaternion.Euler(Vector2.Angle(pf.nodes[n], pf.nodes[n - 1])-90, 90,0), 0.5f);
            }
        }
        if (pf.loop)
        {
            Handles.DrawLine(pf.nodes[0], pf.nodes[pf.nodes.Length-1]);
        }
        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(target, "Changed Node");
        }
    }
}
