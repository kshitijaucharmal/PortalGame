using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour {

  public GameObject rightWall;
  public GameObject topWall;

  public GameObject rightWallGFX;
  public GameObject rightWallPortal;
  public GameObject topWallGFX;
  public GameObject topWallPortal;

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
}
