using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonFader : MonoBehaviour
{
    private Button button;
    public GameObject playerGameObject;
    public GameObject floatingTextPrefab; // Reference to the TextMeshPro UI prefab
    public Transform canvasTransform;
    public static bool toggle = false;


    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        PlayerStats playerStats = playerGameObject.GetComponent<PlayerStats>();
        if (playerStats.coins >= 3)
        {
            toggle = true;
            playerStats.coins -= 3;
            // Optionally, you can update your UI to reflect the new coin count here
            // For example, you might have a Text component displaying the coin count
            // and you can update its text accordingly.
            // coinCountText.text = "Coins: " + PlayerStats.coins.ToString();

            // Disable the button
            button.interactable = false;
            // Optionally, change the button's color to grey
            var colors = button.colors;
            colors.normalColor = Color.grey;
            button.colors = colors;
        }
        else
        {
            ShowFloatingText("Not enough Coins!");
            // Optionally, you can display a message indicating insufficient coins
            Debug.Log("Insufficient coins to buy checkpoint!");
        }
    }

    public void ShowFloatingText(string message)
    {
   GameObject floatingTextObject = Instantiate(floatingTextPrefab, canvasTransform);
    TextMeshProUGUI floatingText = floatingTextObject.GetComponent<TextMeshProUGUI>();
    if (floatingText != null)
    {
        floatingText.text = message;
        floatingText.alignment = TextAlignmentOptions.Center; // Align text to the center
        StartCoroutine(FadeOutAndDestroy(floatingTextObject));
    }
    else
    {
        Debug.LogError("The floatingTextPrefab doesn't contain a TextMeshProUGUI component!");
    }
    }

    private IEnumerator FadeOutAndDestroy(GameObject floatingTextObject)
    {
        float duration = 1f;
        float timer = 0f;
        TextMeshProUGUI floatingText = floatingTextObject.GetComponent<TextMeshProUGUI>();
        Color startColor = floatingText.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0);

        while (timer < duration)
        {
            timer += Time.deltaTime;
            floatingText.color = Color.Lerp(startColor, endColor, timer / duration);
            yield return null;
        }

        Destroy(floatingTextObject);
    }
}