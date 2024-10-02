using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{

    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    [SerializeField] float speed;
    Rigidbody rb;
    NavMeshPath navPath;
    Queue<Vector3> remainingPoints;
    Vector3 currentTargetPoint;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody>();
        navPath = new NavMeshPath();
        remainingPoints = new Queue<Vector3>();

        if (agent.CalculatePath(target.position, navPath))
        {
            Debug.Log("Found path to target");
            foreach (Vector3 p in navPath.corners)
            {
                remainingPoints.Enqueue(p);
            }
            currentTargetPoint = remainingPoints.Dequeue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //agent.SetDestination(target.position);

        var newForward = (currentTargetPoint - transform.position).normalized;
        newForward.y = 0;
        transform.forward = newForward;

        float distToPoint = Vector3.Distance(transform.position, currentTargetPoint);
        if (distToPoint < 1)
        {
            currentTargetPoint = remainingPoints.Dequeue();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
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
}
