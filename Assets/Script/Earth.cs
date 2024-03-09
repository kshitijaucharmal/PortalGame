using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using TMPro;

public class Earth : MonoBehaviour
{
    private Transform Floor;
    public Vector3 offset = new Vector3(2, 0, 2);
    private TMP_Text instructions;

    void Start(){
        instructions = GameObject.FindGameObjectWithTag("Instructions").GetComponent<TMP_Text>();
    }

    public void SetTarget(Transform floor){
        Floor = floor;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (Floor == null){
                instructions.text = "Shoot another earth portal to connect";
                return;
            }
            var player = GameObject.FindGameObjectWithTag("Player").transform;
            player.position = Floor.position + offset;
        }
    }

}

