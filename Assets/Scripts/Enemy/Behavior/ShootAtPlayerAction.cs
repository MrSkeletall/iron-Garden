using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ShootAtPlayer", story: "[I] [fire] projectile at [Player]", category: "Action", id: "51130587f9dc0f276e7fa8f55224dd84")]
public partial class ShootAtPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> I;
    [SerializeReference] public BlackboardVariable<EnemyShooting> Fire;
    [SerializeReference] public BlackboardVariable<GameObject> Player;
    protected override Status OnStart()
    {
        // Find and set the Player GameObject
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Player.SetObjectValueWithoutNotify(player);
        
        // Get the EnemyShooting component from the enemy GameObject (I)
        GameObject enemy = I.Value;
        if (enemy != null && player != null)
        {
            EnemyShooting enemyWeapon = enemy.GetComponent<EnemyShooting>();
            if (enemyWeapon != null)
            {
                // Extract the CharacterController from the Player and set it on the EnemyShooting script
                CharacterController playerController = player.GetComponent<CharacterController>();
                if (playerController != null)
                {
                    enemyWeapon.playerController = playerController;
                }
            }
        }
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        // Get the EnemyShooting component from the enemy GameObject and call the shooting method
        GameObject enemy = I.Value;
        if (enemy != null)
        {
            EnemyShooting enemyWeapon = enemy.GetComponent<EnemyShooting>();
            if (enemyWeapon != null)
            {
                enemyWeapon.ShootAtPlayer();
            }
        }
        
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

