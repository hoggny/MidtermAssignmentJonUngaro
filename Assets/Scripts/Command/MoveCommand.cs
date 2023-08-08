using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveCommand : Command
{
    private NavMeshAgent _agent;
    private Vector3 _destination;

    public MoveCommand(NavMeshAgent agent, Vector3 destination)
    {
        _agent = agent;
        _destination = destination;
    }
    public override bool IsComplete => HasReachedDestination();

    public override void Execute()
    {
        _agent.SetDestination(_destination);
    }

    bool HasReachedDestination()
    {
        if (_agent.remainingDistance > 0.1f) //guard clause
            return false;

        return true;
    }
}
