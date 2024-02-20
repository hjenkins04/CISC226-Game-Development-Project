using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Just matching the camera pos with player pos
        Transform playerPos = player.transform;

        transform.position = new Vector3(playerPos.position.x, playerPos.position.y, -10);
    }
}
