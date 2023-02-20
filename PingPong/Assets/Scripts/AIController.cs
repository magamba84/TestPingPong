using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [SerializeField] GameObject ball;
    

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        pos.x = ball.transform.position.x;
        transform.position = pos;
    }
}
