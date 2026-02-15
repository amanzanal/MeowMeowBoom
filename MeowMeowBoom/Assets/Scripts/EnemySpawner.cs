using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private Tilemap river;

    private GameObject currentEnemy = null; // Referencia al enemigo activo
    private Transform playerTransform;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Cat").transform;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // Verificar si el enemigo actual ha sido destruido
            if (currentEnemy == null)
            {
                Vector2 randomPos = getSpawnNearPlayer(playerTransform.position, 3, 10); // Distancia min y max para reaparecer
                currentEnemy = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
            }

            // Disminuir el intervalo de manera progresiva
            spawnInterval = Mathf.Clamp(spawnInterval * 0.9f, 1f, 10f);
        }
    }

    private Vector2 getSpawnNearPlayer(Vector2 playerPos, float minDistance, float maxDistance)
    {
        Vector2 spawnPos = Vector2.zero;
        bool posValid = false;

        while (!posValid)
        {
            float distance = Random.Range(minDistance, maxDistance);
            float angle = Random.Range(0f, 360f);
            spawnPos = playerPos + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;

            if (isPositionValid(spawnPos))
            {
                posValid = true;
            }
        }

        return spawnPos;
    }

    //Posicion valida, que no salga en el rio
    private bool isPositionValid(Vector2 position)
    {
        Vector3Int cellPosition = river.WorldToCell(position);
        TileBase tileAtPosition = river.GetTile(cellPosition);

        // La posición es válida si no hay un tile del río en esa celda
        return tileAtPosition == null;
    }
}
