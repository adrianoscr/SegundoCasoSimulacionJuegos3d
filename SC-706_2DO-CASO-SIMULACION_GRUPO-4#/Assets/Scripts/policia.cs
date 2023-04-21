using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class policia : MonoBehaviour
{
    enum PatrolStates { Patroling, Chasing, Attacking }
    [Header("Target")]
    [SerializeField]
    Transform target;
    [SerializeField]
    LayerMask WhatIsPlayer;

    [Header("Patrol")]
    [SerializeField]
    LayerMask WhatIsGround;
    [SerializeField]
    float walkPointRange;
    [SerializeField]
    float sightRange;
    [Header("Attack")]
    [SerializeField]
    float attackRange;
    [SerializeField]
    float attackRate = 0.5F;
    NavMeshAgent agent;
    Vector3 walkPoint;
    bool isWalkPointSet;
    bool isAttacking;
    bool isTargetOnSight;
    bool isTargetInAttackRange;
    PatrolStates currentState;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        isTargetOnSight = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        isTargetInAttackRange = Physics.CheckSphere(transform.position, attackRate, WhatIsPlayer);
        currentState = isTargetOnSight && isTargetInAttackRange ?
            PatrolStates.Attacking : isTargetOnSight && !isTargetInAttackRange ?
            PatrolStates.Chasing : PatrolStates.Patroling;

        switch (currentState)
        {
            case PatrolStates.Attacking:
                HandleAttack();
                break;
            case PatrolStates.Chasing:
                HandleChase();
                break;
            case PatrolStates.Patroling:
                HandlePatrol();
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, walkPointRange);
        Gizmos.color = Color.yellow; Gizmos.DrawWireSphere(transform.position, sightRange);
        Gizmos.color = Color.red; Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void HandleChase()
    {
        agent.SetDestination(target.position);

    }
    private void HandlePatrol()
    {
        if (!isWalkPointSet)
        {
            FindNextWalkPoint();
            if (isWalkPointSet)
            {
                agent.SetDestination(walkPoint);

            }

        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if (distanceToWalkPoint.magnitude < 1)
        {
            isWalkPointSet = false;
        }
    }

    private void FindNextWalkPoint()
    {
        float positionX = Random.Range(-walkPointRange, walkPointRange);
        float positionZ = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + positionX, transform.position.y, transform.position.z + positionZ);

        if (Physics.Raycast(walkPoint, -transform.up, WhatIsGround))
        {
            isWalkPointSet = true;
        }
    }
    private void HandleAttack()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(target); if (!isAttacking)
        {
            isAttacking = true;
            Invoke(nameof(ResetAttack), attackRate);
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
    }
}
