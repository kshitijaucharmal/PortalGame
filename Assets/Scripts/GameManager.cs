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
}

public class GameManager : MonoBehaviour {

  [SerializeField] private GameObject[] gems;

  [SerializeField] private Transform player;
  [SerializeField] private MazeGenerator mazeGenerator;

  [Header("Canvas controls")]
  [SerializeField] private GameObject gameoverCanvas;
  [SerializeField] private GameObject hudCanvas;

  [Header("Stats")]
  public UIElements mainUI;

  [Header("Earth Shield")]
  public GameObject earthShield;
  public GameObject finalPortal;

  public ElementType currentElement = ElementType.NONE;
  public HashSet<ElementType> inventory = new HashSet<ElementType>();

  void Start(){
    inventory.Add(ElementType.NONE);
    earthShield.SetActive(false);
  }

  public TMP_Text instructions;

  public void AddElement(ElementType et) {
    inventory.Add(et);
    SetCurrentElement(et);
  }

  public int tunnel = 0;
  public List<Transform> portals;

  public void SetCurrentElement(ElementType et) {
    // Check to see if player has this element
    if (!inventory.Contains(et)) {
      instructions.text = "You haven't found this element yet.";
      return;
    }

    currentElement = et;

    if(et == ElementType.EARTH){
      earthShield.SetActive(true);
    }
    else{
      earthShield.SetActive(false);
    }
    
    // Notify player
    player.GetComponent<Shooting>().SetElement(currentElement);
  }

  void Update() {
    TakeInput(false);
  }

  void TakeInput(bool debug=false){
    if(!debug){
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
    // Debug
    else{
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
      if (Input.GetKeyDown(KeyCode.Alpha5)) {
        AddElement(ElementType.NONE);
      }
    }
  }

  private int gemsPlaced = 0;

  public void GemPlaced() {
    gemsPlaced++;
    if (gemsPlaced >= 4) {
      instructions.text =
          "You did it! Jump Into the Portal to go back to the future";
      finalPortal.SetActive(true);
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
    finalPortal.SetActive(false);

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
    int health = player.GetComponent<Player>().health;
    int enemies_killed = player.GetComponent<Shooting>().enemies_killed;

    // UI Updates
    mainUI.playerHealthText.text = won ? health.ToString() : "DEAD";
    mainUI.enemiesKilledText.text = enemies_killed.ToString();
    mainUI.winLoseText.text = won ? "YOU WIN :) !!" : "You Lost :(";

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
