using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBPM : MonoBehaviour
{
    public float BPM;
    public float beatForce;
    public float returnSpeed;

    private Camera cam;

    private float startZoom;

    private float beatCounter;


    void Start()
    {
        cam = GetComponent<Camera>();
        startZoom = cam.orthographicSize;
    }

    void Update()
    {
        beatCounter += Time.deltaTime;

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, startZoom, Time.deltaTime * returnSpeed);

        if (beatCounter < 60f / BPM)
            return;

        beatCounter = 0;
        cam.orthographicSize = startZoom + beatForce;
    }
}
