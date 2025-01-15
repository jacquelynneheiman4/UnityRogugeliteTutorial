using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform player;
    public float detectionRange;
    public List<Transform> waypoints = new List<Transform>();

    private StateMachine stateMachine;
    private List<Vector3> waypointPositions = new List<Vector3>();
    private ICombat combat;

    private void Awake()
    {
        combat = transform.GetComponent<ICombat>();

        foreach (var waypoint in waypoints)
        {
            waypointPositions.Add(waypoint.position);
        }

        stateMachine = new StateMachine();
        stateMachine.ChangeState(new PatrolState(this.transform, waypointPositions));
    }

    private void Update()
    {
        stateMachine.Update();

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange && distanceToPlayer > combat.AttackRange)
        {
            if (!stateMachine.IsCurrentState<ChaseState>())
            {
                stateMachine.ChangeState(new ChaseState(this.transform, player));
            }
        }
        else if (distanceToPlayer <= detectionRange && distanceToPlayer <= combat.AttackRange)
        {
            if (!stateMachine.IsCurrentState<AttackState>())
            {
                stateMachine.ChangeState(new AttackState(this.transform, player));
            }
        }
        else 
        {
            if (!stateMachine.IsCurrentState<PatrolState>())
            {
                stateMachine.ChangeState(new PatrolState(this.transform, waypointPositions));
            }
        }
    }
}