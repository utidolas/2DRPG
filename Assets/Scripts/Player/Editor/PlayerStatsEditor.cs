using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayerStats))]
public class PlayerStatsEditor : Editor
{
    private PlayerStats StatsTarget => target as PlayerStats; // reference the script target PlayerStats
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("Reset Player"))
        {
            StatsTarget.ResetPlayer();
        }
    }
}
