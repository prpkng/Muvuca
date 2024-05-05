using System;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : State
{
    
    public Vector3 direction;

    public override void Enter(string[] data = null)
    {
        direction = Util.DeserializeVector3Array(data[0])[0];

        var owner = (PlayerController)machine.owner;
        owner.collidedWithPlatform += Collided;
    }
    public override void Exit()
    {
        ((PlayerController)machine.owner).collidedWithPlatform -= Collided;
    }

    private void Collided(Transform platform)
    {
        var owner = (PlayerController)machine.owner;
        owner.platform = platform;
        owner.transform.position = platform.position;
        if (platform.TryGetComponent(out PlatformMoving plat))
            plat.hasPlayer = true;
        machine.ChangeState("idle", null);
    }

    public override void FixedUpdate()
    {
        var owner = (PlayerController)machine.owner;
        owner.transform.position += direction * owner.movingSpeed * Time.deltaTime;
    }
}