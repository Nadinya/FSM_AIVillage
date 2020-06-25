using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(JobCenter))]
public class JobCenterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("Resource with highest priority first. Resource amount is a Per Villager integer.", MessageType.Info);
        DrawDefaultInspector();
    }

}
