using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 1f;
    public GameObject deathParticles;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (IsDead())
        {
            objectDeath();
        }
    }
    public float GetHealth()
    {
        return health;
    }
    public bool IsDead()
    {
        return health <= 0;
    }
    public void objectDeath()
    {
        GameObject effectObject = Instantiate(deathParticles, transform.position, Quaternion.identity);
        ParticleSystem ps = effectObject.GetComponent<ParticleSystem>();
        ps.Play();
        Destroy(ps.gameObject, 3f);
        
        Destroy(gameObject);
    }
}
