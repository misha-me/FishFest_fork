using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    public LayerMask mask;
    public int rayDistance;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            Debug.DrawRay(transform.position, transform.forward * 10, Color.red);


            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, rayDistance, mask))
            {
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Enemy"))
                    hit.collider.gameObject.SetActive(false);
            }
        }
    }
}
