using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Vector3 spawnPos = new Vector3(25, 0, 0);
    public float startDelay = 2;
    public float repeatRate = 2;

    private PlayerController playerController;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void SpawnObstacle()
    {
        if (!playerController.gameOver)
        {
            GameObject obj = ObstacleObjectPool.Instance.Acquire(spawnPos, Quaternion.identity); //Object Pool
        }
    }
}