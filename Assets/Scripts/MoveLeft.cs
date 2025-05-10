using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed = 10f;
    private float leftBound = -15;
    private PlayerController playerController;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (!playerController.gameOver)
        {
            float currentSpeed = playerController.isSprinting ? speed * 2 : speed;
            transform.Translate(Vector3.left * Time.deltaTime * currentSpeed);
        }

        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            ObstacleObjectPool.Instance.Return(gameObject); //Object Pool
        }
    }
}