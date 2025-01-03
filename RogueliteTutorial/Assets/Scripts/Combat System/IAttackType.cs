using UnityEngine;

public interface IAttackType
{
    void ExecuteAttack(Transform attacker, int damage, LayerMask targetLayer, bool isFacingLeft);
}