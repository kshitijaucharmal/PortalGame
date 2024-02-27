using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject[] gems;

    [SerializeField] private Transform player;
    [SerializeField] private Transform dirArrow;


    private int selectedGem = 0;
    private Vector3 gemPos;

    // Start is called before the first frame update
    void Start()
    {
        // Instantiate gems
        gemPos = gems[selectedGem].transform.position;
    }

    // Update is called once per frame
    void Update() {
        gemPos.y = player.position.y;
        var dir = (gemPos - player.position).normalized;
        var angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
        dirArrow.rotation = Quaternion.Euler(0f, -angle, 0f);
    }
}
