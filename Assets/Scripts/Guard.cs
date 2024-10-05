using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{

    [SerializeField] NavMeshAgent agent;
    //[SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] Transform[] patrolPoints;
    [SerializeField] float pauseTime;
    int destPoint = 0;
    bool isPaused = false;


    Rigidbody rb;
    NavMeshPath navPath;


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
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
