using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestPos : MonoBehaviour
{

    public Vector2 currentPos;
    // Start is called before the first frame update
    void Start()
    {
        currentPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentPos = transform.position;
    }
}
