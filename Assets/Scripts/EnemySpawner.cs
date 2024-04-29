using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

  [SerializeField] private Vector2Int enemyNoRange = new Vector2Int(10, 20);
  [SerializeField] private GameObject enemyPrefab;
  [SerializeField] private Transform playerTransform;

  private List<EnemyScript> enemies = new();

  public void SpawnEnemies(int cell_size, int gridSize) {
    int n_enemies = Random.Range(enemyNoRange.x, enemyNoRange.y);
    for (int i = 0; i < n_enemies; i++) {
      int x = Random.Range(0, gridSize) * cell_size;
      int z = Random.Range(2, gridSize) * cell_size;
      Vector3 pos = new(x, 0, z);;
      EnemyScript enemy = Instantiate(enemyPrefab, pos, Quaternion.identity, transform).GetComponent<EnemyScript>();
      enemy.SetTarget(playerTransform);
      enemies.Add(enemy);
    }
  }
}
