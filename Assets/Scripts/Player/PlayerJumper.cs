using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumper : MonoBehaviour
{
    public float movingSpeed;

    public Transform platform;

    private StateMachine machine = new();

    void Start()
    {
        machine.AddState("idle", new IdleState());
        machine.AddState("moving", new MovingState());
        machine.owner = this;
        machine.ChangeState("idle", new string[] { });
    }

    void Update()
    {
        machine.Update();
    }

    private void FixedUpdate()
    {
        machine.FixedUpdate();
    }
}
