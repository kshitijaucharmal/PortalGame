using UnityEngine;
using UnityEngine.UI;

public class ElementCollection : MonoBehaviour {
  private bool isElementCollected = false;
  public Button yourUIButton; // Reference to your UI button

  // Call this method when the element is collected
  public void CollectElement() {
    isElementCollected = true;
    ShowHideButton();
  }

  // Method to show/hide the button based on element collection
  private void ShowHideButton() {
    Color buttonColor = yourUIButton.GetComponent<Image>().color;

    // Set opacity to 1 if the element is collected, else set it to 0
    buttonColor.a = isElementCollected ? 1f : 0f;

    // Apply the updated color to the button
    yourUIButton.GetComponent<Image>().color = buttonColor;
  }
}
