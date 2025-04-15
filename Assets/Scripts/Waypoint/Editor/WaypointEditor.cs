using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Waypoint))]

public class WaypointEditor : Editor
{
    private Waypoint WaypointTarget => target as Waypoint;

    private void OnSceneGUI()
    {
        if (WaypointTarget.Points.Length <= 0f) return;

        Handles.color = Color.red;
        for (int i = 0; i < WaypointTarget.Points.Length; i++) // loop through points inside inspector
        {
            EditorGUI.BeginChangeCheck();

            Vector3 currentPoint = WaypointTarget.EntityPosition + WaypointTarget.Points[i];
            Vector3 newPosition = Handles.FreeMoveHandle(currentPoint, 0.5f, Vector3.one * 0.5f, Handles.SphereHandleCap); // create a handle inside a position and able to move it

            // creating text next to each waypoint pos inside scene
            GUIStyle text = new GUIStyle();
            text.fontStyle = FontStyle.Bold;
            text.fontSize = 16;
            text.normal.textColor = Color.black;
            Vector3 textPos = new Vector3(.2f, -.2f);
            Handles.Label(WaypointTarget.EntityPosition + WaypointTarget.Points[i] + textPos, $"{i+1}", text);

            if(EditorGUI.EndChangeCheck()) 
            {
                Undo.RecordObject(target, "Free Move");
                WaypointTarget.Points[i] = newPosition - WaypointTarget.EntityPosition;
            }
        }
    }
}
