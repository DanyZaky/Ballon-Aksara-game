using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonSpawner : MonoBehaviour
{
    public Transform[] spawnPoints; // Array posisi spawn
    public GameObject prefabCorrect, prefabUncorrect; // Prefab yang akan di-spawn
    public int spawnCount = 20; // Jumlah spawn yang diinginkan
    public int prefabSpawnCorrectCount;

    private List<int> spawnedIndexes = new List<int>(); // Menyimpan indeks yang telah di-spawn
    private GameObject prefabToSpawn;

    void Start()
    {
        StartCoroutine(SpawnPrefabRepeatedly());
    }

    IEnumerator SpawnPrefabRepeatedly()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnPrefab();
            yield return new WaitForSeconds(.1f); // Delay antara spawn, sesuaikan sesuai kebutuhan
        }
    }

    void SpawnPrefab()
    {
        // Memilih indeks yang belum di-spawn
        int randomIndex = GetRandomSpawnIndex();

        // Mengecek apakah prefab telah di-spawn di semua indeks
        if (randomIndex == -1)
        {
            Debug.LogWarning("Semua spawn point telah di-spawn.");
            return;
        }

        if (prefabSpawnCorrectCount > 0)
        {
            prefabToSpawn = prefabCorrect;
            prefabSpawnCorrectCount -= 1;
        }
        else
        {
            prefabToSpawn = prefabUncorrect;
        }

        // Spawn prefab di posisi yang dipilih
        Instantiate(prefabToSpawn, spawnPoints[randomIndex].position, Quaternion.identity);

        // Menambah indeks yang telah di-spawn ke dalam list
        spawnedIndexes.Add(randomIndex);
    }

    int GetRandomSpawnIndex()
    {
        List<int> availableIndexes = new List<int>();

        // Memeriksa indeks yang belum di-spawn
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (!spawnedIndexes.Contains(i))
            {
                availableIndexes.Add(i);
            }
        }

        // Mengembalikan indeks yang masih tersedia secara acak
        if (availableIndexes.Count > 0)
        {
            int randomIndex = Random.Range(0, availableIndexes.Count);
            return availableIndexes[randomIndex];
        }
        else
        {
            // Mengembalikan -1 jika semua indeks telah di-spawn
            return -1;
        }
    }
}
