using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform player;
    public bool useMeleeAttack;
    public GameObject projectilePrefab;

    private void Awake()
    {
        PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();

        if (playerCombat != null)
        {
            if (useMeleeAttack)
            {
                playerCombat.attackType = new MeleeAttack(playerCombat.AttackRange, new Vector2(0.5f, 0), player.Find("Sword").GetComponent<Animator>());
            }
            else
            {
                playerCombat.attackType = new RangedAttack(projectilePrefab, new Vector3(0.5f, 0), player.Find("Bow").GetComponent<Animator>());
            }
        }
    }
}