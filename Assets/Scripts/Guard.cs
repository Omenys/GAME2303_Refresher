using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{

    [SerializeField] NavMeshAgent agent;
    [SerializeField] float speed;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float pauseTime;

    LineOfSight sight;

    GuardState state = GuardState.IDLE;

    int destPoint = 0;
    bool isPaused = false;


    Rigidbody rb;
    NavMeshPath navPath;


    // Start is called before the first frame update
    void Start()
    {
        navPath = new NavMeshPath();

        GoToNextPoint();

    }

    // Update is called once per frame
    void Update()
    {

        if (!agent.pathPending && agent.remainingDistance < 1 && !isPaused)
        {
            StartCoroutine(PauseAtPoint());
        }

        switch (state)
        {
            case GuardState.IDLE:
                Idle();
                break;
            case GuardState.INVESTIGATE:
                Investigate();
                break;
            case GuardState.PURSUE:
                Pursue();
                break;
            case GuardState.PATROL:
                Patrol();
                break;
        }

    }

    private void OnDrawGizmos()
    {
        if (navPath == null)
        {
            return;
        }

        Gizmos.color = Color.red;
        foreach (Vector3 node in navPath.corners)
        {
            Gizmos.DrawWireSphere(node, 0.5f);
        }
    }

    void Idle()
    {
        //Debug.Log("Idle");

    }
    void Patrol()
    {
        Debug.Log("Patroling");
        if (sight.playerInSight)
        {
            state = GuardState.PURSUE;
        }
        else
        {
            state = GuardState.PATROL;
        }
    }

    void Investigate()
    {
        Debug.Log("Investigating");
    }
    void Pursue()
    {
        Debug.Log("Pursuing");


    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0)
        {
            return;
        }

        // go to current destination
        agent.destination = patrolPoints[destPoint].position;

        // choose next patrol point as destination
        destPoint = Random.Range(0, patrolPoints.Length);
    }

    IEnumerator PauseAtPoint()
    {
        isPaused = true;
        agent.isStopped = true;

        yield return new WaitForSeconds(pauseTime);

        agent.isStopped = false;
        GoToNextPoint();
        isPaused = false;
    }


}
