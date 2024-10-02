using UnityEngine;

public enum GuardState
{
    IDLE,
    PATROL,
    INVESTIGATE,
    PURSUE
}
public class LineOfSight : MonoBehaviour
{
    [SerializeField] Transform target;
    GuardState state = GuardState.IDLE;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 directionToTarget = (target.position - transform.position).normalized;
        Vector3 forwardDirection = transform.forward;

        float dot = Vector3.Dot(forwardDirection, directionToTarget);
        Debug.Log(dot);

        switch (state)
        {
            case GuardState.IDLE:
                UpdateIdle();
                break;
            case GuardState.INVESTIGATE:
                UpdateInvestigate();
                break;
            case GuardState.PURSUE:
                UpdatePursue();
                break;
            case GuardState.PATROL:
                UpdatePatrol();
                break;
        }
    }

    void UpdateIdle()
    {
        Debug.Log("Idle");

    }
    void UpdatePatrol()
    {
        Debug.Log("Patroling");
    }

    void UpdateInvestigate()
    {
        Debug.Log("Investigating");
    }
    void UpdatePursue()
    {
        Debug.Log("Pursuing");
    }
}
