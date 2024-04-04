using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    private ObjectPoolDef poolDef;
    
    
    public ObjectPoolDef PoolDef { get => poolDef; set => poolDef = value; }
    public void Release()
    {
        poolDef.ReturnToPool(this);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemie")
            Release();
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.SetActive(false);
    } //Время ще треба і ок або координати

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.SetActive(false);
        }

    }


    /* IEnumerator Start()
     {
         Debug.Log("yyy");
         if (gameObject.activeInHierarchy)
         {
             yield return new WaitForSeconds(5);
             Debug.Log(2222);
             Release();
         }
     }*/




}


