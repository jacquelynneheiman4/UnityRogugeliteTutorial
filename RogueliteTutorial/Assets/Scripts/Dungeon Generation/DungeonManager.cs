using System.Collections.Generic;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public DungeonGenerator dungeonGenerator;
    public LevelExit levelExitPrefab;
    public Transform player;

    private Dungeon currentDungeon;
    private Vector3 spawnOffset = new Vector3(0.5f, 0.5f);
    private Transform levelParent;

    private void Start()
    {
        player.transform.position = NextLevel();
    }

    public Vector3 NextLevel()
    {
        if (levelParent != null)
        {
            Destroy(levelParent.gameObject);
        }

        levelParent = new GameObject("Level Parent").transform;

        currentDungeon = dungeonGenerator.CreateDungeon();

        LevelExit exit = Instantiate(
            levelExitPrefab, 
            currentDungeon.GetExitLocation() + spawnOffset, 
            Quaternion.identity
        );

        exit.dungeonManager = this;
        exit.transform.parent = levelParent;

        return currentDungeon.GetStartLocation() + spawnOffset;
    }
}
