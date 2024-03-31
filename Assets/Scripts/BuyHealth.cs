using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyHealth : MonoBehaviour
{

    public GameObject playerObject;
    public TextMeshPro floatingTextPrefab; // Drag your TextMeshPro prefab here in the Unity Editor
    private bool buttonClicked = false;

    void Update(){
        PlayerStats playerStats = playerObject.GetComponent<PlayerStats>();
        GetComponent<Button>().onClick.AddListener(OnClick);
         if (buttonClicked)
        {
            // Perform actions when the button is clicked
            Debug.Log("Button is clicked!");
            // Reset buttonClicked to false after performing actions

            if (playerStats != null)
        {

            if (playerStats.coins >= 3.0f){
                playerStats.coins-=3.0f;
                playerStats.health+=1.0f;
            }

            else{

                ShowFloatingText("Not enough coins!");
            }
            
            // Increase player's health

            // Optionally, you can update UI elements to reflect changes
        }

            buttonClicked = false;
        }
        

    }
    private void OnClick()
    {
        buttonClicked = true;
        // Get the PlayerStats component attached to the player object

    }

    private void ShowFloatingText(string message)
    {

        Vector3 centerScreenPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // Instantiate the floating text prefab
        TextMeshPro floatingText = Instantiate(floatingTextPrefab, centerScreenPosition, Quaternion.identity);

        // Set the text message
        floatingText.text = message;

        floatingText.sortingOrder = int.MaxValue;

        // Optionally, you can configure additional properties of the floating text (e.g., font size, color, etc.)

        // Animate the floating text (e.g., move upwards and fade away)
        StartCoroutine(FadeOutFloatingText(floatingText));
    }

    private IEnumerator FadeOutFloatingText(TextMeshPro floatingText)
    {
        // Animation duration in seconds
        float duration = 2.0f;

        // Initial alpha value
        float alpha = 1.0f;

        // Calculate the speed at which alpha decreases per second
        float fadeSpeed = 1.0f / duration;

        // Move the text upwards
        Vector3 startPosition = floatingText.transform.position;
        Vector3 targetPosition = startPosition + Vector3.up * 2.0f; // Adjust the upward movement distance as needed

        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            // Update alpha value
            alpha -= fadeSpeed * Time.deltaTime;

            // Update text color alpha
            Color textColor = floatingText.color;
            textColor.a = alpha;
            floatingText.color = textColor;

            // Move the text upwards gradually
            floatingText.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Destroy the floating text object after the animation
        Destroy(floatingText.gameObject);
    }
}
