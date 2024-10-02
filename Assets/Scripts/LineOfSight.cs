using UnityEngine;
public class LineOfSight : MonoBehaviour
{
    [SerializeField] Transform target;

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
    }
}
