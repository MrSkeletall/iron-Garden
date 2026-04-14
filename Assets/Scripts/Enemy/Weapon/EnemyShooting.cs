using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
  [Header("References")]
      public Transform projectileSpawnPoint;
      public GameObject projectilePrefab;
      public CharacterController playerController;
  
      [Header("Projectile Settings")]
      public float projectileSpeed = 20f;
  
      public void ShootAtPlayer()
      {
          if (playerController == null || projectilePrefab == null || projectileSpawnPoint == null)
              return;
  
          Vector3 playerPos = playerController.transform.position;
          Vector3 playerVel = playerController.velocity;
  
          // Predict where the player will be
          Vector3 predictedPos = PredictFuturePosition(
              projectileSpawnPoint.position,
              projectileSpeed,
              playerPos,
              playerVel
          );
  
          // Direction to predicted point
          Vector3 shootDir = (predictedPos - projectileSpawnPoint.position).normalized;
  
          // Spawn projectile
          GameObject proj = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.LookRotation(shootDir));
  
          // Launch projectile
          Rigidbody projRb = proj.GetComponent<Rigidbody>();
          projRb.linearVelocity = shootDir * projectileSpeed;
      }
  
      /// <summary>
      /// Calculates the predicted future position of a moving target.
      /// </summary>
      private Vector3 PredictFuturePosition(Vector3 shooterPos, float shotSpeed, Vector3 targetPos, Vector3 targetVel)
      {
          Vector3 displacement = targetPos - shooterPos;
          float targetSpeedSq = targetVel.sqrMagnitude;
          float shotSpeedSq = shotSpeed * shotSpeed;
  
          // Quadratic formula components
          float a = targetSpeedSq - shotSpeedSq;
          float b = 2f * Vector3.Dot(displacement, targetVel);
          float c = displacement.sqrMagnitude;
  
          // Solve quadratic discriminant
          float discriminant = b * b - 4f * a * c;
  
          // If no real solution, aim directly at player
          if (discriminant < 0f)
              return targetPos;
  
          float sqrtDisc = Mathf.Sqrt(discriminant);
  
          // Two possible times
          float t1 = (-b + sqrtDisc) / (2f * a);
          float t2 = (-b - sqrtDisc) / (2f * a);
  
          // Choose the smallest positive time
          float t = Mathf.Min(t1, t2);
          if (t < 0f)
              t = Mathf.Max(t1, t2);
  
          // If still negative, aim directly
          if (t < 0f)
              return targetPos;
  
          return targetPos + targetVel * t;
      }
}
