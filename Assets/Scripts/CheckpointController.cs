using UnityEngine;
using System.Collections;

public class CheckpointController : MonoBehaviour
{
    // Array to store the positions of checkpoints
    public Vector2[] checkpoints;

    // The index of the latest checkpoint the player has reached
    private int latestCheckpointIndex = 0;

    public Transform playerTransform;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Call this method with the index of the checkpoint the player reached
    public void UpdateLatestCheckpoint(int checkpointIndex)
    {
        StartCoroutine(WaitBeforeUpdate(checkpointIndex));
    }

    IEnumerator WaitBeforeUpdate(int checkpointIndex)
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(0.5f);

        if (ButtonFader.toggle)
        {
            if (checkpointIndex >= 0 && checkpointIndex < checkpoints.Length && checkpointIndex > latestCheckpointIndex)
            {
                latestCheckpointIndex = checkpointIndex;
                Debug.Log("Checkpoint updated to: " + checkpointIndex);
                ButtonFader.toggle = false;
            }
            else
            {
                Debug.LogError("Invalid checkpoint index");
            }
        }
    }

    // Call this function to move the player to the latest checkpoint
    public void RespawnAtLatestCheckpoint()
    {
        playerTransform.position = checkpoints[latestCheckpointIndex];
        Debug.Log("Player respawned at checkpoint: " + latestCheckpointIndex);
        
    }
}
