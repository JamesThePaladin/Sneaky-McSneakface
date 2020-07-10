using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor((typeof(Pawn)))]
public class FOVEditorEditor : Editor
{
    void OnSceneGUI()
    {
        Pawn fov = (Pawn)target;
        //show the view radius in the editor
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, fov.gameObject.transform.forward, fov.gameObject.transform.right, 360, fov.viewRadius);
        //draw the view angle in the editor
        Handles.color = Color.white;
        Vector3 viewAngle_A = fov.AngleToTarget(-fov.fieldOfView / 2, false);
        Vector3 viewAngle_B = fov.AngleToTarget(fov.fieldOfView / 2, false);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle_A * fov.viewRadius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle_B * fov.viewRadius);
    }
}
