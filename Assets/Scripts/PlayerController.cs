using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float gravityModifier;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public ParticleSystem obstacleHitParticle;

    public AudioClip jumpSfx;
    public AudioClip crashSfx;

    private Rigidbody rb;
    private InputAction jumpAction;

    private int jumpCount = 0;
    private int maxJumpCount = 1;

    private Animator playerAnim;
    private AudioSource playerAudio;

    public bool gameOver = false;

    public bool isDashing = false;

    public int playerHP = 3;

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
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isDashing = true;
        }
        else
        {
            isDashing = false;
        }

        if (jumpAction.triggered && jumpCount < maxJumpCount && !gameOver)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
            jumpCount++;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSfx);
        }
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
            playerHP--;

            if (explosionParticle != null)
            {
                explosionParticle.Stop();
                explosionParticle.Clear();
                explosionParticle.Play();
            }

            if (playerAudio != null && crashSfx != null)
            {
                playerAudio.PlayOneShot(crashSfx);
            }

            if (playerHP <= 0)
            {
                Debug.Log("Game Over! HP is 0");
                gameOver = true;
                playerAnim.SetBool("Death_b", true);
                playerAnim.SetInteger("DeathType_int", 1);
            }
            else
            {
                Debug.Log("HP remaining: " + playerHP);
            }

            ObstacleObjectPool.Instance.Return(collision.gameObject);
        }
    }
}