using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

  [Header("Wall")]
  public GameObject rightWall;
  public GameObject topWall;

  [Header("Wall GFX")]
  public GameObject rightWallGFX;
  public GameObject rightWallPortal;
  public GameObject topWallGFX;
  public GameObject topWallPortal;

  [Header("MazeGen Helper")]
  public GameObject airElement;
  public GameObject fireElement;
  public GameObject earthElement;
  public Earth earthSc;

  [Header("MazeGen Helper")]
  public bool visited = false;

  public void ConvertRight() {
    rightWallGFX.SetActive(false);
    rightWallPortal.SetActive(true);
    rightWall.GetComponent<Collider>().enabled = false;
  }
  public void ConvertTop() {
    topWallGFX.SetActive(false);
    topWallPortal.SetActive(true);
    topWall.GetComponent<Collider>().enabled = false;
  }

  public void RemoveRight() { DestroyImmediate(rightWall); }

  public void RemoveTop() { DestroyImmediate(topWall); }

  public void OpenAirPortal() { airElement.SetActive(true); }
  public void OpenFirePortal() { fireElement.SetActive(true); }
  public void OpenEarthPortal() { earthElement.SetActive(true); }
  public Earth ReturnEarthPortal(){
    return earthSc;
  }
}
