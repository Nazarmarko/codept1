using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyManager : MonoBehaviour
{
    Transform targetTrans;

    NavMeshAgent agentNavMesh;
    #region RangeVars
    [Range(0f, 100f)]
  public float distanceToStopFloat;

    [Range(0f, 10f)]
    public float speedNavMesh;

    [Range(0f, 10f)]
    public float startTimeBTWShoots;
    #endregion
    float timeBTWShoots;

 

    public GameObject enemyBullet;
    void Awake()
    {
        targetTrans = GameObject.FindGameObjectWithTag("FPPlayer").GetComponent<Transform>();
        agentNavMesh = GetComponent<NavMeshAgent>();

        agentNavMesh.stoppingDistance = distanceToStopFloat;
        agentNavMesh.speed = speedNavMesh;
        timeBTWShoots = startTimeBTWShoots;
    }
    void Update()
    {
        float distanceFloat = Vector3.Distance(transform.position, targetTrans.position);

        if (distanceFloat >  distanceToStopFloat)
        {
            agentNavMesh.updatePosition = true;
            agentNavMesh.SetDestination(targetTrans.position);
        }
        else
        {
            agentNavMesh.updatePosition = false;
            if (timeBTWShoots <= 0)
            {
                Instantiate(enemyBullet, transform.position, Quaternion.identity);
                timeBTWShoots = startTimeBTWShoots;
            }
            else
            {
                timeBTWShoots -= Time.deltaTime;
            }
            
        }

        
    }
}
