using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public Transform target;

    public Transform farBackground;
    public Transform middleBackground;

    public float minHeight = -1.5f;
    public float maxHeight = 2.5f;

    public bool stopFollow;

    private Vector2 lastPos;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (stopFollow)
        {
            return;
        }

        float campedY = Mathf.Clamp(target.position.y, minHeight, maxHeight);
        transform.position = new Vector3(target.position.x, campedY, transform.position.z);

        Vector2 amountToMove = new Vector2(transform.position.x - lastPos.x, transform.position.y - lastPos.y);
        lastPos = transform.position;

        farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
        middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;
    }
}
