using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScript : MonoBehaviour
{
    public bool canInsert = false;

    // Update is called once per frame
    void Update()
    {
        // If E button pressed
        if(canInsert && Input.GetButtonDown("Interact")){
            
        }
    }


    void OnCollisionEnter(Collision other){
        if (other.transform.CompareTag("Player")){
            canInsert = true;
            // Enable UI
        }
    }
    void OnCollisionExit(Collision other){
        if (other.transform.CompareTag("Player")){
            canInsert = false;
            // Disable UI
        }
    }
}
