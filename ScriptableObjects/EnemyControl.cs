using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyControl : MonoBehaviour
{
    private NavMeshAgent navAgent;
    private float wanderDistance = 3;

    public EnemyData data;

    private void Start()
    {
        if (navAgent == null)
            navAgent = this.GetComponent<NavMeshAgent>();

        if (data != null)
            LoadEnemy(data);
    }

    private void LoadEnemy(EnemyData _data)
    {
        //remove children objects i.e. visuals
        foreach (Transform child in this.transform)
        {
            if (Application.isEditor)
                DestroyImmediate(child.gameObject);
            else
                Destroy(child.gameObject);
        }

        //load current enemy visuals
        GameObject visuals = Instantiate(data.enemyModel);
        visuals.transform.SetParent(this.transform);
        visuals.transform.localPosition = Vector3.zero;
        visuals.transform.rotation = Quaternion.identity;

        //use stats data to set up enemy
        if (navAgent == null)
            navAgent = this.GetComponent<NavMeshAgent>();

        this.navAgent.speed = data.speed;
    }

    private void Update()
    {
        if (data == null)
            return;

        if (navAgent.remainingDistance < 1f)
            GetNewDestination();
    }

    private void GetNewDestination()
    {
        Vector3 nextDestination = this.transform.position;
        nextDestination += wanderDistance * new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(nextDestination, out hit, 3f, NavMesh.AllAreas))
            navAgent.SetDestination(hit.position);
    }

}
