using UnityEngine;

public class PlatformController : MonoBehaviour, IEnablable
{
    public bool hasPlayer;
    public float rotationSpeed;

    private Quaternion startRot;
    private Vector3 startPos;

    private void Awake()
    {
        startRot = transform.rotation;
        startPos = transform.position;
    }

    public void Disable()
    {
        hasPlayer = false;
        gameObject.SetActive(false);
        LevelManager.Instance.disabledElements.Add(this);
    }

    public void Enable()
    {
        print("enabled");
        gameObject.SetActive(true);
        gameObject.AddComponent<PlatformReacher>();

        transform.rotation = startRot;
        transform.position = startPos;
    }

    public void Update()
    {
        if (!hasPlayer) return;
        transform.Rotate(rotationSpeed * Time.deltaTime * Vector3.forward);
    }
}