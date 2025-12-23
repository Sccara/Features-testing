using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public enum State { Patrol, Chase, ReturnToPatrol }

    public Transform[] patrolPoints;
    public bool loopPatrol = true;
    public float waitAtPoint = 1.0f;

    public string playerTag = "Player";
    public float chaseSpeed = 6f;
    public float patrolSpeed = 3.5f;
    public float detectionRadius = 10f;
    [Range(0, 180)] public float fovAngle = 60f;
    public float loseSightTime = 2f;


    public float stoppingDistanceToPlayer = 1.5f;
    public LayerMask obstructionMask;
    public bool drawGizmos = true;

    NavMeshAgent agent;
    Transform player;
    [SerializeField] private State currentState = State.Patrol;
    int currentPatrolIndex = 0;
    bool waiting = false;
    float lastTimePlayerSeen = -Mathf.Infinity;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag(playerTag);
        if (p != null)
        {
            player = p.transform;
        }

        agent.speed = patrolSpeed;

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            agent.destination = patrolPoints[0].position;
        }


        StartCoroutine(DetectionLoop());
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                PatrolUpdate();
                break;
            case State.Chase:
                ChaseUpdate();
                break;
            case State.ReturnToPatrol:
                ReturnToPatrolUpdate();
                break;
        }

        if (player && CanSeePlayer())
        {
            lastTimePlayerSeen = Time.time;
            if (currentState != State.Chase)
                StartChase();
        }
        else
        {

            if (currentState == State.Chase && Time.time - lastTimePlayerSeen > loseSightTime)
            {
                StopChaseAndReturn();
            }
        }
    }

    IEnumerator DetectionLoop()
    {
        var wait = new WaitForSeconds(0.15f);
        while (true)
        {
            yield return wait;
        }
    }

    bool CanSeePlayer()
    {
        if (!player) return false;

        Vector3 dir = (player.position - transform.position);
        float dist = dir.magnitude;
        if (dist > detectionRadius)
            return false;

        float angle = Vector3.Angle(transform.forward, dir.normalized);
        if (angle > fovAngle * 0.5f)
            return false;

        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, dir.normalized, out RaycastHit hit, detectionRadius, ~0, QueryTriggerInteraction.Ignore))
        {
            if ((obstructionMask.value & (1 << hit.collider.gameObject.layer)) != 0)
            {
                return false;
            }

            if (hit.transform == player || hit.collider.CompareTag(playerTag))
                return true;
            else
                return false;
        }
        return false;
    }

    #region Patrol
    void PatrolUpdate()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
            return;

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            if (!waiting)
                StartCoroutine(WaitAndMoveToNext());
        }
    }

    IEnumerator WaitAndMoveToNext()
    {
        waiting = true;
        yield return new WaitForSeconds(waitAtPoint);

        currentPatrolIndex++;
        if (currentPatrolIndex >= patrolPoints.Length)
        {
            if (loopPatrol)
            {
                currentPatrolIndex = 0;
            }
            else
            {
                waiting = false;
                yield break;
            }
        }

        agent.destination = patrolPoints[currentPatrolIndex].position;
        waiting = false;
    }
    #endregion

    #region Chase
    void StartChase()
    {
        currentState = State.Chase;
        agent.speed = chaseSpeed;
        agent.stoppingDistance = stoppingDistanceToPlayer;
    }

    void ChaseUpdate()
    {
        if (player == null)
        {
            return;
        }
        agent.isStopped = false;
        agent.SetDestination(player.position);

        if (agent.remainingDistance <= agent.stoppingDistance + 0.1f)
        {
            agent.isStopped = true;
        }
    }

    void StopChaseAndReturn()
    {
        currentState = State.ReturnToPatrol;
        agent.speed = patrolSpeed;
        agent.stoppingDistance = 0f;

        if (patrolPoints != null && patrolPoints.Length > 0)
        {
            int nearest = FindNearestPatrolIndex();
            currentPatrolIndex = nearest;
            agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }
    #endregion

    #region Return
    void ReturnToPatrolUpdate()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentState = State.Patrol;

            currentPatrolIndex = (currentPatrolIndex + 1) % (patrolPoints.Length == 0 ? 1 : patrolPoints.Length);
            if (patrolPoints.Length > 0)
                agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        }
    }

    int FindNearestPatrolIndex()
    {
        if (patrolPoints == null || patrolPoints.Length == 0) return 0;
        int idx = 0;
        float best = float.MaxValue;
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            float d = Vector3.SqrMagnitude(transform.position - patrolPoints[i].position);
            if (d < best)
            { 
                best = d;
                idx = i; 
            }
        }
        return idx;
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        if (!drawGizmos) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Vector3 left = Quaternion.Euler(0, -fovAngle * 0.5f, 0) * transform.forward;
        Vector3 right = Quaternion.Euler(0, fovAngle * 0.5f, 0) * transform.forward;

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position + Vector3.up * 0.5f, left * detectionRadius);
        Gizmos.DrawRay(transform.position + Vector3.up * 0.5f, right * detectionRadius);

#if UNITY_EDITOR
        if (agent != null && agent.hasPath)
        {
            Gizmos.color = Color.cyan;
            var path = agent.path;
            var corners = path.corners;
            for (int i = 0; i < corners.Length - 1; i++)
                Gizmos.DrawLine(corners[i], corners[i + 1]);
        }
#endif
    }

}
