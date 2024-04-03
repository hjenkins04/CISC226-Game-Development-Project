using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseMenuPanel; // Assign this in the inspector

    private bool isPaused = false;

    private void Start()
    {
        // Ensure the pause menu is not visible when the game starts
        pauseMenuPanel.SetActive(false);
    }

    void Update()
    {
        // Check for the pause key (Escape in this example) being pressed
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKey(KeyCode.P))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pauseMenuPanel.SetActive(isPaused); // Show or hide the pause menu panel
    }
}
