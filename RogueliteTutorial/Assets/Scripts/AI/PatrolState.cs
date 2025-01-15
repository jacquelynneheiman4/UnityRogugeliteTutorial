using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    private IMovement movement;
    private List<Vector3> waypoints;
    private int currentWaypointIndex;
    private float closeEnough = 0.1f;
    
    public PatrolState(Transform owner, List<Vector3> waypoints) : base(owner)
    {
        movement = owner.GetComponent<IMovement>();
        this.waypoints = new List<Vector3>(waypoints);
        currentWaypointIndex = 0;
    }

    public override void Enter()
    {
        
    }

    public override void Execute()
    {
        movement.Move(waypoints[currentWaypointIndex]);

        float distanceToWaypoint = Vector3.Distance(owner.position, waypoints[currentWaypointIndex]);

        if (distanceToWaypoint <= closeEnough)
        {
            currentWaypointIndex++;

            if (currentWaypointIndex >= waypoints.Count)
            {
                currentWaypointIndex = 0;
            }
        }
    }

    public override void Exit()
    {
        
    }
}