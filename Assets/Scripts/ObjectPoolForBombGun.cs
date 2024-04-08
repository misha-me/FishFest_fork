using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPoolBomb : MonoBehaviour
{
    [SerializeField] private uint initPoolSize;
    //[SerializeField] private uint initSecondPoolSize;
    [SerializeField] private PooledBomb objectToPool;
    // store the pooled objects in a collection
    private Stack<PooledBomb> stack;
    private Stack<PooledBomb> stack2;

    public AudioClip clip1;
    public AudioClip clip2;
    public int numBullets = 5;
    public float fireRate = 0.01f;
    private float nextFireTime = 0f;
    private void Start()
    {
        SetupPool();
        SetupSecondaryPool();
        //objectToPool.Pool = this;
    }
    // creates the pool (invoke when the lag is not noticeable)
    private void SetupPool()
    {
        stack = new Stack<PooledBomb>();
        PooledBomb instance = null;
        for (int i = 0; i < initPoolSize; i++)
        {
            instance = Instantiate(objectToPool);
            instance.PoolBomb = this;
            instance.gameObject.SetActive(false);
            stack.Push(instance);
        }
        
    }
    private void SetupSecondaryPool()
    {
        stack2 = new Stack<PooledBomb>();
        PooledBomb instance = null;
        for (int i = 0; i < initPoolSize*5; i++)
        {
            instance = Instantiate(objectToPool);
            instance.PoolBomb2 = this;
            instance.gameObject.tag = "Second";
            instance.gameObject.SetActive(false);
            stack2.Push(instance);
        }
        
    }
    // returns the first active GameObject from the pool
    public PooledBomb GetPooledObject()
    {
        if (stack.Count == 0)
        {
            Debug.Log("No ammo");
            return null;
        }
        PooledBomb nextInstance = stack.Pop();
        nextInstance.gameObject.SetActive(true);
        nextInstance.gameObject.transform.position = transform.position;
        nextInstance.gameObject.transform.rotation = transform.rotation;
        nextInstance.gameObject.GetComponent<Rigidbody>().velocity = transform.forward * 20;
        GetComponent<AudioSource>().PlayOneShot(clip1);
        return nextInstance;
    }
    public PooledBomb[] GetSecondaryObject(Transform x)
    {
        if (stack2.Count < numBullets)
        {
            Debug.Log("No ammo");
            return null;
        }
        PooledBomb[] bullets = new PooledBomb[numBullets];
        for (int i = 0; i < numBullets; i++)
        {
            PooledBomb nextInstanse = stack2.Pop();
            nextInstanse.gameObject.SetActive(true);
            StartCoroutine(DisableColliderForTime(nextInstanse.gameObject.GetComponent<Collider>(), 0.1f));

            Vector3 randomDirection = Random.insideUnitSphere; 
            randomDirection = x.TransformDirection(randomDirection); 
            randomDirection = Vector3.RotateTowards(randomDirection, transform.forward+transform.up, 45 * Mathf.Deg2Rad, 0f); 

            nextInstanse.gameObject.transform.position = x.position;
            nextInstanse.gameObject.transform.rotation = Quaternion.LookRotation(randomDirection);
            nextInstanse.gameObject.GetComponent<Rigidbody>().velocity = randomDirection * 40;

            bullets[i] = nextInstanse;
        }

        return bullets;
    }
    public void ReturnToPool(PooledBomb pooledObject)
    {
        Transform x = pooledObject.gameObject.transform;
        stack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
        GetComponent<AudioSource>().PlayOneShot(clip2);
        GetSecondaryObject(x);

    }
    public void ReturnToSecondPool(PooledBomb pooledObject)
    {
        stack2.Push(pooledObject);
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

    IEnumerator DisableColliderForTime(Collider collider, float disableTime)
    {
        collider.enabled = false;
        yield return new WaitForSeconds(disableTime); 
        collider.enabled = true;
    }
}
