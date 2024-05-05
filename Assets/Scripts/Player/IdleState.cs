using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{

    public override void Enter(string[] data = null)
    {
        ((PlayerController)machine.owner).lineRenderer.enabled = true;
    }


    public override void Update()
    {
        var owner = (PlayerController)machine.owner;
        owner.transform.up = owner.platform.up;
        if (Input.GetKeyDown(KeyCode.Space)) {
            machine.ChangeState("moving", new string[] { Util.SerializeVector3Array(new Vector3[] { owner.platform.up }) });

            if (owner.platform.TryGetComponent(out PlatformMoving plat))
                plat.hasPlayer = false;
        }
    }

    public override void Exit()
    {
        var owner = (PlayerController)machine.owner;
        owner.lineRenderer.enabled = false;
        owner.platform.gameObject.SetActive(false);
    }
}