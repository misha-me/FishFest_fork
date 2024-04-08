using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    private ObjectPoolDef poolDef;
    private float timer = 0f;
    
    
    public ObjectPoolDef PoolDef { get => poolDef; set => poolDef = value; }
    
    public void Release()
    {
        
        poolDef.ReturnToPool(this);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Release();
            timer = 0f;
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage();
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
            
    } //Время ще треба і ок або координати

   

    public void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            timer += Time.deltaTime;
        }
            

        if (timer >= 5 && gameObject.activeInHierarchy)
        {
            Release();
            timer = 0f;
        }
            
    }
    







}


