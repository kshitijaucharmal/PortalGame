using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class MazeGenerator : MonoBehaviour {

  public int scaling = 5;
  public Vector2Int gridDimensions = new Vector2Int(10, 10);
  public Vector2Int playerSpawnPosition = new Vector2Int(1, 1);

  [SerializeField] private GameObject cellPrefab;
  [SerializeField] private GameObject playerPrefab;

  // Maze gen variables
  private Cell[,] cells;
  private Stack<Vector2Int> visited = new Stack<Vector2Int>();
  private Vector2Int current;

  // Public for editor
  [HideInInspector] public bool completed = false;
  [HideInInspector] public bool generatedBlocks = false;

  // Create maze in awake
  void Awake() {
    if (!generatedBlocks) GenerateBlocks();
    while (!completed) Step();
    BakeNavMesh();

    SpawnPlayer();
  }

  void SpawnPlayer(){
    Vector3 pos = new Vector3(playerSpawnPosition.x * scaling + scaling, 1, playerSpawnPosition.y * scaling + scaling);
    var playerRef = Instantiate(playerPrefab, pos, Quaternion.identity).GetComponent<PlayerMovement>();
  }

  public void GenerateBlocks() {
    GameObject mazeObj = new GameObject("Maze");
    mazeObj.transform.parent = transform;
    cells = new Cell[gridDimensions.x, gridDimensions.y];
    for (int i = 0; i < gridDimensions.x; i++) {
      for (int j = 0; j < gridDimensions.y; j++) {
        Vector3 pos =
            new Vector3(i * scaling + scaling, 0, j * scaling + scaling);
        GameObject obj = Instantiate(cellPrefab, pos, Quaternion.identity,
                                     mazeObj.transform);
        cells[i, j] = obj.GetComponent<Cell>();
      }
    }
    generatedBlocks = true;
  }

  public void Step() {
    if (completed)
      return;
    Cell currentCell = cells[current.x, current.y];
    // Set visited
    currentCell.visited = true;
    // While unvisited cells
    Vector2Int[] neighbors = GetUnvisitedNeighbors(current.x, current.y);

    if (neighbors.Length > 0) {

      // Random neighbor
      Vector2Int neighbor = neighbors[Random.Range(0, neighbors.Length)];
      // Set visited
      cells[neighbor.x, neighbor.y].visited = true;
      // Push to stack
      visited.Push(new Vector2Int(current.x, current.y));

      // Remove walls to create path
      RemoveAdjacentWalls(current.x, current.y, neighbor.x, neighbor.y);

      // Set neighbor as current
      current.Set(neighbor.x, neighbor.y);
    } else if (visited.Count > 0) {
      var n = visited.Pop();

      current.Set(n.x, n.y);
    } else {
      Debug.Log("Done Maze");
      completed = true;
    }
  }

  public void BakeNavMesh() {
    GameObject navMeshObj = new GameObject("EnemyNavMesh");
    navMeshObj.transform.parent = transform;
    var navMesh = navMeshObj.AddComponent<NavMeshSurface>();
    navMesh.BuildNavMesh();
  }

  Vector2Int[] GetUnvisitedNeighbors(int x, int y) {
    List<Vector2Int> neighs = new List<Vector2Int>();
    if (x > 0) {
      if (!cells[x - 1, y].visited)
        neighs.Add(new Vector2Int(x - 1, y));
    }
    if (x < gridDimensions.x - 1) {
      if (!cells[x + 1, y].visited)
        neighs.Add(new Vector2Int(x + 1, y));
    }
    if (y > 0) {
      if (!cells[x, y - 1].visited)
        neighs.Add(new Vector2Int(x, y - 1));
    }
    if (y < gridDimensions.y - 1) {
      if (!cells[x, y + 1].visited)
        neighs.Add(new Vector2Int(x, y + 1));
    }

    return neighs.ToArray();
  }

  void RemoveAdjacentWalls(int x1, int y1, int x2, int y2) {
    int x = x1 - x2;
    int y = y1 - y2;

    // X Axis
    if (x == 1) {
      cells[x2, y2].RemoveRight();
    } else if (x == -1) {
      cells[x1, y1].RemoveRight();
    }
    // Y Axis
    if (y == 1) {
      cells[x2, y2].RemoveTop();
    } else if (y == -1) {
      cells[x1, y1].RemoveTop();
    }
  }
}
