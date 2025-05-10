using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool isSprinting = false;
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public ParticleSystem hitParticle;

    public AudioClip jumpSfx;
    public AudioClip crashSfx;

    private int hp = 3;
    private Rigidbody rb;
    private InputAction jumpAction;
    private int jumpCount = 0;
    private int maxJumps = 1;
    private Animator playerAnim;
    private AudioSource playerAudio;

    public bool gameOver = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    void Start()
    {
        Physics.gravity *= gravityModifier;
        jumpAction = InputSystem.actions.FindAction("Jump");
        gameOver = false;
    }

    void Update()
    {
        if (jumpAction.triggered && jumpCount < maxJumps && !gameOver)
        {
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            jumpCount++;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSfx);
        }

        isSprinting = Keyboard.current.leftShiftKey.isPressed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            hp--;
            hitParticle.Play();

            ObstacleObjectPool.Instance.Return(collision.gameObject); //Object Pool

            Debug.Log("HP: " + hp);

            if (hp <= 0)
            {
                gameOver = true;
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
                explosionParticle.Play();
                dirtParticle.Stop();
                playerAudio.PlayOneShot(crashSfx);
                Debug.Log("Game Over!");
            }
        }
    }
}
