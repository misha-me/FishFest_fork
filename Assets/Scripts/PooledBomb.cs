using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledBomb : MonoBehaviour
{
    private ObjectPoolBomb poolBomb;
    private ObjectPoolBomb poolBomb2;
    public AudioClip clip2;
    private float timer = 0f;

    public ObjectPoolBomb PoolBomb { get => poolBomb; set => poolBomb = value; }
    public ObjectPoolBomb PoolBomb2 { get => poolBomb2; set => poolBomb2 = value; }

    public void Release()
    {
        if(this.CompareTag("Second"))
        {
            
            
            poolBomb2.ReturnToSecondPool(this);
            
        }
        else
        {
            
            poolBomb.ReturnToPool(this);
        }
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.tag == "Enemy")
        {
            
            Release();
            timer = 0f;
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(100);
            return;
        }
        else
        {
            if (collision.gameObject.tag == "Enemie")
            {
                
                Release();
                timer = 0f;
                return;
            }

        }
    }


    public void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            timer += Time.deltaTime;
        }


        if (timer >= 7 && gameObject.activeInHierarchy)
        {
            Release();
            timer = 0f;
        }

    }






}
