using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

  [SerializeField]
  private GameObject[] gems;

  [SerializeField]
  private Transform player;
  [SerializeField]
  private MazeGenerator mazeGenerator;

  [Header("Canvas controls")]
  [SerializeField]
  private GameObject gameoverCanvas;
  [SerializeField]
  private GameObject hudCanvas;

  [Header("Stats")]
  [SerializeField]
  private TMP_Text playerHealthText;
  [SerializeField]
  private TMP_Text enemiesKilledText;
  [SerializeField]
  private TMP_Text timeTakenText;
  [SerializeField]
  private TMP_Text timeTakenTitle;
  [SerializeField]
  private TMP_Text winLoseText;

  private ElementType currentElement = ElementType.NONE;
  public HashSet<ElementType> inventory = new HashSet<ElementType>();

  public TMP_Text instructions;

  public void AddElement(ElementType et) {
    inventory.Add(et);
    SetCurrentElement(et);
  }

  public void SetCurrentElement(ElementType et) {
    // Check to see if player has this element
    if (!inventory.Contains(et)) {
      instructions.text = "You Don't Posses " + et;
      return;
    }
    currentElement = et;
    instructions.text = "Currently Equipped: " + et;

    if (inventory.Count >= 4) {
      Debug.Log("Every Element Collected");
    }
    // TODO: Update the UI (Now this element is with you)
    //
    // Notify player
    player.GetComponent<Shooting>().SetElement(currentElement);
  }

  void Update() {
    if (Input.GetKeyDown(KeyCode.Alpha1)) {
      AddElement(ElementType.FIRE);
    }
    if (Input.GetKeyDown(KeyCode.Alpha2)) {
      AddElement(ElementType.WATER);
    }
    if (Input.GetKeyDown(KeyCode.Alpha3)) {
      AddElement(ElementType.AIR);
    }
    if (Input.GetKeyDown(KeyCode.Alpha4)) {
      AddElement(ElementType.EARTH);
    }
  }

  public static GameManager instance;

  void Awake() {
    if (instance == null)
      instance = this;
    else {
      Destroy(gameObject);
      return;
    }

    DontDestroyOnLoad(gameObject);

    hudCanvas.SetActive(true);
    gameoverCanvas.SetActive(false);

    // Instantiate Gems
    for (int i = 0; i < gems.Length; i++) {
      int x = Random.Range(5, (int)(mazeGenerator.gridDimensions.x)) *
              mazeGenerator.scaling;
      int z = Random.Range(5, (int)(mazeGenerator.gridDimensions.y)) *
              mazeGenerator.scaling;

      Vector3 pos = new Vector3(x, 2f, z);
      gems[i] =
          Instantiate(gems[i], pos, Quaternion.Euler(-90f, 0f, 0f), transform);
      var col = gems[i].GetComponent<Collider>();
      // Set gems as triggers
      col.isTrigger = true;
    }
  }

  public void GameDone(bool won) {
    // Pause Time
    Time.timeScale = 0;

    // Cursor
    Cursor.visible = true;
    Cursor.lockState = CursorLockMode.None;

    // Get Data for UI
    int health = player.GetComponent<PlayerMovement>().GetHealth();
    int enemies_killed = player.GetComponent<Shooting>().enemies_killed;

    // UI Updates
    playerHealthText.text = won ? health.ToString() : "DEAD";
    enemiesKilledText.text = enemies_killed.ToString();
    winLoseText.text = won ? "YOU WIN :) !!" : "You Lost :(";

    // Canvas Updates
    gameoverCanvas.SetActive(true);
    hudCanvas.SetActive(false);
  }

  public void GameRestart() {
    // Canvas Updates
    gameoverCanvas.SetActive(false);
    hudCanvas.SetActive(true);

    // Time back to normal
    Time.timeScale = 1;

    // Cursor Setup again
    Cursor.visible = false;
    Cursor.lockState = CursorLockMode.Locked;
  }
}
