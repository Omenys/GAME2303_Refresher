using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterLogic : MonoBehaviour

{
    private Vector2 input;
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] Animator animator;

    IA_PlayerActions playerActions;

    // Start is called before the first frame update
    void Start()
    {
        playerActions = new IA_PlayerActions();
        playerActions.Player.Enable();

        rb = GetComponent<Rigidbody>();

        playerActions.Player.Jump.performed += OnJump;
    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        animator.SetFloat("moveSpeed", input.magnitude);
    }

    private void FixedUpdate()
    {
        var newInput = GetCameraBasedInput(input, Camera.main);
        var newVelocity = new Vector3(newInput.x * speed * Time.fixedDeltaTime, rb.velocity.y, newInput.z * speed * Time.fixedDeltaTime);
        rb.velocity = newVelocity;
    }

    Vector3 GetCameraBasedInput(Vector2 input, Camera cam)
    {
        Vector3 camRight = cam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        return (input.x * camRight) + (input.y * camForward);
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Debug.Log("Press Jump");
            rb.AddForce(Vector3.up * 20, ForceMode.Impulse);
        }
    }

}
