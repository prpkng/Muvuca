using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movingSpeed;

    public Transform platform;

    private StateMachine machine = new();

    public LineRenderer lineRenderer;

    public static PlayerController Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        machine.AddState("idle", new IdleState());
        machine.AddState("moving", new MovingState());
        machine.owner = this;
        machine.ChangeState("idle", new string[] { });
        if (platform.TryGetComponent(out PlatformMoving plat))
            plat.hasPlayer = true;
    }

    void Update()
    {
        machine.Update();
    }

    private void FixedUpdate()
    {
        machine.FixedUpdate();
    }

    public Action<Transform> collidedWithPlatform;
}
