using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public LayerMask mask;
    public int rayDistance;
    public AudioClip clip;
    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
            if (GetComponent<AudioSource>().isPlaying == true)
            {

            }
            else
            {
                GetComponent<AudioSource>().PlayOneShot(clip);
            }
            


            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance, mask))
            {
                //Debug.Log(hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag == "Enemy")
                {
                    hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(5);
                    //Debug.Log(hit.collider.gameObject.GetComponent<EnemyHealth>().Health); 
                }
                    
            }
        }
        if (!Input.GetMouseButton(0))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            GetComponent<AudioSource>().Stop();
        }
            

    }
}
