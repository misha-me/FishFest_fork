using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolDef : MonoBehaviour
{
    [SerializeField] private uint initPoolSize;
    [SerializeField] private PooledObject objectToPool;
    // store the pooled objects in a collection
    private Stack<PooledObject> stack;

    public int bulletVelocity;
    public float fireRate = 0.01f;
    private float nextFireTime = 0f;
    private void Start()
    {
        SetupPool();
        //objectToPool.Pool = this;
    }
    // creates the pool (invoke when the lag is not noticeable)
    private void SetupPool()
    {
        stack = new Stack<PooledObject>();
        PooledObject instance = null;
        for (int i = 0; i < initPoolSize; i++)
        {
            instance = Instantiate(objectToPool);
            instance.PoolDef = this;
            instance.gameObject.SetActive(false);
            stack.Push(instance);
        }
    }
    // returns the first active GameObject from the pool
    public PooledObject GetPooledObject()
    {
        // if the pool is not large enough, instantiate a new PooledObjects
        if (stack.Count == 0)
        {
            //PooledObject newInstance = Instantiate(objectToPool);
            //newInstance.Pool = this;
            //return newInstance;
            Debug.Log("No ammo");
            return null;
        }
        // otherwise, just grab the next one from the list
        
        PooledObject nextInstance = stack.Pop();
        //stack.Push(nextInstance);

        nextInstance.gameObject.SetActive(true);
        nextInstance.gameObject.transform.position = transform.position;
        nextInstance.gameObject.transform.rotation = transform.rotation;
        nextInstance.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * bulletVelocity;

        return nextInstance;
    }
    public void ReturnToPool(PooledObject pooledObject)
    {
        stack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetMouseButton(0) && (Time.time >= nextFireTime))
        {
            nextFireTime = Time.time + 1f / fireRate;

            GetPooledObject();
        }


    }
}




