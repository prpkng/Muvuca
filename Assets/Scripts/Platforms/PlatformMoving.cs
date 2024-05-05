using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public bool hasPlayer;
    public float rotationSpeed;

    public void Update()
    {
        if (!hasPlayer) return;
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }
}