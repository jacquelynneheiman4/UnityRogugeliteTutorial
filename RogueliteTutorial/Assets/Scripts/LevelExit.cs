using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public DungeonManager dungeonManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = dungeonManager.NextLevel();
        }
    }
}
