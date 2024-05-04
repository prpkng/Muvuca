using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    public override void Enter(string[] data = null)
    {
    }


    public override void Update()
    {
        var owner = (PlayerJumper)machine.owner;
        owner.transform.up = owner.platform.up;
        if (Input.GetKeyDown(KeyCode.Space)) {
            machine.ChangeState("moving", new string[] { Util.SerializeVector3Array(new Vector3[] { owner.platform.up }) });
        }
    }
}