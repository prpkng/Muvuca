using System;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State
{
    
    public Vector3 direction;

    public override void Enter(string[] data = null)
    {
        direction = Util.DeserializeVector3Array(data[0])[0];
    }


    public override void FixedUpdate()
    {
        var owner = (PlayerJumper)machine.owner;
        owner.transform.position += direction * owner.movingSpeed * Time.deltaTime;
    }
}