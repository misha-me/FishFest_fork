using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitcher : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "DefaultGun")
                transform.GetChild(i).gameObject.SetActive(true);
            else
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    //public List<GameObject> childObjects; // List of child GameObjects
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Alpha4))
            EnableChildren();
    }

    public void EnableChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (transform.GetChild(i).name == "DefaultGun")
                    transform.GetChild(i).gameObject.SetActive(true);
                else
                    transform.GetChild(i).gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (transform.GetChild(i).name == "MiniGun")
                    transform.GetChild(i).gameObject.SetActive(true);
                else
                    transform.GetChild(i).gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (transform.GetChild(i).name == "LaserGun")
                {
                    transform.GetChild(i).gameObject.SetActive(true);
                    transform.GetChild(i).transform.GetChild(0).gameObject.SetActive(false);
                }  
                else
                    transform.GetChild(i).gameObject.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                if (transform.GetChild(i).name == "BombGun")
                    transform.GetChild(i).gameObject.SetActive(true);
                else
                    transform.GetChild(i).gameObject.SetActive(false);
            }

        }
    }
}
