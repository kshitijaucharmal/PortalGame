using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

    public GameObject rightWall;
    public GameObject topWall;
    public bool visited = false;

    public void RemoveRight(){
        DestroyImmediate(rightWall);
    }

    public void RemoveTop(){
        DestroyImmediate(topWall);
    }
}
