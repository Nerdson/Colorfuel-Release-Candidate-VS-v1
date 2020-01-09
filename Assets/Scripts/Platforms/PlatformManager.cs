using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public static PlatformManager Instance = null;

    [SerializeField]
    GameObject platformPrefab;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    IEnumerator SpawnPlatform(Vector3 spawnPosition)
    {
        yield return new WaitForSeconds(4f);
        Instantiate(platformPrefab, spawnPosition, platformPrefab.transform.rotation);
    }
}
