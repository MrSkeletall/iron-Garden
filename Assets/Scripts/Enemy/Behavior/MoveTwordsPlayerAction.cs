using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "MoveTwordsPlayer", story: "Move [Self] to [near] [Player]", category: "Action", id: "416291b85ba296b85ecf3d3b8fb396c1")]
public partial class MoveTwordsPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Vector3> Near;
    [SerializeReference] public BlackboardVariable<string> Player;
    private EnemyActions enemyActions;
    
    protected override Status OnStart()
    {
        enemyActions = Self.Value.GetComponent<EnemyActions>();
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

