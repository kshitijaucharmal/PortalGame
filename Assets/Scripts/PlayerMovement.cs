using UnityEngine;

public class PlayerMovement : MonoBehaviour {

  [Header("Movement Controls")]
  [SerializeField] private Rigidbody rb;
  [SerializeField] private float speed = 12f;

  [Header("Jumping Controls")]
  [SerializeField] private float gravity = 9.81f;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private float groundDistance = 2.4f;
  [SerializeField] private float jumpForce = 800;
  [Range(0f, 1f)] [SerializeField] float stopFactor = 0.5f;
  [SerializeField] private LayerMask groundMask;
  [SerializeField] private AudioSource footSteps;

  // Private variables
  private Vector3 velocity;
  private bool isGrounded = true;
  private bool canJump = false;
  private Vector3 movement;

  void GetInputs(){
    float x = Input.GetAxisRaw("Horizontal");
    float z = Input.GetAxisRaw("Vertical");
    movement = new Vector3(x, 0, z).normalized;

    isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
  }

  // Update is called once per frame
  void Update() {
    GetInputs();
    ManageSounds();

    // Jumping
    // if (isGrounded && velocity.y < 0) velocity.y = -2f;
    if (isGrounded && Input.GetButtonDown("Jump")) {
      canJump = true;
    }
    if(Input.GetButtonUp("Jump")){
      rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y * stopFactor, rb.velocity.z);
    }
  }

  void FixedUpdate(){
    var velocity = rb.velocity;
    velocity = transform.TransformDirection(movement) * (speed * Time.deltaTime);
    velocity.y = rb.velocity.y;
    velocity.y -= gravity * Time.deltaTime;
    rb.velocity = velocity;

    // Jumping
    if (canJump){
      rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
      canJump = false;
    }
  }

  void ManageSounds(){
    // Movement Sounds
    footSteps.volume = movement.magnitude > 0.1 ? 0.5f : 0f;
  }

  #region Obsolete
  // Triggers Should not exist on player. Use scripts on these objects
  void OnTriggerEnter(Collider other) {
    if (other.transform.CompareTag("Portal")) {
      AudioManager.instance.Play("whoosh");
    }
    if (other.transform.CompareTag("Finish")) {
      GameManager.instance.GameDone(true);
    }
  }
  #endregion
}