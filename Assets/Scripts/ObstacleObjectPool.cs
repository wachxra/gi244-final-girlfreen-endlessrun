using System.Collections.Generic;
using UnityEngine;

public class ObstacleObjectPool : MonoBehaviour
{
    public GameObject[] prefabVariants;
    public int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();

    public static ObstacleObjectPool Instance;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            int index = i % prefabVariants.Length;
            GameObject obj = Instantiate(prefabVariants[index]);
            obj.SetActive(false);
            pool.Enqueue(obj);
        }
    }

    public GameObject Acquire(Vector3 position, Quaternion rotation)
    {
        GameObject obj;

        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
        }
        else
        {
            int index = Random.Range(0, prefabVariants.Length);
            obj = Instantiate(prefabVariants[index]);
        }

        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        return obj;
    }

    public void Return(GameObject obj)
    {
        obj.SetActive(false);
        pool.Enqueue(obj);
    }
}