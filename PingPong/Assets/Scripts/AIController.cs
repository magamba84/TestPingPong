using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : BasePadController
{

    void Update()
    {
        var pos = transform.position;
        pos.x = ball.transform.position.x;
        transform.position = pos;
    }
}
