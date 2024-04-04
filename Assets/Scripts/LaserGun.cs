using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    private bool isinprogress = false;
    public bool progressGetGet { get => isinprogress; set => isinprogress = value; }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(!isinprogress)
                StartCoroutine(AppearDisappear());
        }
    }
    IEnumerator AppearDisappear()
    {
        isinprogress = true;
        transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(0.4f);
        isinprogress = false;
    }
}
