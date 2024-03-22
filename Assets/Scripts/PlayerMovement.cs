using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  [SerializeField] private CharacterController controller;
  [SerializeField] private float speed = 12f;
  [SerializeField] private float gravity = -9.81f;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private float groundDistance = 2.4f;
  [SerializeField] private float jumpHeight = 3f;
  [SerializeField] private LayerMask groundMask;
  [SerializeField] private AudioSource footSteps;

  // Camera Movement
  [SerializeField] private MouseMovment cameraMovement;

  private Vector3 velocity = Vector3.zero;
  private bool isGrounded = true;
  private Vector3 movement = Vector3.zero;

  void GetInputs(){
    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
    float x = Input.GetAxis("Horizontal");
    float z = Input.GetAxis("Vertical");
    movement = (transform.right * x + transform.forward * z).normalized;
  }

  // Update is called once per frame
  void Update() {

    GetInputs();

    // Movement
    controller.Move(movement * speed * Time.deltaTime);

    // Jumping
    // if (isGrounded && velocity.y < 0) velocity.y = -2f;
    if (isGrounded && Input.GetButtonDown("Jump")) {
      velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    // Gravity
    velocity.y += gravity * Time.deltaTime;
    controller.Move(velocity * Time.deltaTime);

    ManageSounds();
  }

  void ManageSounds(){
    // Movement Sounds
    footSteps.volume = movement.magnitude > 0.1 ? 0.5f : 0f;
  }

  // Triggers Should not exist on player. Use scripts on these objects
  // void OnTriggerEnter(Collider other) {
  //   if (other.transform.CompareTag("Portal")) {
  //     AudioManager.instance.Play("whoosh");
  //   }
  //   if (other.transform.CompareTag("Finish")) {
  //     GameManager.instance.GameDone(true);
  //   }
  // }
}