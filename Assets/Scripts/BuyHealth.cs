using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BuyHealth : MonoBehaviour
{
    public GameObject playerObject;
    public GameObject floatingTextPrefab; // Reference to the TextMeshPro UI prefab
    public Transform canvasTransform; // Drag your Canvas object here in the Unity Editor
    private bool buttonClicked = false;

    void Update()
    {
        PlayerStats playerStats = playerObject.GetComponent<PlayerStats>();
        GetComponent<Button>().onClick.AddListener(OnClick);
        if (buttonClicked)
        {
            Debug.Log("Button is clicked!");
            if (playerStats != null)
            {
                if (playerStats.coins >= 3.0f)
                {
                    playerStats.coins -= 3.0f;
                    playerStats.health += 1.0f;
                }
                else
                {
                    ShowFloatingText("Not enough coins");
                }
            }
            buttonClicked = false;
        }
    }

    private void OnClick()
    {
        buttonClicked = true;
    }

    private void ShowFloatingText(string message)
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
