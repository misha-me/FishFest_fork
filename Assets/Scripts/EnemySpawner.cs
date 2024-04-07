using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private uint initPoolSize;
    [SerializeField] private PooledEnemy objectToPool;
    [SerializeField] private List<Transform> spawners;
    [SerializeField] private float lagBetweenSpawn;
    [SerializeField] private int neededCountOfEnemy;
    private bool lastMsg = false;
    private float timer;
    private int countOfEnemy;
    // store the pooled objects in a collection
    private Stack<PooledEnemy> stack;
    private void Start()
    {
        SetupPool();
        timer = 0f;
        countOfEnemy= 0;
        
    }
    // creates the pool (invoke when the lag is not noticeable)
    private void SetupPool()
    {
        stack = new Stack<PooledEnemy>();
        PooledEnemy instance = null;
        for (int i = 0; i < initPoolSize; i++)
        {
            instance = Instantiate(objectToPool);
            instance.EnemyPool = this;
            instance.gameObject.SetActive(false);
            stack.Push(instance);
        }
    }

    // returns the first active GameObject from the pool
    public PooledEnemy GetPooledObject()
    {
        
       
        if (stack.Count == 0)
        {
            //PooledEnemy newInstance = Instantiate(objectToPool);
            //newInstance.EnemyPool = this;
            //Debug.Log("No enemy in pool");
            return null;
        }
        
        Transform randomTransform;
        PooledEnemy nextInstance = stack.Pop();
        randomTransform = spawners[Random.Range(0,10)];
        nextInstance.gameObject.SetActive(true);
        nextInstance.gameObject.transform.position = randomTransform.position;
        nextInstance.gameObject.transform.rotation = transform.rotation;
        return nextInstance;
    }
    public void ReturnToPool(PooledEnemy pooledObject)
    {
        stack.Push(pooledObject);
        pooledObject.gameObject.SetActive(false);
    }

    private void Update()
    {
        if(countOfEnemy < neededCountOfEnemy)
        {
            timer += Time.deltaTime;
            if (timer >= lagBetweenSpawn)
            {
                GetPooledObject();
                timer = 0f;
                countOfEnemy++;
            }
        }
        else
        {
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && !lastMsg)
            {
                lastMsg= !lastMsg;
                Debug.Log("You have got them all");
            }
            
        }
        
    }
}
