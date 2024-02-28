using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private GameObject[] gems;

    [SerializeField] private Transform player;
    [SerializeField] private Transform dirArrow;
    [SerializeField] private MazeGenerator mazeGenerator;

    private Vector3 gemPos;

    // Start is called before the first frame update
    void Start() {
        // Instantiate Gems
        for(int i = 0; i < gems.Length; i++){
            float x = Random.Range(0, mazeGenerator.gridDimensions.x * mazeGenerator.scaling);
            float z = Random.Range(0, mazeGenerator.gridDimensions.y * mazeGenerator.scaling);
            Vector3 pos = new Vector3(x, 2f, z);
            gems[i] = Instantiate(gems[i], pos, Quaternion.identity, transform);
        }

        // Set Current Position
        SetTarget(0);
    }

    void SetTarget(int gemIndex){
        gemPos = gems[gemIndex].transform.position;
    }

    // Update is called once per frame
    void Update() {
        gemPos.y = player.position.y;
        var dir = (gemPos - player.position).normalized;
        var angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        dirArrow.rotation = Quaternion.Euler(0f, -angle, 0f);
    }
}