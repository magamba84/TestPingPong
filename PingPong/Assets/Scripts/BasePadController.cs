using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePadController : MonoBehaviour
{
    protected GameObject ball;
    public void Init(GameObject ball)
    {
        this.ball = ball;
    }
}
