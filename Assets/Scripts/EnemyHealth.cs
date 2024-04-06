using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int health;
    public int healthMax;

    private void Start()
    {
        health = healthMax; 
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            EnemyDie();
            
    }
    public void EnemyDie()
    {
        gameObject.SetActive(false);
    }
    public void TakeDamage()
    {
        health -= 50;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }
}
