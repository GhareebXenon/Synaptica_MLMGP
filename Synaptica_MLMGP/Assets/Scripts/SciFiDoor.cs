using UnityEngine;

public class SciFiDoor : MonoBehaviour
{
    public bool isOpen = false;
    [SerializeField] private Vector3 slideDirection = Vector3.back;
    [SerializeField] private float slideAmount = 3f;
    [SerializeField] private float speed = 1.0f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool isMoving = false;

    private void Awake()
    {
        startPos = transform.position;
        targetPos = startPos;
    }

    private void Update()
    {
        if (isMoving)
        {
            MoveDoor();
        }
    }

    private void MoveDoor()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);

        if (Vector3.Distance(transform.position, targetPos) < 0.01f)
        {
            transform.position = targetPos;
            isMoving = false;
            isOpen = !isOpen;
        }
    }

    public void Open()
    {
        if (!isOpen && !isMoving)
        {
            targetPos = startPos + slideAmount * slideDirection;
            isMoving = true;
        }
    }

    public void Close()
    {
        if (isOpen && !isMoving)
        {
            targetPos = startPos;
            isMoving = true;
        }
    }
}