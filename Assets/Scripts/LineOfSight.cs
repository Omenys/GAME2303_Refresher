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
    public bool playerInSight = false;

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
        if (dot > 0.5f)
        {
            playerInSight = true;
        }
        else
        {
            playerInSight = false;
        }
        //Debug.Log(dot);

    }

}


