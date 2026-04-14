using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FindCover", story: "This Checks for Cover", category: "Action", id: "6fe5232ac877bec267db0c0193c500cc")]
public partial class FindCoverAction : Action
{
    [SerializeReference] public BlackboardVariable<Vector3> CoverPosition;

    protected override Status OnStart()
    {
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
       
    }

    protected override void OnEnd()
    {
    }
}
