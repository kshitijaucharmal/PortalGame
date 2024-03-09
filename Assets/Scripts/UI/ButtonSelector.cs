using UnityEngine;
using UnityEngine.UI;

public class ButtonScroll : MonoBehaviour {
  public Button[] buttons; // Array containing the button presets in the scene
  private int currentIndex = 0; // Index of the currently selected button

  private void Start() {
    // Attach a custom method to handle button clicks for each button
    for (int i = 0; i < buttons.Length; i++) {
      int buttonIndex = i; // Capture the current index in a local variable
      buttons[i].onClick.AddListener(() => OnButtonClick(buttonIndex));
    }
  }

  private void Update() {
    // Scroll the mouse to change the index of the array
    float scroll = Input.mouseScrollDelta.y;
    if (scroll != 0) {
      ChangeIndex((int)Mathf.Sign(scroll));
    }
  }

  private void ChangeIndex(int direction) {
    // Change the index of the array based on scroll direction
    currentIndex = (currentIndex + direction + buttons.Length) % buttons.Length;

    // Update the button opacity based on the new index
    UpdateButtonOpacity();
  }

  private void OnButtonClick(int clickedIndex) {
    // Update the opacity of all buttons based on the clicked index
    for (int i = 0; i < buttons.Length; i++) {
      Color buttonColor = buttons[i].GetComponent<Image>().color;

      // Set opacity to 1 for the clicked button, and 0 for others
      buttonColor.a = (i == clickedIndex) ? 1f : 0f;

      // Apply the updated color to the button
      buttons[i].GetComponent<Image>().color = buttonColor;
    }

    // Set the current index to the clicked index
    currentIndex = clickedIndex;
  }

  private void UpdateButtonOpacity() {
    // Update the opacity of all buttons based on the current index
    for (int i = 0; i < buttons.Length; i++) {
      Color buttonColor = buttons[i].GetComponent<Image>().color;

      // Set opacity to 1 for the selected button, and 0 for others
      buttonColor.a = (i == currentIndex) ? 1f : 0f;

      // Apply the updated color to the button
      buttons[i].GetComponent<Image>().color = buttonColor;
    }
  }
}