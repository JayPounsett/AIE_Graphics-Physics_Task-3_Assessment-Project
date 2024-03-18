using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator Anim;

    // public float jumpVelocity = 10.0f;
    [SerializeField]
    Transform Cam;

    [SerializeField]
    float Speed = 10.0f;

    [SerializeField]
    float JumpHeight = 4.0f;

    [SerializeField]
    Vector3 Velocity = new(0, 0, 0);

    [SerializeField]
    bool IsGrounded = true;

    [SerializeField]
    float MinDistance = 2.0f;

    [SerializeField]
    float MaxDistance = 10.0f;

    [SerializeField]
    float ZoomSpeed = 2.0f;

    [SerializeField]
    float DistanceBack;

    CharacterController Cc;
    Vector2 MoveInput = new();
    bool JumpInput;

    void Start()
    {
        Cc = GetComponent<CharacterController>();
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveInput.x = Input.GetAxis("Horizontal");
        MoveInput.y = Input.GetAxis("Vertical");
        JumpInput = Input.GetButton("Jump");

        Anim.SetFloat("Forward", Mathf.Abs(MoveInput.y));
        Anim.SetFloat("Sense", Mathf.Sign(MoveInput.y));
        Anim.SetFloat("Turn", MoveInput.x);
        Anim.SetBool("Jump", !IsGrounded);

        // TODO: Set up jumping animations for idle, forward & forward+left/forward+right
    }

    void FixedUpdate()
    {
        Vector3 delta = ((MoveInput.x * Vector3.right) + (MoveInput.y * Vector3.forward)) * Speed;

        // find the horizontal unit vector facing forward from the camera
        Vector3 camForward = Cam.forward;
        transform.forward = camForward;
        camForward.y = 0;
        camForward.Normalize();

        // use our camera's right vector, which is always horizontal

        delta = ((MoveInput.x * Cam.right) + (MoveInput.y * camForward)) * Speed;

        if (IsGrounded || MoveInput.x != 0 || MoveInput.y != 0)
        {
            Velocity.x = delta.x;
            Velocity.z = delta.z;
        }

        // check for jumping
        if (JumpInput && IsGrounded)
            Velocity.y = Mathf.Sqrt(JumpHeight * -2.0f * Physics.gravity.y); // Have the player jump up to a consistent height

        // check if we've hit ground from falling. If so, remove our velocity
        if (IsGrounded && Velocity.y < 0)
            Velocity.y = 0.0f;

        // apply gravity after zeroing velocity so we register as grounded still
        Velocity += Physics.gravity * Time.fixedDeltaTime;

        // zoom in/out with mouse wheel
        DistanceBack = Mathf.Clamp(
            DistanceBack - Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed,
            MinDistance,
            MaxDistance
        );

        Cc.Move(Velocity * Time.deltaTime);
        IsGrounded = Cc.isGrounded;
    }
}
