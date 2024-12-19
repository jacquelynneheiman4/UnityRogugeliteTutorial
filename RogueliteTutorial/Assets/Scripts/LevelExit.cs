using UnityEngine;

public class LevelExit : MonoBehaviour
{
    public Transform nextLevelSpawnPoint;
    public GameObject previousLevelParent;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player has reached the level exit. Teleporting player to the next level...");
            TeleportPlayer(other.transform);
            CleanUpPreviousLevel();
        }
    }

    private void TeleportPlayer(Transform player)
    {
        if (nextLevelSpawnPoint != null)
        {
            player.position = nextLevelSpawnPoint.position;
        }
        else
        {
            Debug.LogWarning("nextLevelSpawnPoint is not set. Please set a placeholder value in the inspector for testing.");
        }
    }

    private void CleanUpPreviousLevel()
    {
        if (previousLevelParent != null)
        {
            Destroy(previousLevelParent);
        }
    }
}
