using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    [SerializeField] private GameObject dest;
    // Start is called before the first frame update
    void Start()
    {
        //agent.ResetPath();
    }

    // Update is called once per frame
    void Update()
    {
        agent.enabled = true;
        agent.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position);
    }
}
