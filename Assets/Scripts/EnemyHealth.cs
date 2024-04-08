using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int health;
    public int healthMax;
    public AudioClip[] deaths;
    private bool isDead;

    public int Health { get => health; set => health = value; }

    private void Start()
    {
        health = healthMax; 
    }
    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
              
            if (GetComponent<AudioSource>().isPlaying == false && !isDead)
            {
                isDead = true;
                GetComponent<AudioSource>().PlayOneShot(deaths[Random.Range(0, deaths.Length)]);
                
                
            }
            StartCoroutine(zatrymka());
            
        }
            
            
    }
    public void EnemyDie()
    {
        
        gameObject.SetActive(false);
        isDead= false;
    }
    public void TakeDamage()
    {
        health -= 50;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    IEnumerator zatrymka()
    {
        
        yield return new WaitForSeconds(0.3f);
        EnemyDie();
    }
}
