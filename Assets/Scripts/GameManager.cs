using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct UIElements{
  public TMP_Text playerHealthText;
  public TMP_Text enemiesKilledText;
  public TMP_Text timeTakenText;
  public TMP_Text timeTakenTitle;
  public TMP_Text winLoseText;
  public TMP_Text instructions;
}

public class GameManager : MonoBehaviour {

  // Reference to player
  [SerializeField] private Transform player;

  // Reference to Maze Generator
  [SerializeField] private MazeGenerator mazeGenerator;

  [Header("Canvas controls")]
  [SerializeField] private GameObject gameoverCanvas;
  [SerializeField] private GameObject hudCanvas;

  // Stats
  public UIElements mainUI;

  // Earth Sheild UI
  public GameObject earthShield;
  // Final Portal to enable when gamedone
  public GameObject finalPortal;

  // Gems
  [SerializeField] private GameObject[] gems;
  public ElementType currentElement = ElementType.NONE;
  public HashSet<ElementType> inventory = new HashSet<ElementType>();
  private int gemsPlaced = 0;

  // Portal
  public int tunnel = 0;
  public List<Transform> portals;

  #region Singleton and Game Setup
  public static GameManager instance;
  void Awake() {
    if (instance == null)
      instance = this;
    else {
      Destroy(gameObject);
      return;
    }
    DontDestroyOnLoad(gameObject);

    GameSetup();
  }

  void CanvasSettings(bool gameStart){
    // Enable these
    hudCanvas.SetActive(gameStart);
    // Disable these
    gameoverCanvas.SetActive(!gameStart);
    finalPortal.SetActive(!gameStart);
  }

  // Setup the game
  void GameSetup(){
    CanvasSettings(true);
    SpawnGems();
  }
  #endregion

  private void Start(){
    inventory.Add(ElementType.NONE);
    earthShield.SetActive(false);
  }

  #region Gems
  private void SpawnGems(){
    for (int i = 0; i < gems.Length; i++) {
      // Spawn at pos
      // Random x and z position
      int x = Random.Range(5, mazeGenerator.gridDimensions.x) * mazeGenerator.scaling;
      int z = Random.Range(5, mazeGenerator.gridDimensions.y) * mazeGenerator.scaling;
      Vector3 pos = new Vector3(x, 2f, z);
      gems[i] = Instantiate(gems[i], pos, Quaternion.Euler(-90f, 0f, 0f), transform);

      // Set gems as triggers
      var col = gems[i].GetComponent<Collider>();
      col.isTrigger = true;
    }
  }

  public void AddElement(ElementType et) {
    inventory.Add(et);
    SetCurrentElement(et);
  }

  public void SetCurrentElement(ElementType et) {
    // Check to see if player has this element
    if (!inventory.Contains(et)) {
      mainUI.instructions.text = "You haven't found this element yet.";
      return;
    }

    currentElement = et;

    if(et == ElementType.EARTH){
      earthShield.SetActive(true);
    }
    else{
      earthShield.SetActive(false);
    }
    
    // TODO: Notify player (This is shit)
    player.GetComponent<Shooting>().SetElement(currentElement);
  }

  public void GemPlaced() {
    gemsPlaced++;
    if (gemsPlaced >= 4) {
      mainUI.instructions.text =
          "You did it! Jump Into the Portal to go back to the future";
      finalPortal.SetActive(true);
    }
  }
  #endregion

  private void Update() {
    if (Input.GetKeyDown(KeyCode.Alpha1)) {
      SetCurrentElement(ElementType.FIRE);
    }
    if (Input.GetKeyDown(KeyCode.Alpha2)) {
      SetCurrentElement(ElementType.WATER);
    }
    if (Input.GetKeyDown(KeyCode.Alpha3)) {
      SetCurrentElement(ElementType.AIR);
    }
    if (Input.GetKeyDown(KeyCode.Alpha4)) {
      SetCurrentElement(ElementType.EARTH);
    }
    if (Input.GetKeyDown(KeyCode.Alpha5)) {
      SetCurrentElement(ElementType.NONE);
    }
  }

  private void LockCursor(bool toLock){
    Cursor.visible = !toLock;
    if(toLock) Cursor.lockState = CursorLockMode.Locked;
    else Cursor.lockState = CursorLockMode.None;
  }

  // Call when game finished, with winning condition
  // Very shitty code
  public void GameDone(bool won) {
    // Pause Time
    Time.timeScale = 0;

    // Cursor
    LockCursor(false);

    // Get Data for UI
    int health = player.GetComponent<Player>().health;
    int enemies_killed = player.GetComponent<Shooting>().enemies_killed;

    // UI Updates
    mainUI.playerHealthText.text = won ? health.ToString() : "DEAD";
    mainUI.enemiesKilledText.text = enemies_killed.ToString();
    mainUI.winLoseText.text = won ? "YOU WIN :) !!" : "You Lost :(";

    CanvasSettings(false);
  }

  // Call When Restarting game
  public void GameRestart() {
    // Time back to normal
    Time.timeScale = 1;

    // Canvas Updates
    CanvasSettings(true);

    // Cursor Setup again
    LockCursor(true);
  }
}
