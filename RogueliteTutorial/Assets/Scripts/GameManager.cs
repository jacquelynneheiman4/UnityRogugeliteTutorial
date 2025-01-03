using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player;

    private void Awake()
    {
        PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();

        if (playerCombat != null)
        {
            playerCombat.attackType = new MeleeAttack(playerCombat.AttackRange, new Vector2(0.5f, 0));
        }
    }
}