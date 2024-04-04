using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledBomb : MonoBehaviour
{
    private ObjectPoolBomb poolBomb;
    private ObjectPoolBomb poolBomb2;


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
        if (collision.gameObject.tag == "Enemie")
        {
            Release();
            
        }
            
        if (collision.gameObject.tag == "Enemy")
            collision.gameObject.SetActive(false);
    } //Время ще треба і ок або координати




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
