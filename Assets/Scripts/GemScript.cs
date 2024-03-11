using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using TMPro;

public enum ElementType { FIRE, WATER, EARTH, AIR, NONE }
public class GemScript : MonoBehaviour {

  public ElementType elementType;
  private TMP_Text instructions;

  // Start is called before the first frame update
  void Start() {
    instructions = GameObject.FindGameObjectWithTag("Instructions").GetComponent<TMP_Text>();
  }

  // Update is called once per frame
  void Update() {}

  void OnTriggerEnter(Collider other) {
    if (other.CompareTag("Player")) {
      Destroy(gameObject);
      if (elementType == ElementType.FIRE){
        instructions.text = "Shoot Fire Bullets, Portal Takes you back to Altar";
      }
      else if(elementType == ElementType.WATER){
        instructions.text = "Shoot Water Bullets, Portal Increases health";
      }
      else if(elementType == ElementType.AIR){
        instructions.text = "Shoot Air Bullets, Portal Sucks Enemies";
      }
      else if(elementType == ElementType.EARTH){
        instructions.text = "Shoot Stone Bullets, Get a shield while using this, Portals Dig through Walls";
      }
      // notify gameManager That we got this element and add to invetory
      GameManager.instance.AddElement(elementType);
    }
  }
}
