using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CanSeePlayer", story: "[Can] [Enemy] See [Player]", category: "Action", id: "26e4a63890e76c5a9c7cba2bf28dbe19")]
public partial class CanSeePlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> Can;
    [SerializeReference] public BlackboardVariable<GameObject> Enemy;
    [SerializeReference] public BlackboardVariable<string> Player;
    private EnemyActions enemyActions;
    protected override Status OnStart()
    {
        enemyActions = Enemy.Value.GetComponent<EnemyActions>();
        if(enemyActions.CanSeePlayer())
        {
            Can.Value = true;
        }
        else
        {
            Can.Value = false;
        }
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

