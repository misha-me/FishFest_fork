using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public LayerMask mask;
    public int rayDistance;
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward * 10, Color.red);


            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance, mask))
            {
                //Debug.Log(hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(1);
                    //Debug.Log(hit.collider.gameObject.GetComponent<EnemyHealth>().Health); 
                }
                    
            }
        }
    }
}
