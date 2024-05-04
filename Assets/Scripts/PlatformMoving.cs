using UnityEngine;

public class PlatformMoving : MonoBehaviour
{
    public float rotationSpeed;

    public void Update()
    {
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }
}