using UnityEngine;
//using UnityEngine.InputSystem;
public class CharacterLogic : MonoBehaviour

{
    private Vector2 input;
    Rigidbody rb;
    [SerializeField] float speed;
    [SerializeField] float jumpStrength;
    [SerializeField] Animator animator;

    IA_PlayerActions playerActions;

    // Start is called before the first frame update
    void Start()
    {
        // Input Actions
        playerActions = new IA_PlayerActions();
        playerActions.Player.Enable();

        rb = GetComponent<Rigidbody>();

        // playerActions.Player.Jump.performed += OnJump;
    }

    // Update is called once per frame
    void Update()
    {
        // New Imput system movement
        input = playerActions.Player.Move.ReadValue<Vector2>();

        // Old input system
        //input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        //Walk animation
        animator.SetFloat("moveSpeed", input.magnitude);


        // Face character in direction of travel
        Vector3 direction = GetCameraBasedInput(input, Camera.main);
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
        }

    }

    private void FixedUpdate()
    {
        // Camera based movement
        var newInput = GetCameraBasedInput(input, Camera.main);
        var newVelocity = new Vector3(newInput.x * speed * Time.fixedDeltaTime, rb.velocity.y, newInput.z * speed * Time.fixedDeltaTime);
        rb.velocity = newVelocity;

    }

    // Camera based movement logic
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

    // Jump
    /*public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

        }

    }*/


}
