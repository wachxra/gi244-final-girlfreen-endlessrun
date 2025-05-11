using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] coinSpawnPoints;

    public Vector3 spawnPos = new Vector3(25, 0, 0);
    public float startDelay = 2;
    public float repeatRate = 2;

    public float coinSpawnRate = 3f;

    private PlayerController playerController;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
        InvokeRepeating(nameof(SpawnCoin), startDelay + 1f, coinSpawnRate);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnCoin()
    {
        if (!playerController.gameOver)
        {
            int index = Random.Range(0, coinSpawnPoints.Length);
            Vector3 coinPos = coinSpawnPoints[index].position;

            if (Physics.CheckSphere(coinPos, 0.5f, LayerMask.GetMask("Obstacle"))) return;

            GameObject coin = CoinObjectPool.Instance.Acquire(coinPos, Quaternion.identity);
        }
    }



    void SpawnObstacle()
    {
        if (!playerController.gameOver)
        {
            GameObject obj = ObstacleObjectPool.Instance.Acquire(spawnPos, Quaternion.identity);
        }
    }
}