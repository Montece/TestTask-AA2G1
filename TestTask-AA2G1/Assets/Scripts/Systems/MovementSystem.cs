using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementSystem : MonoBehaviour
{
    [Header("Movement System")]
    [SerializeField] private float Acceleration = 8f;
    [SerializeField] private float Deceleration = 8f;
    [SerializeField] private float MaximumForwardSpeed = 8f;
    [SerializeField] private float MaximumBackSpeed = 8f;
    [SerializeField] private float StopThresholdSpeed = 8f;
    [SerializeField] private float RotationSpeed = 8f;
    [Space]
    [SerializeField] private Transform GroundCheck;
    [SerializeField] private float GroundDistance = 0.4f;
    [SerializeField] private LayerMask GroundMask;

    public bool BlockMovement = false;

    private const float GRAVITY_g = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private Vector3 move;
    private bool isGrounded;
    private bool isInput;
    private bool isMoveForward;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public void DoMovement(float moving, float rotation)
    {
        if (BlockMovement) return;

        //Проверка гравитации и падения на твердую поверхность для прыжков
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistance, GroundMask);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        float zAxis = moving;
        isInput = zAxis != 0f;
        isMoveForward = zAxis >= 0f;

        if (isInput)
        {
            velocity.z += zAxis * Acceleration * Time.deltaTime;
            if (velocity.z > MaximumForwardSpeed) velocity.z = MaximumForwardSpeed;
            if (velocity.z < -MaximumBackSpeed) velocity.z = -MaximumBackSpeed;
        }
        else
        {
            if (velocity.z < 0f) velocity.z += Deceleration * Time.deltaTime;
            if (velocity.z > 0f) velocity.z -= Deceleration * Time.deltaTime;
            if (Mathf.Abs(velocity.z) < StopThresholdSpeed) velocity.z = 0f;
        }

        velocity.y += GRAVITY_g * Time.deltaTime;

        move = (transform.forward * velocity.z + transform.up * velocity.y) * Time.deltaTime;
        if (controller.enabled) controller.Move(move);

        float xAxis = rotation;
        transform.Rotate(transform.up, (isMoveForward ? 1 : -1) * xAxis * RotationSpeed * Time.deltaTime);
    }
}
