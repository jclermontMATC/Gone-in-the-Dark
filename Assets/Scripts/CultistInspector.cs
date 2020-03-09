using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
//////////////////////////////////
/// Script by Brian Dornbusch ///
////////// 2/27/2020 ////////////
////////////////////////////////
[CustomEditor (typeof (CultistController))]

public class CultistInspector : Editor {
    GameObject Waypoint;
    public override void OnInspectorGUI () {
        DrawDefaultInspector ();

        CultistController CC = (CultistController) target;
        EditorUtility.SetDirty(CC);
        EditorGUILayout.Separator();
        EditorGUILayout.LabelField("Waypoints");
        foreach (Transform waypoint in CC.waypoints) {

           Waypoint = (GameObject) EditorGUILayout.ObjectField (waypoint.gameObject, typeof (GameObject), false);
          
            if (CC.waypoints.Count > 1) {
                if (GUILayout.Button ("Delete waypoint",GUILayout.MaxWidth(100f))) {
                    CC.waypoints.Remove (waypoint);
                    DestroyImmediate(waypoint.gameObject);
                }
            }
        }
        EditorGUILayout.Separator();
        if (GUILayout.Button ("Add waypoint")) {
            CC.Addwaypoint ();
        }
    }
}