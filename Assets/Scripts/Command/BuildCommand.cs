using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildCommand : Command
{
    private NavMeshAgent _agent;
    private Builder _builder;

    public BuildCommand(NavMeshAgent agent, Builder builder)
    {
        this._agent = agent;
        this._builder = builder;
    }
    public override bool IsComplete => IsBuildComplete();

    bool IsBuildComplete()
    {
        if (_agent.remainingDistance > 0.6f) return false;


        if (_builder != null)
        {
            _builder.Build();
        }

        return true;
    }

    public override void Execute()
    {
        _agent.SetDestination(_builder.transform.position);
    }
}
