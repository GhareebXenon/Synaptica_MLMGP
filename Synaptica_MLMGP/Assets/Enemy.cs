using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int startingHealth = 100;
  

    private int currentHealth;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        currentHealth = startingHealth;
    }

    public void GetShot(int damage,AgentController shooter)
    {
        ApplyDamage(damage,shooter);
    }

    private void ApplyDamage(int damage, AgentController shooter)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die(shooter);
        }
    }

    private void Die(AgentController shooter)
    {
        Debug.Log("I Died!");
        
        Respawn();
    }

    private void Respawn()
    {
        currentHealth = startingHealth;
        transform.position = startPosition;
    }

   
}
