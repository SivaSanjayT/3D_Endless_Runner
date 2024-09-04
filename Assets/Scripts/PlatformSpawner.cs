using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [Header("Platform Settings")]
    public GameObject[] PlatformPrefab;
    public int StartPoolSize = 10;
    public int spawnCount;
    public float deactivateDistance;

    [Header("Vectors")]
    public Vector3 SpawnPoint;

    [Header("playerTransform")]
    public Transform playerTransform;

    private Dictionary<GameObject, List<GameObject>> PooledPrefabs;
    private List<GameObject> ActivePlatfroms;

    public static PlatformSpawner Instance;

    private void Awake()
    { 
            Instance = this;
    }

    private void Start()
    {
        InitializePool();

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnPlatform();
        }
    }

    private void Update()
    {
        CheckPlatformsBehindPlayer();
    }
    public void SpawnPlatform()
    {
        GameObject SelectedPrefab = PlatformPrefab[Random.Range(0, PlatformPrefab.Length)];
        GameObject SelectedPlatform = GetPooledPlatform(SelectedPrefab);

        if (SelectedPlatform != null)
        {
            SelectedPlatform.transform.position = SpawnPoint;
            SelectedPlatform.SetActive(true);
            ActivePlatfroms.Add(SelectedPlatform);
            SpawnPoint = SelectedPlatform.transform.GetChild(0).position;
        }
    }
    private void InitializePool()
    {
        PooledPrefabs = new Dictionary<GameObject, List<GameObject>> ();
        ActivePlatfroms = new List<GameObject>();

        foreach (GameObject prefab in PlatformPrefab)
        {
            List<GameObject> pool = new List<GameObject>();

            for (int s = 0; s < StartPoolSize; s++)
            {
                GameObject Platform = Instantiate(prefab);
                Platform.SetActive(false);
                pool.Add(Platform);
            }
            PooledPrefabs[prefab] = pool;
        }
    }

    public GameObject GetPooledPlatform(GameObject prefab)
    {
        if (PooledPrefabs.ContainsKey(prefab))
        {
            List<GameObject> pool = PooledPrefabs[prefab];

            foreach (GameObject Platforms in pool)
            {
                if (!Platforms.activeInHierarchy)
                {
                    return Platforms;
                }
            }

            GameObject NewPlatform = Instantiate(prefab);
            NewPlatform.SetActive(false);
            pool.Add(NewPlatform);
            return NewPlatform;
        }
        else { return null; }
    }

    public void ReturnToPool(GameObject platform)
    {
        platform.SetActive(false);
        ActivePlatfroms.Remove(platform);
    }

    public void CheckPlatformsBehindPlayer()
    {
        float playerZ = playerTransform.position.z;

        for (int i = ActivePlatfroms.Count - 1; i >= 0; i--)
        {
            GameObject platform = ActivePlatfroms[i];
            float platformZ = platform.transform.position.z;

           
            if (platformZ < playerZ - deactivateDistance)
            {
                ReturnToPool(platform);
            }
        }
    }
}