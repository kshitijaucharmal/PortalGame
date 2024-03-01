using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject[] gems;

    [SerializeField] private Transform player;
    [SerializeField] private Transform dirArrow;
    [SerializeField] private MazeGenerator mazeGenerator;
    [SerializeField] private Timer timer;
    
    [Header("Canvas controls")]
    [SerializeField] private GameObject gameoverCanvas;
    [SerializeField] private GameObject hudCanvas;

    [Header("Stats")]
    [SerializeField] private TMP_Text playerHealthText;
    [SerializeField] private TMP_Text enemiesKilledText;
    [SerializeField] private TMP_Text timeTakenText;
    [SerializeField] private TMP_Text timeTakenTitle;
    [SerializeField] private TMP_Text winLoseText;

    private Vector3 gemPos;

    private int currentGem = 0;

    public static GameManager instance;
    //AudioManager

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start() {
        // Instantiate Gems
        for(int i = 0; i < gems.Length; i++){
            float x = Random.Range(0, mazeGenerator.gridDimensions.x * mazeGenerator.scaling);
            float z = Random.Range(0, mazeGenerator.gridDimensions.y * mazeGenerator.scaling);
            Vector3 pos = new Vector3(x, 2f, z);
            gems[i] = Instantiate(gems[i], pos, Quaternion.identity, transform);
            var col = gems[i].GetComponent<Collider>();
            col.isTrigger = true;
            col.enabled = false;
        }

        // Set Current Position
        SetTarget(currentGem);

        timer.StartTimer();
        gameoverCanvas.SetActive(false);
    }

    void SetTarget(int gemIndex){
        gemPos = gems[gemIndex].transform.position;
        gems[gemIndex].GetComponent<Collider>().enabled = true;
    }

    public void CollectGem(){
        Destroy(gems[currentGem]);
        currentGem++;
        if(currentGem >= gems.Length){
            // Game Completed
            GameDone(true);
        }
        SetTarget(currentGem);
    }

    public void GameDone(bool won){
        Time.timeScale = 0;
        timer.StopTimer();
        // Cusor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        var player = GameObject.FindGameObjectWithTag("Player");
        int health = player.GetComponent<PlayerMovement>().GetHealth();
        int enemies_killed = player.GetComponent<Shooting>().enemies_killed;
        string timeString = timer.timeString;

        playerHealthText.text = won ? health.ToString() : "DEAD";
        enemiesKilledText.text = enemies_killed.ToString();
        timeTakenText.text = timeString;
        timeTakenTitle.text = won ? "Beat Maze in:" : "Got Ass kicked for:";
        winLoseText.text = won ? "YOU WIN :) !!" : "You Lost :(";

        CanvasCleanup();
    }

    public void CanvasCleanup(){
        gameoverCanvas.SetActive(true);
        hudCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update() {
        if (player == null) return;
        gemPos.y = player.position.y;
        var dir = (gemPos - player.position).normalized;
        var angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        dirArrow.rotation = Quaternion.Euler(0f, -angle, 0f);
    }
}