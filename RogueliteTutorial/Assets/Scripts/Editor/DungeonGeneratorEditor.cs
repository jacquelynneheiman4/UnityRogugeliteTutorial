using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DungeonGenerator), true)]
public class DungeonGeneratorEditor : Editor
{
    DungeonGenerator dungeonGenerator;

    private void Awake()
    {
        dungeonGenerator = target as DungeonGenerator;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Generate Dungeon"))
        {
            dungeonGenerator.GenerateDungeon();
        }
    }
}