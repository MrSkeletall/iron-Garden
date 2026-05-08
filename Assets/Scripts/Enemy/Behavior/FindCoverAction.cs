using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindCover", story: "[I] find [cover] position", category: "Action", id: "6fe5232ac877bec267db0c0193c500cc")]
public partial class FindCoverAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> I;
    [SerializeReference] public BlackboardVariable<Vector3> Cover;
    

    protected override Status OnStart()
    {
        // Get the enemy GameObject from the blackboard
        GameObject enemy = I.Value;
        if (enemy == null)
        {
            return Status.Failure;
        }

        // Get the EnemyActions component
        EnemyActions enemyActions = enemy.GetComponent<EnemyActions>();
        if (enemyActions == null)
        {
            return Status.Failure;
        }

        // Find a cover position using the EnemyActions script
        Vector3 coverPos = enemyActions.FindCoverPosition();
        Cover.Value = coverPos;
        

        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}
